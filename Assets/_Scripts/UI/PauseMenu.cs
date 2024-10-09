using System;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private CanvasGroup m_parentCanvasGroup;

    private bool m_isPausing;

    private void Start()
    {
        m_isPausing = false;
        m_parentCanvasGroup.alpha = 0;
        m_parentCanvasGroup.interactable = false;
        m_parentCanvasGroup.blocksRaycasts = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (m_isPausing)
            {
                Unpause();
            }
            else
            {
                Pause();
            }
        }
    }

    private void Pause()
    {
        m_isPausing = true;
        m_parentCanvasGroup.alpha = 1;
        m_parentCanvasGroup.interactable = true;
        m_parentCanvasGroup.blocksRaycasts = true;
        
        LevelManager.Instance.PauseGame();
    }

    public void Unpause()
    {
        m_isPausing = false;
        m_parentCanvasGroup.alpha = 0;
        m_parentCanvasGroup.interactable = false;
        m_parentCanvasGroup.blocksRaycasts = false;
        
        LevelManager.Instance.UnpauseGame();
    }
}
