using UnityEngine;

public sealed class HexChunk
{
    public readonly HexChunkKey key;
    public readonly GameObject root;

    readonly int _chunkSize;
    readonly float _hexSize;
    readonly GameObject _tilePrefab;

    public HexChunk(HexChunkKey key, int chunkSize, float hexSize, GameObject tilePrefab, Transform parent)
    {
        this.key = key;
        _chunkSize = chunkSize;
        _hexSize = hexSize;
        _tilePrefab = tilePrefab;

        root = new GameObject(key.ToString());
        root.transform.SetParent(parent, false);

        Build();
    }

    void Build()
    {
        // 这里用最简单的“axial 方块区域”：q 和 r 各生成 chunkSize
        int q0 = key.cq * _chunkSize;
        int r0 = key.cr * _chunkSize;

        for (int dr = 0; dr < _chunkSize; dr++)
        {
            for (int dq = 0; dq < _chunkSize; dq++)
            {
                var c = new HexCoord(q0 + dq, r0 + dr);
                var world = HexMetrics.AxialToWorld(c, _hexSize);

                var go = Object.Instantiate(_tilePrefab, world, _tilePrefab.transform.localRotation, root.transform);
                go.name = $"Hex{c}";
            }
        }
    }

    public void Destroy()
    {
        if (root != null) Object.Destroy(root);
    }
}