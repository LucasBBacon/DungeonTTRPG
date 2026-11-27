using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using Dungeon.Core.Content;

namespace Dungeon.Core;

/// <summary>
/// A stream row's count formula (plan call 4), evaluated exactly over <see cref="Rational"/>. The grammar:
/// <code>
/// formula := "owed:" owner | sum
/// sum     := product ("+" product)*
/// product := factor ("*" factor)*
/// factor  := integer ["/" integer] | name | "floor" "(" sum ")" | "(" sum ")"
/// </code>
/// An integer is a non-negative decimal literal with no leading zero; <c>p/q</c> is a rational literal in
/// canonical form (reduced, <c>q &gt; 1</c>, DS13); a name or owner is <c>[A-Za-z][A-Za-z0-9]*</c>, and
/// <c>floor</c> is not a name. Whitespace between tokens is ignored. <c>owed:OWNER</c> marks a formula
/// another owner has not yet published.
/// </summary>
public sealed class CountFormula
{
    private const string OwedPrefix = "owed:";
    private const string FloorName = "floor";

    private readonly string _source;
    private readonly Node? _root;

    private CountFormula(string source, Node? root, string? owedBy, ImmutableArray<string> parameters)
    {
        _source = source;
        _root = root;
        OwedBy = owedBy;
        Parameters = parameters;
    }

    /// <summary>True if the formula is <c>owed:OWNER</c>: its owner has not published it, so it cannot be evaluated.</summary>
    public bool IsOwed => OwedBy is not null;

    /// <summary>The owner an owed formula waits on, or null if the formula is published.</summary>
    public string? OwedBy { get; }

    /// <summary>The names the formula reads, ordinal and distinct; the stage run supplies their values.</summary>
    public ImmutableArray<string> Parameters { get; }

    /// <summary>Parses <paramref name="s"/>; on failure <paramref name="error"/> says why and <paramref name="f"/> is null.</summary>
    public static bool TryParse(string s, out CountFormula f, out string error)
    {
        f = null!;
        error = string.Empty;
        if (s is null)
        {
            error = "the formula is null.";
            return false;
        }

        if (s.StartsWith(OwedPrefix, StringComparison.Ordinal))
        {
            string owner = s.Substring(OwedPrefix.Length);
            if (!IsName(owner))
            {
                error = $"\"{s}\" does not name an owner after \"{OwedPrefix}\" ([A-Za-z][A-Za-z0-9]*).";
                return false;
            }

            f = new CountFormula(s, null, owner, []);
            return true;
        }

        var parser = new Parser(s);
        if (!parser.TryParse(out var root, out var problem))
        {
            error = $"\"{s}\" is not a count formula: {problem}";
            return false;
        }

        f = new CountFormula(s, root, null, parser.Names.ToImmutableArray());
        return true;
    }

    /// <summary>
    /// Evaluates the formula with the named <paramref name="parameters"/>; names it does not read are ignored.
    /// </summary>
    /// <exception cref="InvalidOperationException">The formula is owed.</exception>
    /// <exception cref="ArgumentException">
    /// A parameter it reads is missing or given twice, or the result is negative or not an integer.
    /// </exception>
    /// <exception cref="OverflowException">A value or an intermediate result leaves <c>Int64</c>.</exception>
    public ulong Evaluate(IReadOnlyList<KeyValuePair<string, ulong>> parameters)
    {
        ArgumentNullException.ThrowIfNull(parameters);
        if (_root is null)
        {
            throw new InvalidOperationException($"The count formula \"{_source}\" is owed by {OwedBy}, who has not published it.");
        }

        var value = _root.Evaluate(name => Lookup(parameters, name));
        if (value.Q != 1)
        {
            throw new ArgumentException($"The count formula \"{_source}\" gives {value}, which is not an integer.", nameof(parameters));
        }

        if (value.P < 0)
        {
            throw new ArgumentException($"The count formula \"{_source}\" gives {value}, which is negative.", nameof(parameters));
        }

        return (ulong)value.P;
    }

    /// <summary>The source text, exactly as parsed.</summary>
    public override string ToString() => _source;

    private Rational Lookup(IReadOnlyList<KeyValuePair<string, ulong>> parameters, string name)
    {
        ulong? found = null;
        for (int i = 0; i < parameters.Count; i++)
        {
            if (string.Equals(parameters[i].Key, name, StringComparison.Ordinal))
            {
                if (found is not null)
                {
                    throw new ArgumentException($"The parameter \"{name}\" of \"{_source}\" is given twice.", nameof(parameters));
                }

                found = parameters[i].Value;
            }
        }

        if (found is not { } v)
        {
            throw new ArgumentException($"The count formula \"{_source}\" needs the parameter \"{name}\".", nameof(parameters));
        }

        return new Rational(checked((long)v), 1);
    }

