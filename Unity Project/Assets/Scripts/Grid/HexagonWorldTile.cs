public enum GroundType
{
    Plain,
    Grass,
    Rock,
    Tree
}

class HexagonWorldTile
{
    public HexagonWorldTile(HexagonCoord coord, GroundType type)
    {
        this.coord = coord;
        this.type = type;
    }

    public HexagonCoord coord;
    public GroundType type;
}
