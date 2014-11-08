
public struct HexagonDirection
{
    public HexagonDirection(Direction direction, int scale)
    {
        this.direction = direction;
        this.scale = scale;
    }
    
    public HexagonDirection(Direction direction) : this(direction, 1)
    {
    }
    
    public int scale;
    public Direction direction;
    
    public HexagonCoord ToCoordOffset()
    {
        HexagonCoord coordOffset = HexagonHelper.DirectionToCoordOffset(direction).Scale(scale);
        return coordOffset;
    }
}

public class HexagonCoord
{
    private int hash = 0;

    public HexagonCoord(int coordX, int coordY)
    {
        this.coordX = coordX;
        this.coordY = coordY;

        UpdateHash();
    }
    
    public HexagonCoord() : this(0,0)
    {
    }
    
    private int coordX;
    private int coordY;

    public int CoordX
    {
        get
        {
            return coordX;
        }
    }

    public int CoordY
    {
        get
        {
            return coordY;
        }
    }

    public HexagonCoord Neighbor(Direction direction)
    {
        HexagonCoord neighbor = HexagonHelper.DirectionToCoordOffset(direction);
        return neighbor.Translate(this);
    }

    public HexagonCoord Translate(HexagonCoord translationCoord)
    {
        coordX += translationCoord.coordX;
        coordY += translationCoord.coordY;

        UpdateHash();
        
        return this;
    }

    public HexagonCoord Translate(HexagonDirection direction)
    {
        HexagonCoord translationCoord = direction.ToCoordOffset();
        
        coordX += translationCoord.coordX;
        coordY += translationCoord.coordY;

        UpdateHash();
        
        return this;
    }
    
    public HexagonCoord Scale(int scale)
    {
        this.coordX *= scale;
        this.coordY *= scale;

        UpdateHash();
        
        return this;
    }

    public override string ToString()
    {
        return string.Format("[HexagonCoord] ({0}, {1})", coordX, coordY);
    }

    private void UpdateHash()
    {
        hash = ((ushort)coordX << 16) | (ushort)coordY;
    }

    public override int GetHashCode()
    {
        return hash;
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as HexagonCoord);
    }

    public bool Equals(HexagonCoord obj)
    {
        return obj != null && obj.coordX == this.coordX && obj.coordY == this.coordY;
    }
}