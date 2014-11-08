using UnityEngine;
using System.Collections;

[System.Serializable]
public class HexagonTextures
{
	public Texture2D[] subArray = new Texture2D[0];
}

public class StaminaBar : MonoBehaviour
{

	public Vector2 Position = new Vector2(Screen.height - 100.0f, 100.0f);

    public int IconSize = 48;
    public int IconSpacing = -5;
    public int BottomOffset = 100;

    public Texture2D background;
    public Texture2D foreground;

	public HexagonTextures[] StaminaCrystal = new HexagonTextures[0];
    public HexagonTextures Highlight = new HexagonTextures();
	public int test;

    public int BaseStamina = 36;
    public float RestedRatio = 1.0f / 3.0f;
    public float SpentRatio = 1.0f / 3.0f;

    public int HighlightedCount = 0;
    public bool UpdateHighlight = false;
    public float UpdateHighlightTime = 0.5f;

	// Use this for initialization
	void Start () {
	
	}

    private int PiecesIndexToCrystalIndex(int pieceIdx)
    {
        int restedPiecesIdx = Mathf.FloorToInt(BaseStamina * RestedRatio);
        int spendPiecesIdx = BaseStamina + Mathf.FloorToInt(BaseStamina * SpentRatio);

        if (pieceIdx > spendPiecesIdx)
        {
            return 3;
        }
        else if (pieceIdx > BaseStamina)
        {
            return 2;
        }
        else if (pieceIdx > restedPiecesIdx)
        {
            return 1;
        }

        return 0;
    }

    private float step = 0.0f;
    public void Update()
    {
        if (UpdateHighlight)
        {
            step += Time.deltaTime;

            if (step > UpdateHighlightTime)
            {
                step = 0.0f;
                ++HighlightedCount;
            }
        }
    }

    private int GetFullStaminaCount()
    {
        return BaseStamina + Mathf.CeilToInt(BaseStamina * SpentRatio);
    }

    private int GetNumberOfCrystal()
    {
        int fullStaminaPieces = GetFullStaminaCount();
        int crystalCount =  Mathf.CeilToInt(fullStaminaPieces / 6.0f);
        if(fullStaminaPieces % 6 == 0)
        {
            ++crystalCount;
        }

        return crystalCount;
    }

    private int GetTotalWidth()
    {
        int crystalCount = GetNumberOfCrystal();

        return crystalCount * (IconSpacing + IconSize);
    }

	public void OnGUI()
	{
        Vector2 offset = Vector2.zero;

        int crystalCount = GetNumberOfCrystal();

        int highlightLeft = HighlightedCount;
        int staminaBarXStart = Mathf.RoundToInt((Screen.width - GetTotalWidth()) / 2.0f);
        for (int i = 0; i < crystalCount; ++i)
        {
            Rect rect = new Rect(staminaBarXStart + offset.x, Screen.height - BottomOffset, IconSize, IconSize);
            GUI.DrawTexture(rect, background);
            for(int j = 0; j < 6; ++j)
            {
                int pieceIdx = i*6+j;
                GUI.DrawTexture(rect, StaminaCrystal[PiecesIndexToCrystalIndex(pieceIdx+1)].subArray[j]);
            }
            GUI.DrawTexture(rect, foreground);

            if(highlightLeft > 0)
            {
                int highlightLevel = Mathf.Min(highlightLeft, 7);

                GUI.DrawTexture(rect, Highlight.subArray[highlightLevel-1]);
                highlightLeft -= 6;
            }

            offset.x += IconSize + IconSpacing;
        }
	}
}


