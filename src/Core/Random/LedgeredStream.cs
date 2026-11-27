using System;

namespace Dungeon.Core;

/// <summary>
/// A stream opened by a <see cref="StageRun"/> (DS15): a fresh counter at 0 on its handle's key, which the DrawLedger
/// checks when the run closes. Draws are exactly those of a <see cref="DrawStream"/> on the same key; the counter is
/// <c>checked</c>, so an overflow throws instead of wrapping. Any use after the run closes throws.
/// </summary>
public sealed class LedgeredStream
{
    private readonly StageRun _run;
    private readonly ulong _key;
    private ulong _counter;

    internal LedgeredStream(StageRun run, StreamHandle handle)
    {
        _run = run;
        _key = handle.Key;
        Handle = handle;
    }

    /// <summary>The handle this stream was opened on.</summary>
    public StreamHandle Handle { get; }

    /// <summary>The 0-based index of the next draw: the number of draws so far.</summary>
    /// <exception cref="InvalidOperationException">The run is closed.</exception>
    public ulong Counter
    {
        get
        {
            _run.ThrowIfClosed();
            return _counter;
        }
    }

    // The counter for the ledger, read while the run closes.
    internal ulong Drawn => _counter;

    /// <summary>The next draw, <c>Draw(key, Counter)</c>; the counter advances by one.</summary>
    /// <exception cref="InvalidOperationException">The run is closed.</exception>
    /// <exception cref="OverflowException">The counter would pass 2^64 - 1.</exception>
    public ulong Next()
    {
        _run.ThrowIfClosed();
        ulong value = KeyAlgebra.Draw(_key, _counter);
        _counter = checked(_counter + 1);
        return value;
    }

    /// <summary>The next draw mapped onto <c>[0, n)</c> by <see cref="KeyAlgebra.Bound"/>.</summary>
    /// <exception cref="InvalidOperationException">The run is closed.</exception>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="n"/> is 0; the counter does not move.</exception>
    public ulong NextBelow(ulong n)
    {
        _run.ThrowIfClosed();
        if (n == 0)
        {
            throw new ArgumentOutOfRangeException(nameof(n), n, "NextBelow requires n >= 1.");
        }

        return KeyAlgebra.Bound(Next(), n);
    }

    /// <summary>Advances the counter by <paramref name="n"/> draws without consuming values; they count as draws.</summary>
    /// <exception cref="InvalidOperationException">The run is closed.</exception>
    /// <exception cref="OverflowException">The counter would pass 2^64 - 1.</exception>
    public void Skip(ulong n)
    {
        _run.ThrowIfClosed();
        _counter = checked(_counter + n);
    }
}
