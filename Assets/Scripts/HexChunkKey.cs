using System;
using UnityEngine;

[Serializable]
public readonly struct HexChunkKey : IEquatable<HexChunkKey>
{
    public readonly int cq;
    public readonly int cr;

    public HexChunkKey(int cq, int cr)
    {
        this.cq = cq;
        this.cr = cr;
    }

    public bool Equals(HexChunkKey other) => cq == other.cq && cr == other.cr;
    public override bool Equals(object obj) => obj is HexChunkKey other && Equals(other);
    public override int GetHashCode() => HashCode.Combine(cq, cr);
    public override string ToString() => $"Chunk({cq},{cr})";
}