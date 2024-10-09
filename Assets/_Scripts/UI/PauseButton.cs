using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseButton : Button
{
    [SerializeField] private CanvasGroup m_selectFrame;

    public override void OnPointerEnter(PointerEventData eventData)
    {
        m_selectFrame.alpha = 1;
        base.OnPointerEnter(eventData);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        m_selectFrame.alpha = 0;
        base.OnPointerExit(eventData);
    }
}
