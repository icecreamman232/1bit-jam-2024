using System;
using UnityEngine;
using UnityEngine.UI;

public class HeartSlotUI : MonoBehaviour
{
    public enum HeartState
    {
        FULL,
        EMPTY,
    }

    [SerializeField] private Image m_icon;
    [SerializeField] private Sprite m_emptySprite;
    [SerializeField] private Sprite m_fullSprite;
    [SerializeField] private HeartState m_currentState;

    
    public void SetState(HeartState state)
    {
        m_currentState = state;
        switch (m_currentState)
        {
            case HeartState.FULL:
                m_icon.sprite = m_fullSprite;
                break;
            case HeartState.EMPTY:
                m_icon.sprite = m_emptySprite;
                break;
        }
    }
}
