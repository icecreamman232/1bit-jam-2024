using System;
using System.Collections;
using SGGames.Scripts.Data;
using SGGames.Scripts.ScriptableEvent;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossPhase2 : MonoBehaviour
{
    [SerializeField] private GameObject m_gunGroup;
    [SerializeField] private HangingGun[] m_hangingGun;
    [SerializeField] private SpriteRenderer m_renderer;
    [SerializeField] private GameObject m_warningSign;
    [SerializeField] private float m_warningDuration;
    [SerializeField] private float m_delayBeforeChangePos;
    [SerializeField] private ShakeEvent m_shakeEvent;
    [SerializeField] private ShakeProfile m_shakeProfile;

    private float m_timer;
    private bool m_isPlayingWarning = true;
    private bool m_isPlaying;
    public bool IsPlaying => m_isPlaying;
    
    public void EnterPhase()
    {
        StartCoroutine(OnEnterPhase());
    }
    
    public void StopPhase()
    {
        for (int i = 0; i < m_hangingGun.Length; i++)
        {
            m_hangingGun[i].StopShoot();
        }
        m_gunGroup.SetActive(false);
        m_warningSign.SetActive(false);
        StopAllCoroutines();
        m_isPlayingWarning = true; //Tricky to stop update loop
    }

    public void ExitPhase()
    {
        for (int i = 0; i < m_hangingGun.Length; i++)
        {
            m_hangingGun[i].StopShoot();
        }
        m_gunGroup.SetActive(false);
        m_warningSign.SetActive(false);
        StopAllCoroutines();
        m_isPlayingWarning = true; //Tricky to stop update loop
    }

    private IEnumerator OnEnterPhase()
    {
        m_isPlaying = true;
        m_renderer.enabled = false;
        transform.parent.position = new Vector3(0, 100, 0);
        yield return new WaitForEndOfFrame();
        m_renderer.enabled = true;
        
        m_gunGroup.SetActive(true);
        for (int i = 0; i < m_hangingGun.Length; i++)
        {
            m_hangingGun[i].Shoot();
        }

        m_timer = m_delayBeforeChangePos;
        StartCoroutine(OnShowingWarningSign(GetRandomPos()));
    }

    private Vector3 GetRandomPos()
    {
        var randX = Random.Range(-7.5f, 7.5f);
        var randY = Random.Range(97, 100);
        return new Vector3(randX, randY, 0);
    }
    
    private void Update()
    {
        if (m_isPlayingWarning)
        {
            return;
        }
        m_timer -= Time.deltaTime;
        if (m_timer <= 0)
        {
            m_timer = m_delayBeforeChangePos;
            StartCoroutine(OnShowingWarningSign(GetRandomPos()));
        }
    }

    private IEnumerator OnShowingWarningSign(Vector3 pos)
    {
        m_isPlayingWarning = true;
        
        m_renderer.enabled = false;
        m_warningSign.transform.position = pos;
        m_warningSign.SetActive(true);
        
        yield return new WaitForSeconds(m_warningDuration);
        
        m_warningSign.SetActive(false);
        transform.parent.position = pos;
        m_renderer.enabled = true;
        m_shakeEvent.Raise(m_shakeProfile);
        
        m_isPlayingWarning = false;
    }
}
