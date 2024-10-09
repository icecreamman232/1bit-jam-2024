using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject m_creditPanel;
    public void LoadToGameplayScene()
    {
        SceneManager.LoadSceneAsync("GameplayScene",LoadSceneMode.Single);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (m_creditPanel.activeSelf)
            {
                m_creditPanel.SetActive(false);
            }
        }
    }
}
