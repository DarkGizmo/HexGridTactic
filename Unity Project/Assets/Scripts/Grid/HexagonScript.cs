using UnityEngine;
using System;

public class HexagonScript : MonoBehaviour
{
    private HexagonCoord coord;

    public void Awake()
    {
        Unhighlight();
    }

    public void SetCoordinate(HexagonCoord coord)
    {
        this.coord = coord;
    }

    public void OnMouseEnter()
    {
        EventMouseEnter();
        //gameObject.BroadcastMessage("EventMouseEnter", SendMessageOptions.DontRequireReceiver);
    }

    public void OnMouseExit()
    {
        EventMouseExit();
        //gameObject.BroadcastMessage("EventMouseExit", SendMessageOptions.DontRequireReceiver);
    }

    public void Highlight(Color highlightColor)
    {
        GameObject highlight = GameObjectUtility.GetChildrenByTag(gameObject, "Highlight");
        
        if (highlight != null)
        {
            highlightColor = new Color(highlightColor.r, highlightColor.g, highlightColor.b, highlight.renderer.material.color.a);
            highlight.renderer.material.color = highlightColor;
            highlight.renderer.enabled = true;
        }
    }

    public void Unhighlight()
    {
        GameObject highlight = GameObjectUtility.GetChildrenByTag(gameObject, "Highlight");

        if (highlight != null)
        {
            highlight.renderer.enabled = false;
        }
    }

    public void EventMouseEnter()
    {
        Highlight(Color.green);

        for(int i = 0; i < 6; ++i)
        {
            HexagonScript neighbor = GridViewer.GetInstance().GetHexagon(coord.Neighbor((Direction)i));
            if(neighbor != null)
            {
                neighbor.Highlight(Color.yellow);
            }
        }
    }

    public void EventMouseExit()
    {
        Unhighlight();
        for(int i = 0; i < 6; ++i)
        {
            HexagonScript neighbor = GridViewer.GetInstance().GetHexagon(coord.Neighbor((Direction)i));
            if(neighbor != null)
            {
                neighbor.Unhighlight();
            }
        }
    }

}
