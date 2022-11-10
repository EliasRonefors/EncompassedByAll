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
        //Debug.Log("OnBeginDrag");
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 plannedRectPos = rectTransform.anchoredPosition;

        plannedRectPos = rectTransform.anchoredPosition + eventData.delta / canvas.scaleFactor;

        if (Mathf.Abs(plannedRectPos.x) >= maskRect.sizeDelta.x / 2 - backgroundRect.anchoredPosition.x/2 - rectTransform.sizeDelta.x / 2)
        {
            plannedRectPos.x = rectTransform.anchoredPosition.x;
        }

        if (Mathf.Abs(plannedRectPos.y) >= maskRect.sizeDelta.y / 2 - rectTransform.sizeDelta.y / 2)
        {
            plannedRectPos.y = rectTransform.anchoredPosition.y;
        }

        rectTransform.anchoredPosition = plannedRectPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("OnEndDrag");
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }

        if (rectTransform.rect.Overlaps(teaMug.rect)) //If teabag overlaps with tea mug
        {
            gamePanel.TeaBagInCup();
        }
    }
}