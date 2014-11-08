using UnityEngine;

// DO NOT change the order of this enum
public enum Direction
{
    XPositive = 0,
    YPositive = 1,
    ZPositive = 2,
    XNegative = 3,
    YNegative = 4,
    ZNegative = 5
}

class HexagonHelper
{
    public static int DirectionToInt(Direction direction)
    {
        return (int)direction;
    }

    public static Direction IntToDirection(int i)
    {
        return (Direction)i;
    }

    public static HexagonCoord DirectionToCoordOffset(Direction direction)
    {
        int coordX = 0;
        int coordY = 0;

        switch (direction)
        {
            case Direction.XPositive:
                coordX = 1;
                break;
            case Direction.YPositive:
                coordY = 1;
                break;
            case Direction.ZPositive:
                coordX = -1;
                coordY = 1;
                break;
            case Direction.XNegative:
                coordX = -1;
                break;
            case Direction.YNegative:
                coordY = -1;
                break;
            case Direction.ZNegative:
                coordX = 1;
                coordY = -1;
                break;
        }
        
        return new HexagonCoord(coordX, coordY);
    }

    public static HexagonCoord DirectionToCoordOffset(Direction direction, int radius)
    {
        return (new HexagonDirection(direction, radius)).ToCoordOffset();
    }

    public static Vector3 CoordToWorldOffset(HexagonCoord coord)
    {
        float height = 1.0f;
        float width = (Mathf.Sqrt(3) / 2.0f) * height;

        float worldOffsetX = 0.0f;
        float worldOffsetZ = 0.0f;

        worldOffsetX += 0.5f * width * coord.CoordX;
        worldOffsetZ += 0.75f * height * coord.CoordX;

        worldOffsetX += width * coord.CoordY;

        return new Vector3(worldOffsetX, 0.0f, worldOffsetZ);
    }
}
