using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridViewer : MonoBehaviour
{
    public GameObject BaseTile = null;
    public int Radius = 6;

    private Dictionary<HexagonCoord, HexagonScript> TilesDic = new Dictionary<HexagonCoord, HexagonScript>();

    private static GridViewer Instance = null;
    public static GridViewer GetInstance()
    {
        return Instance;
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

	void Start ()
    {
        List<HexagonCoord> results = new List<HexagonCoord>();

        results.Add(new HexagonCoord(0, 0));

        for(int radius = 1; radius <= Radius; ++radius)
        {
            // Starting on -Y, lets us you the enum order to iterate around perfectly
            HexagonCoord currentHex = HexagonHelper.DirectionToCoordOffset(Direction.YNegative, radius);

            for(int i = 0; i < 6; ++i)
            {
                for(int j = 0; j < radius; ++j)
                {
                    results.Add(currentHex);
                    currentHex = currentHex.Neighbor((Direction)i);
                }
            }
        }

        Vector3 basePosition = gameObject.transform.position;
        foreach(HexagonCoord coord in results)
        {
            Vector3 offset = HexagonHelper.CoordToWorldOffset(coord);
            GameObject go = (GameObject)GameObject.Instantiate(BaseTile, basePosition + offset, Quaternion.identity);
            HexagonScript hexagon = go.GetComponent<HexagonScript>();
            hexagon.SetCoordinate(coord);
            hexagon.name = coord.ToString();
            hexagon.transform.parent = gameObject.transform;

            TilesDic.Add(coord, hexagon);
        }
	}

    public HexagonScript GetHexagon(HexagonCoord coord)
    {
        HexagonScript hexagon = null;

        TilesDic.TryGetValue(coord, out hexagon);

        return hexagon;
    }

    // Fix for MouseOver Lagging
    int HighlightCount = 0;
    bool HighlightTrigger = false;
    bool HighlightOn = false;

    private void UpdateMouseOverLagging()
    {
        if (!HighlightTrigger)
        {
            HighlightTrigger = HighlightOn;


            foreach(KeyValuePair<HexagonCoord, HexagonScript> kvp in TilesDic)
            {
                if (!HighlightOn)
                {
                    kvp.Value.Highlight(Color.black);
                }
                else
                {
                    kvp.Value.Unhighlight();
                }
            }

            HighlightOn = true;
        }
        else
        {
            ++HighlightCount;
            if(HighlightCount < 2)
            {
                HighlightTrigger = false;
                HighlightOn = false;
            }
        }
    }

    public void Update()
    {
        UpdateMouseOverLagging();
    }
}
