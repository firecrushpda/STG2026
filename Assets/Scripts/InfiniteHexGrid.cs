using System.Collections.Generic;
using UnityEngine;

public sealed class InfiniteHexGrid : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] Camera targetCamera;
    [SerializeField] GameObject hexTilePrefab;

    [Header("Grid")]
    [SerializeField] float hexSize = 1f;
    [SerializeField] int chunkSize = 16;

    [Header("Streaming")]
    [SerializeField] int viewDistanceChunks = 2;
    [SerializeField] float updateInterval = 0.2f;

    readonly Dictionary<HexChunkKey, HexChunk> _loaded = new();
    float _timer;

    void Reset()
    {
        targetCamera = Camera.main;
    }

    void Update()
    {
        if (targetCamera == null || hexTilePrefab == null) return;

        _timer += Time.deltaTime;
        if (_timer < updateInterval) return;
        _timer = 0f;

        StreamAroundCamera();
    }

    void StreamAroundCamera()
    {
        Vector3 camPos = targetCamera.transform.position;
        HexCoord centerHex = HexMetrics.WorldToAxial(camPos, hexSize);

        HexChunkKey centerChunk = HexToChunk(centerHex);

        var needed = new HashSet<HexChunkKey>();
        for (int dr = -viewDistanceChunks; dr <= viewDistanceChunks; dr++)
        {
            for (int dq = -viewDistanceChunks; dq <= viewDistanceChunks; dq++)
            {
                var key = new HexChunkKey(centerChunk.cq + dq, centerChunk.cr + dr);
                needed.Add(key);

                if (!_loaded.ContainsKey(key))
                {
                    _loaded[key] = new HexChunk(key, chunkSize, hexSize, hexTilePrefab, transform);
                }
            }
        }

        // unload far chunks
        var toRemove = new List<HexChunkKey>();
        foreach (var kv in _loaded)
        {
            if (!needed.Contains(kv.Key))
                toRemove.Add(kv.Key);
        }

        for (int i = 0; i < toRemove.Count; i++)
        {
            var k = toRemove[i];
            _loaded[k].Destroy();
            _loaded.Remove(k);
        }
    }

    HexChunkKey HexToChunk(HexCoord c)
    {
        int cq = HexMath.FloorDiv(c.q, chunkSize);
        int cr = HexMath.FloorDiv(c.r, chunkSize);
        return new HexChunkKey(cq, cr);
    }
}