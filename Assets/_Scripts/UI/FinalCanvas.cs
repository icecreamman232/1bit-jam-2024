using System.Collections;
using SGGames.Scripts.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalCanvas : MonoBehaviour
{
    [SerializeField] private CanvasGroup m_canvasGroup;
    [SerializeField] private CanvasGroup m_thanksTxtGroup;
    [SerializeField] private CanvasGroup m_enjoyTxtGroup;

    private bool m_canReturnMenu;
    
    public void TriggerThankText()
    {
        m_canvasGroup.alpha = 1;
        StartCoroutine(OnTrigger());
    }

    private void Update()
    {
        if (m_canReturnMenu && Input.anyKeyDown)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }
    }

    private IEnumerator OnTrigger()
    {
        LevelManager.Instance.FreezePlayer();
        
        var thanksTimer = .0f;
        while (thanksTimer < 5.0f)
        {
            thanksTimer += Time.deltaTime;
            m_thanksTxtGroup.alpha = MathHelpers.Remap(thanksTimer, 0, 3.0f, 0, 1);
            yield return null;
        }

        m_thanksTxtGroup.alpha = 1;
        
        var enjoyTimer = .0f;
        while (enjoyTimer < 2.0f)
        {
            enjoyTimer += Time.deltaTime;
            m_enjoyTxtGroup.alpha = MathHelpers.Remap(enjoyTimer, 0, 1.5f, 0, 1);
            yield return null;
        }

        m_enjoyTxtGroup.alpha = 1;
        m_canReturnMenu = true;
    }

}
