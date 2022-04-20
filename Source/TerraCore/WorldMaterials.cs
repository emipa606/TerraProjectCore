using UnityEngine;
using Verse;

namespace TerraCore;

[StaticConstructorOnStartup]
public static class WorldMaterials
{
    public static readonly Material HillinessImpassable =
        MaterialPool.MatFrom("World/HillinessImpassable", ShaderDatabase.WorldOverlayTransparentLit, 3511);

    public static readonly Material CaveSystem =
        MaterialPool.MatFrom("World/CaveSystem", ShaderDatabase.WorldOverlayTransparentLit, 3512);

    public static readonly Material SpikeMountains = MaterialPool.MatFrom("World/LayerIcon/SpikeMountains",
        ShaderDatabase.WorldOverlayTransparentLit, 3515);

    public static readonly Material Oasis =
        MaterialPool.MatFrom("World/LayerIcon/Oasis", ShaderDatabase.WorldOverlayTransparentLit, 3515);

    public static readonly Material InfestedMountains = MaterialPool.MatFrom("World/LayerIcon/InfestedMountains",
        ShaderDatabase.WorldOverlayTransparentLit, 3515);

    public static readonly Material DeepRavine =
        MaterialPool.MatFrom("World/LayerIcon/DeepRavine", ShaderDatabase.WorldOverlayTransparentLit, 3515);

    public static readonly Material Archipelago =
        MaterialPool.MatFrom("World/LayerIcon/Archipelago", ShaderDatabase.WorldOverlayTransparentLit, 3515);

    public static readonly Material VolcanicIsland = MaterialPool.MatFrom("World/LayerIcon/VolcanicIsland",
        ShaderDatabase.WorldOverlayTransparentLit, 3515);

    public static readonly Material TundraSkerries = MaterialPool.MatFrom("World/LayerIcon/TundraSkerries",
        ShaderDatabase.WorldOverlayTransparentLit, 3515);

    public static readonly Material Atoll =
        MaterialPool.MatFrom("World/LayerIcon/Atoll", ShaderDatabase.WorldOverlayTransparentLit, 3515);


    public static readonly Material FrozenLake =
        MaterialPool.MatFrom("World/LayerIcon/FrozenLake", ShaderDatabase.WorldOverlayTransparentLit, 3515);

    public static readonly Material DesertDunes =
        MaterialPool.MatFrom("World/LayerIcon/DesertDunes", ShaderDatabase.WorldOverlayTransparentLit, 3515);

    public static readonly Material PackIce =
        MaterialPool.MatFrom("World/LayerIcon/PackIce", ShaderDatabase.WorldOverlayTransparentLit, 3515);
}