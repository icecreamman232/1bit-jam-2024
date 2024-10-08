using System.Collections;
using SGGames.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : Singleton<ScreenFader>
{
    [SerializeField] private CanvasGroup m_canvasGroup;

    private bool m_isProcess;
    
    /// <summary>
    /// Go to black
    /// </summary>
    /// <param name="isInstant"></param>
    public void FadeOut(bool isInstant = false)
    {
        if (isInstant)
        {
            m_canvasGroup.alpha = 1;
            return;
        }
        StartCoroutine(OnFadeOut());
    }

    /// <summary>
    /// Go to white
    /// </summary>
    /// <param name="isInstant"></param>
    public void FadeIn(bool isInstant = false)
    {
        if (isInstant)
        {
            m_canvasGroup.alpha = 0;
            return;
        }
        StartCoroutine(OnFadeIn());
    }

    private IEnumerator OnFadeIn()
    {
        if (m_isProcess)
        {
            yield break;
        }

        m_isProcess = true;
        
        var timer = 0.5f;
        
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            m_canvasGroup.alpha = MathHelpers.Remap(timer, 0, 0.5f, 0, 1);
            yield return null;
        }

        m_canvasGroup.alpha = 0;
        m_isProcess = false;
    }
    
    private IEnumerator OnFadeOut()
    {
        if (m_isProcess)
        {
            yield break;
        }

        m_isProcess = true;
        
        var timer = 0f;
        while (timer < 0.5f)
        {
            timer += Time.deltaTime;
            m_canvasGroup.alpha = MathHelpers.Remap(timer, 0, 0.5f, 0, 1);
            yield return null;
        }

        m_canvasGroup.alpha = 1;

        m_isProcess = false;
    }
}
