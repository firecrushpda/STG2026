using System;
using UnityEngine;

[Serializable]
public readonly struct HexCoord : IEquatable<HexCoord>
{
    public readonly int q;
    public readonly int r;

    public HexCoord(int q, int r)
    {
        this.q = q;
        this.r = r;
    }

    public bool Equals(HexCoord other) => q == other.q && r == other.r;
    public override bool Equals(object obj) => obj is HexCoord other && Equals(other);
    public override int GetHashCode() => HashCode.Combine(q, r);
    public override string ToString() => $"({q},{r})";

    public static HexCoord operator +(HexCoord a, HexCoord b) => new(a.q + b.q, a.r + b.r);
}