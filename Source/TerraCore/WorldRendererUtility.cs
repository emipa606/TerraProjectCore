using System.Collections.Generic;
using RimWorld.Planet;
using UnityEngine;
using Verse;

namespace TerraCore;

public static class WorldRendererUtility
{
    private static readonly List<Vector3> tmpVerts = new List<Vector3>();

    public static void DrawTileTangentialToPlanetWithRodation(WorldGrid grid, LayerSubMesh subMesh, int tileID,
        int atlasX, int atlasZ, IntVec2 texturesInAtlas, int rotDir)
    {
        var count = subMesh.verts.Count;
        grid.GetTileVertices(tileID, tmpVerts);
        var count2 = tmpVerts.Count;
        if (rotDir < 0)
        {
            rotDir += count2;
        }

        for (var i = 0; i < count2; i++)
        {
            var index = (i + rotDir) % count2;
            subMesh.verts.Add(tmpVerts[index] + (tmpVerts[index].normalized * 0.012f));
            var vector = (GenGeo.RegularPolygonVertexPosition(count2, i) + Vector2.one) / 2f;
            vector.x = (vector.x + atlasX) / texturesInAtlas.x;
            vector.y = (vector.y + atlasZ) / texturesInAtlas.z;
            subMesh.uvs.Add(vector);
            if (i >= count2 - 2)
            {
                continue;
            }

            subMesh.tris.Add(count + i + 2);
            subMesh.tris.Add(count + i + 1);
            subMesh.tris.Add(count);
        }
    }

    public static void PrintQuadTangentialToPlanetWithRodation(Vector3 pos, Vector3 posForTangents, float size,
        float altOffset, LayerSubMesh subMesh, Vector3 rotVec)
    {
        GetTangentsToPlanetWithRotation(posForTangents, out var first, out var second, rotVec);
        var normalized = posForTangents.normalized;
        var num = size * 0.5f;
        var item = pos - (first * num) - (second * num) + (normalized * altOffset);
        var item2 = pos - (first * num) + (second * num) + (normalized * altOffset);
        var item3 = pos + (first * num) + (second * num) + (normalized * altOffset);
        var item4 = pos + (first * num) - (second * num) + (normalized * altOffset);
        var count = subMesh.verts.Count;
        subMesh.verts.Add(item);
        subMesh.verts.Add(item2);
        subMesh.verts.Add(item3);
        subMesh.verts.Add(item4);
        subMesh.tris.Add(count);
        subMesh.tris.Add(count + 1);
        subMesh.tris.Add(count + 2);
        subMesh.tris.Add(count);
        subMesh.tris.Add(count + 2);
        subMesh.tris.Add(count + 3);
    }

    public static void GetTangentsToPlanetWithRotation(Vector3 pos, out Vector3 first, out Vector3 second,
        Vector3 rotVec)
    {
        var quaternion = Quaternion.LookRotation(pos.normalized, rotVec);
        first = quaternion * Vector3.up;
        second = quaternion * Vector3.right;
    }
}