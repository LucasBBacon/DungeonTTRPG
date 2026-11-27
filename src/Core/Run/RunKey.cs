using Dungeon.Core.Canonical;
using Dungeon.Core.Content;

namespace Dungeon.Core;

/// <summary>
/// The reproducibility key (SV 4.6): <c>(runSeed, genVersion, manifestHash, configHash, unlockHash)</c>.
/// Two runs with equal keys play identically on a build with the same rules version. Outside the key:
/// <c>displayHash</c>, <c>genContentHash</c> (M2 comparability), <c>runId</c>, profile memory and the UI.
/// </summary>
public readonly record struct RunKey(ulong RunSeed, int GenVersion, Digest ManifestHash, Digest ConfigHash, Digest UnlockHash)
    : ICanonical
{
    /// <summary>The key of a run on this build: the embedded manifest hash and <see cref="Dungeon.Core.GenVersion.Current"/>.</summary>
    /// <exception cref="System.ArgumentOutOfRangeException"><paramref name="runSeed"/> is at least 2^60.</exception>
    /// <exception cref="System.ArgumentException">The config or unlock snapshot is invalid.</exception>
    public static RunKey For(ulong runSeed, RunConfig config, UnlockSnapshot unlocks) =>
        new(
            Dungeon.Core.RunSeed.Validate(runSeed, nameof(runSeed)),
            Dungeon.Core.GenVersion.Current,
            Digest.Parse(ContentHashes.Manifest),
            config.ConfigHash,
            unlocks.UnlockHash);

    /// <summary>The key's own digest, for display and logs.</summary>
    public Digest Id => Digest.Of(this);

    /// <inheritdoc />
    public void WriteCanonical(CanonicalWriter w)
    {
        w.BeginRecord(5);
        w.UInt64(0, RunSeed);
        w.Int32(1, GenVersion);
        w.Record(2, new DigestRecord(ManifestHash));
        w.Record(3, new DigestRecord(ConfigHash));
        w.Record(4, new DigestRecord(UnlockHash));
        w.EndRecord();
    }

    private sealed class DigestRecord(Digest d) : ICanonical
    {
        public void WriteCanonical(CanonicalWriter w)
        {
            w.BeginRecord(4);
            w.UInt64(0, d.W0);
            w.UInt64(1, d.W1);
            w.UInt64(2, d.W2);
            w.UInt64(3, d.W3);
            w.EndRecord();
        }
    }
}
