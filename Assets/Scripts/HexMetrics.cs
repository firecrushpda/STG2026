using UnityEngine;

public static class HexMetrics
{
    // 尖顶(pointy-top) 六边形
    public const float SQRT3 = 1.7320508075688772f;

    public static Vector3 AxialToWorld(in HexCoord c, float size)
    {
        float x = size * SQRT3 * (c.q + c.r * 0.5f);
        float z = size * 1.5f * c.r;
        return new Vector3(x, 0f, z);
    }

    // world -> axial（近似，再用 cube rounding）
    public static HexCoord WorldToAxial(Vector3 pos, float size)
    {
        float qf = (SQRT3 / 3f * pos.x - 1f / 3f * pos.z) / size;
        float rf = (2f / 3f * pos.z) / size;
        return CubeRound(qf, rf);
    }

    static HexCoord CubeRound(float qf, float rf)
    {
        // axial(q,r) -> cube(x=q, z=r, y=-x-z)
        float xf = qf;
        float zf = rf;
        float yf = -xf - zf;

        int xi = Mathf.RoundToInt(xf);
        int yi = Mathf.RoundToInt(yf);
        int zi = Mathf.RoundToInt(zf);

        float dx = Mathf.Abs(xi - xf);
        float dy = Mathf.Abs(yi - yf);
        float dz = Mathf.Abs(zi - zf);

        if (dx > dy && dx > dz) xi = -yi - zi;
        else if (dy > dz) yi = -xi - zi;
        else zi = -xi - yi;

        // cube -> axial(q=x, r=z)
        return new HexCoord(xi, zi);
    }
}