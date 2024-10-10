using System.Collections;
using SGGames.Scripts.Managers;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private AudioSource m_audioSource;
    [SerializeField] private AudioClip m_exploreBGM;
    [SerializeField] private AudioClip m_bossBGM;
    
    public void StopMusic(float fadeDuration = 0.5f)
    {
        StartCoroutine(OnStoppingMusic(fadeDuration));
    }
    
    public void PlayExploreBGM(bool isStopPreviousAndPlay = true)
    {
        StartCoroutine(OnPlayNewMusic(m_exploreBGM,isStopPreviousAndPlay));
    }
    
    public void PlayBossBGM(bool isStopPreviousAndPlay = true)
    {
        StartCoroutine(OnPlayNewMusic(m_bossBGM,isStopPreviousAndPlay));
    }

    private IEnumerator OnPlayNewMusic(AudioClip newMusic, bool isStopAndPlayNew = true)
    {
        //Same music is playing so we exit
        if (m_audioSource.clip == newMusic)
        {
            yield break;
        }
        
        if (isStopAndPlayNew)
        {
            StopMusic(0.5f);
            yield return new WaitForSeconds(0.5f);
        }

        m_audioSource.volume = 0;
        m_audioSource.clip = newMusic;
        m_audioSource.Play();
        
        var timer = 0f;
        while (timer < 0.5f)
        {
            timer += Time.deltaTime;
            m_audioSource.volume = MathHelpers.Remap(timer, 0, 0.5f, 0, 1);
        }

        m_audioSource.volume = 1;
    }

    private IEnumerator OnStoppingMusic(float fadeDuration)
    {
        var timer = fadeDuration;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            m_audioSource.volume = MathHelpers.Remap(timer, 0, fadeDuration, 0, 1);
            yield return null;
        }
        m_audioSource.Stop();
    }
}
