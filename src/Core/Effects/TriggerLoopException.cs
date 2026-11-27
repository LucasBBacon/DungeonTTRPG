using System;
using System.Globalization;

namespace Dungeon.Core.Effects;

/// <summary>
/// One command fired more than <see cref="Effects.MaxTriggerFirings"/> triggers (M1-07 plan call 7): a trigger chain
/// that does not end, such as a trigger that re-applies its own effect. Class <c>defect</c>: content or a hook is
/// wrong, and the command's result is discarded.
/// </summary>
public sealed class TriggerLoopException : Exception
{
    /// <summary>Creates the loop report.</summary>
    /// <param name="eventIndex">The index, in the command's event list, of the event whose trigger passed the bound.</param>
    /// <param name="firings">The triggers already fired by the command.</param>
    public TriggerLoopException(int eventIndex, int firings)
        : base(string.Create(
            CultureInfo.InvariantCulture,
            $"TriggerLoop{{{eventIndex}, {firings}}}: one command may fire at most {Effects.MaxTriggerFirings} triggers."))
    {
        EventIndex = eventIndex;
        Firings = firings;
    }

    /// <summary>The index, in the command's event list, of the event whose trigger passed the bound.</summary>
    public int EventIndex { get; }

    /// <summary>The triggers fired before the one refused.</summary>
    public int Firings { get; }
}
