using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragBag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Canvas canvas;
    [SerializeField] private RectTransform maskRect;
    [SerializeField] private RectTransform backgroundRect;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private RectTransform teaMug;

    [SerializeField] private TeaGamePanel gamePanel;

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 plannedRectPos = (Vector2)rectTransform.position + eventData.delta / canvas.scaleFactor;

        float xLow = (Screen.width / 2) - (maskRect.sizeDelta.x / 2);
        float yLow = (Screen.height / 2) - (maskRect.sizeDelta.y / 2);

        float xHigh = (Screen.width / 2) + (maskRect.sizeDelta.x / 2);
        float yHigh = (Screen.height / 2) + (maskRect.sizeDelta.y / 2);

        if (xLow + rectTransform.sizeDelta.x / 2 >= rectTransform.position.x) //If plannedRectPos surpasses the left border
        {
            plannedRectPos.x = xLow + rectTransform.sizeDelta.x / 2;
        }
        else if (xHigh - rectTransform.sizeDelta.x / 2 <= rectTransform.position.x) //If plannedRectPos surpasses the right border
        {
            plannedRectPos.x = xHigh - rectTransform.sizeDelta.x / 2;
        }

        if (yLow + rectTransform.sizeDelta.y / 2 >= rectTransform.position.y) //If plannedRectPos surpasses the bottom border
        {
            plannedRectPos.y = yLow + rectTransform.sizeDelta.y / 2;
        }
        else if (yHigh - rectTransform.sizeDelta.y / 2 <= rectTransform.position.y) //If plannedRectPos surpasses the top border
        {
            plannedRectPos.y = yHigh - rectTransform.sizeDelta.y / 2;
        }

        rectTransform.position = plannedRectPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;

        if (RectTransformExtensions.GetWorldRect(teaMug).Contains(RectTransformExtensions.GetWorldRect(rectTransform).center)) //If teabag overlaps with tea mug
        {
            gamePanel.TeaBagInCup();
        }
    }

}

public static class RectTransformExtensions //This class is stolen from the web cause I couldn't find a better way to do this
{
    public static Rect GetWorldRect(this RectTransform rectTransform)
    {
        Vector3[] corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);
        return new Rect(corners[0], corners[2] - corners[0]);
    }
}