    private static bool IsName(string s)
    {
        if (s.Length == 0 || !IsLetter(s[0]))
        {
            return false;
        }

        foreach (var c in s)
        {
            if (!IsLetter(c) && !IsDigit(c))
            {
                return false;
            }
        }

        return true;
    }

    private static bool IsLetter(char c) => (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z');

    private static bool IsDigit(char c) => c >= '0' && c <= '9';

    private static bool IsSpace(char c) => c == ' ' || c == '\t' || c == '\r' || c == '\n';

    private abstract class Node
    {
        public abstract Rational Evaluate(Func<string, Rational> lookup);
    }

    private sealed class Literal(Rational value) : Node
    {
        public override Rational Evaluate(Func<string, Rational> lookup) => value;
    }

    private sealed class Name(string name) : Node
    {
        public override Rational Evaluate(Func<string, Rational> lookup) => lookup(name);
    }

    private sealed class Sum(Node left, Node right) : Node
    {
        public override Rational Evaluate(Func<string, Rational> lookup) => left.Evaluate(lookup).Add(right.Evaluate(lookup));
    }

    private sealed class Product(Node left, Node right) : Node
    {
        public override Rational Evaluate(Func<string, Rational> lookup) => left.Evaluate(lookup).Multiply(right.Evaluate(lookup));
    }

    private sealed class Floor(Node inner) : Node
    {
        public override Rational Evaluate(Func<string, Rational> lookup) => new(inner.Evaluate(lookup).Floor(), 1);
    }

    /// <summary>Recursive descent over the source; the first error stops it.</summary>
    private sealed class Parser(string source)
    {
        private int _at;

        /// <summary>The names read, kept ordinal and distinct as they are found.</summary>
        public List<string> Names { get; } = [];

        public bool TryParse(out Node root, out string error)
        {
            root = null!;
            error = string.Empty;
            try
            {
                root = ParseSum();
                SkipSpace();
                if (_at < source.Length)
                {
                    throw Fail($"unexpected '{source[_at]}'");
                }

                return true;
            }
            catch (FormatException ex)
            {
                error = ex.Message;
                return false;
            }
        }

        private Node ParseSum()
        {
            var node = ParseProduct();
            while (Accept('+'))
            {
                node = new Sum(node, ParseProduct());
            }

            return node;
        }

        private Node ParseProduct()
        {
            var node = ParseFactor();
            while (Accept('*'))
            {
                node = new Product(node, ParseFactor());
            }

            return node;
        }

        private Node ParseFactor()
        {
            SkipSpace();
            if (_at >= source.Length)
            {
                throw Fail("the formula ends where a value is expected");
            }

            char c = source[_at];
            if (IsDigit(c))
            {
                string text = ReadWhile(IsDigit);
                if (Accept('/'))
                {
                    SkipSpace();
                    text += "/" + ReadWhile(IsDigit);
                }

                return new Literal(Number(text));
            }

            if (IsLetter(c))
            {
                string name = ReadWhile(ch => IsLetter(ch) || IsDigit(ch));
                if (string.Equals(name, FloorName, StringComparison.Ordinal))
                {
                    Expect('(');
                    var inner = ParseSum();
                    Expect(')');
                    return new Floor(inner);
                }

                int at = Names.BinarySearch(name, StringComparer.Ordinal);
                if (at < 0)
                {
                    Names.Insert(~at, name);
                }

                return new Name(name);
            }

            if (Accept('('))
            {
                var inner = ParseSum();
                Expect(')');
                return inner;
            }

            throw Fail($"unexpected '{c}'");
        }

        // An integer, or p/q in the canonical form Rational.TryParse accepts (reduced, q > 1).
        private Rational Number(string text) =>
            Rational.TryParse(text, out var r)
                ? r
                : throw Fail($"\"{text}\" is not a non-negative integer or a reduced p/q with q > 1");

        private string ReadWhile(Func<char, bool> accept)
        {
            int start = _at;
            while (_at < source.Length && accept(source[_at]))
            {
                _at++;
            }

            return source.Substring(start, _at - start);
        }

        private bool Accept(char c)
        {
            SkipSpace();
            if (_at < source.Length && source[_at] == c)
            {
                _at++;
                return true;
            }

            return false;
        }

        private void Expect(char c)
        {
            if (!Accept(c))
            {
                throw Fail($"'{c}' expected");
            }
        }

        private void SkipSpace()
        {
            while (_at < source.Length && IsSpace(source[_at]))
            {
                _at++;
            }
        }

        private FormatException Fail(string problem) =>
            new($"{problem} at character {_at.ToString(CultureInfo.InvariantCulture)}.");
    }
}
