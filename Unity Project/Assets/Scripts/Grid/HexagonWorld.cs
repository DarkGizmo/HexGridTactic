sealed class HexagonWorld
{

    private static readonly HexagonWorld instance = new HexagonWorld();

    private HexagonWorld()
    {
    }

    public static HexagonWorld Instance
    {
        get
        {
            return instance;
        }
    }


}
