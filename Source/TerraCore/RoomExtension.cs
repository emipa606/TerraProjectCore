using Verse;

namespace TerraCore;

public static class RoomExtension
{
    public static bool OutdoorsByRCType(this Room room, RoomCalculationType rcType)
    {
        switch (rcType)
        {
            case RoomCalculationType.LessSky:
            {
                if (room.TouchesMapEdge)
                {
                    return true;
                }

                float num = room.OpenRoofCount;
                var num2 = num / room.CellCount;
                var num3 = 0.05f / ((0.003f * num) + 0.1f);
                return num2 >= num3;
            }
            case RoomCalculationType.NoSky:
                return room.TouchesMapEdge;
            default:
                return room.PsychologicallyOutdoors;
        }
    }
}