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
        Vector2 plannedRectPos = rectTransform.anchoredPosition + eventData.delta / canvas.scaleFactor;

        float xLow = (Screen.width / 2) - (maskRect.sizeDelta.x / 2);
        float yLow = (Screen.height / 2) - (maskRect.sizeDelta.y / 2);

        float xHigh = (Screen.width / 2) + (maskRect.sizeDelta.x / 2);
        float yHigh = (Screen.height / 2) + (maskRect.sizeDelta.y / 2);

        Debug.LogFormat("X: {0}, Y: {1}", xLow, yLow);
        Debug.LogFormat("World positin tbag {0}", rectTransform.position);

        if (xLow + rectTransform.sizeDelta.x/2 > rectTransform.position.x || xHigh - rectTransform.sizeDelta.x/2 < rectTransform.position.x)
        {
            plannedRectPos.x = rectTransform.anchoredPosition.x;
        }

        if (yLow + rectTransform.sizeDelta.y/2 > rectTransform.position.y || yHigh - rectTransform.sizeDelta.y/2 < rectTransform.position.y)
        {
            plannedRectPos.y = rectTransform.anchoredPosition.y;
        }

        rectTransform.anchoredPosition = plannedRectPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;

        if (rectTransform.rect.Overlaps(teaMug.rect, true)) //If teabag overlaps with tea mug
        {
            gamePanel.TeaBagInCup();
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