using UnityEngine;
using Verse;

namespace TerraCore;

public class WLTileGraphicData
{
    public readonly int atlasX;

    public readonly int atlasZ;

    public readonly bool drawAsQuad = true;
    public readonly Material material;

    public readonly float posOffset;

    public readonly int rotDir;

    public readonly float sizeFactor = 1f;

    public readonly int texturesInAtlasX;

    public readonly int texturesInAtlasZ;

    public Vector3 rotVector = Vector3.up;

    public WLTileGraphicData()
    {
    }

    public WLTileGraphicData(Material material, FloatRange sizeFactor, FloatRange posOffset, bool randomRotation = true)
    {
        this.material = material;
        texturesInAtlasX = material.mainTexture.width / 256;
        texturesInAtlasZ = material.mainTexture.height / 256;
        atlasX = Rand.RangeInclusive(0, texturesInAtlasX - 1);
        atlasZ = Rand.RangeInclusive(0, texturesInAtlasZ - 1);
        this.sizeFactor = sizeFactor.RandomInRange;
        this.posOffset = posOffset.RandomInRange;
        if (randomRotation)
        {
            rotVector = Rand.UnitVector3;
        }
    }

    public WLTileGraphicData(Material material, int atlasX, int atlasZ, int rotDir)
    {
        this.material = material;
        texturesInAtlasX = material.mainTexture.width / 256;
        texturesInAtlasZ = material.mainTexture.height / 256;
        this.atlasX = atlasX;
        this.atlasZ = atlasZ;
        this.rotDir = rotDir;
        drawAsQuad = false;
    }
}