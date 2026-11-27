using System;
using System.Collections.Generic;
using Dungeon.Core.Content;

namespace Dungeon.Core;

/// <summary>
/// One run of a generation stage and its DrawLedger (SV 4.2, DS15). Every stream the stage draws on is opened here, at
/// counter 0, and <see cref="Close"/> checks each opened row against its count class. A row of a later wave than the
/// build is inert and expects 0 draws (D-08). Draw indices are 0-based.
/// </summary>
public sealed class StageRun
{
    private readonly ContentSet _registry;
    private readonly string _stage;
    private readonly KeyValuePair<string, ulong>[] _parameters;

    // Opened streams grouped by row id, in ordinal row-id order; a per-entry row's units are its streams.
    private readonly SortedDictionary<string, List<LedgeredStream>> _opened = new(StringComparer.Ordinal);
    private readonly List<ulong> _keys = [];
    private bool _closed;

    /// <summary>Starts a run of <paramref name="stage"/> over the stream rows of <paramref name="registry"/>.</summary>
    /// <param name="registry">The content set holding the stream registry.</param>
    /// <param name="stage">The stage; only rows whose <see cref="StreamRow.Stage"/> equals it can be opened.</param>
    /// <param name="parameters">The values the rows' count formulas read, copied now.</param>
    /// <exception cref="ArgumentNullException">An argument is null.</exception>
    public StageRun(ContentSet registry, string stage, IReadOnlyList<KeyValuePair<string, ulong>> parameters)
    {
        ArgumentNullException.ThrowIfNull(registry);
        ArgumentNullException.ThrowIfNull(stage);
        ArgumentNullException.ThrowIfNull(parameters);
        _registry = registry;
        _stage = stage;
        _parameters = [.. parameters];
    }

    /// <summary>Opens a fresh ledgered stream on <paramref name="h"/>, at counter 0 (DS15).</summary>
    /// <exception cref="InvalidOperationException">
    /// The run is closed; the row is unknown or belongs to another stage; its class is not counted per stage
    /// (<c>vector</c>, <c>keyed</c>, <c>sequential-saved</c>, <c>display</c> or <c>sim</c>); or the handle's key is
    /// already open in this run.
    /// </exception>
    public LedgeredStream Open(StreamHandle h)
    {
        var row = RowOf(h);
        switch (row.Class)
        {
            case CountClass.Vector:
                throw new InvalidOperationException($"\"{row.Id}\" is a vector; read it with ReadVector.");
            case CountClass.Keyed:
            case CountClass.SequentialSaved:
            case CountClass.Display:
            case CountClass.Sim:
                throw new InvalidOperationException($"\"{row.Id}\" is not ledgered per stage, so a stage run cannot open it.");
        }

        if (_keys.Contains(h.Key))
        {
            throw new InvalidOperationException($"\"{row.Id}\" is already open in this run with the same key.");
        }

        _keys.Add(h.Key);

        var stream = new LedgeredStream(this, h);
        if (!_opened.TryGetValue(row.Id, out var streams))
        {
            streams = [];
            _opened.Add(row.Id, streams);
        }

        streams.Add(stream);
        return stream;
    }

    /// <summary>Reads draw <paramref name="index"/> of the vector <paramref name="h"/>, <c>Draw(key, index)</c>; vectors have no counter.</summary>
    /// <exception cref="InvalidOperationException">
    /// The run is closed; the row is unknown, belongs to another stage, is not a <c>vector</c>, or is inert (D-08).
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is not below the row's length.</exception>
    public ulong ReadVector(StreamHandle h, ulong index)
    {
        var row = RowOf(h);
        if (row.Class != CountClass.Vector)
        {
            throw new InvalidOperationException($"\"{row.Id}\" is not a vector.");
        }

        if (row.IsInert)
        {
            throw new InvalidOperationException($"\"{row.Id}\" is inert in this build's wave, so it expects no reads (D-08).");
        }

        ulong length = row.Length ?? 0;
        if (index >= length)
        {
            throw new ArgumentOutOfRangeException(nameof(index), index, $"\"{row.Id}\" has length {length}.");
        }

        return KeyAlgebra.Draw(h.Key, index);
    }

    /// <summary>
    /// Closes the run and checks every opened row in ordinal row-id order: an inert row or a <c>node</c> expects 0
    /// draws; <c>fixed</c>, <c>fixed-per-data</c> and <c>leaf-formula</c> expect their formula; a <c>per-entry</c>
    /// row expects at most its cap of units, each drawing its count. The run is closed even when a check fails.
    /// </summary>
    /// <exception cref="InvalidOperationException">The run is already closed.</exception>
    /// <exception cref="DrawLedgerMismatchException">The first row that drew other than its class allows.</exception>
    /// <exception cref="DrawLedgerOwedException">The first opened row whose formula or cap is owed.</exception>
    /// <exception cref="ArgumentException">A formula reads a parameter the run was not given, or gives no count.</exception>
    public void Close()
    {
        ThrowIfClosed();
        _closed = true;
        foreach (var (id, streams) in _opened)
        {
            var row = _registry.Find<StreamRow>(id)!;
            if (row.IsInert || row.Class == CountClass.Node)
            {
                Expect(id, streams, 0);
                continue;
            }

            ulong count = Evaluate(row, row.Count);
            if (row.Class == CountClass.PerEntry)
            {
                ulong cap = Evaluate(row, row.Cap);
                ulong units = (ulong)streams.Count;
                if (units > cap)
                {
                    throw new DrawLedgerMismatchException(id, cap, units);
                }
            }

            Expect(id, streams, count);
        }
    }

    internal void ThrowIfClosed()
    {
        if (_closed)
        {
            throw new InvalidOperationException($"The {_stage} stage run is closed.");
        }
    }

    private static void Expect(string id, List<LedgeredStream> streams, ulong expected)
    {
        foreach (var stream in streams)
        {
            if (stream.Drawn != expected)
            {
                throw new DrawLedgerMismatchException(id, expected, stream.Drawn);
            }
        }
    }

    private ulong Evaluate(StreamRow row, CountFormula? formula)
    {
        if (formula is null)
        {
            throw new InvalidOperationException($"\"{row.Id}\" is {row.Class} and has no formula to check against.");
        }

        if (formula.OwedBy is { } owner)
        {
            throw new DrawLedgerOwedException(row.Id, owner);
        }

        return formula.Evaluate(_parameters);
    }

    // The open row of h in this run's stage.
    private StreamRow RowOf(StreamHandle h)
    {
        ThrowIfClosed();
        var row = _registry.Find<StreamRow>(h.RowId)
            ?? throw new InvalidOperationException($"\"{h.RowId}\" names no stream row.");
        if (!string.Equals(row.Stage, _stage, StringComparison.Ordinal))
        {
            throw new InvalidOperationException($"\"{row.Id}\" belongs to stage {row.Stage ?? "(none)"}, not {_stage}.");
        }

        return row;
    }
}
