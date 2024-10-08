using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossPhase2 : MonoBehaviour
{
    [SerializeField] private GameObject m_gunGroup;
    [SerializeField] private HangingGun[] m_hangingGun;
    [SerializeField] private SpriteRenderer m_renderer;
    [SerializeField] private float m_delayBeforeChangePos;

    private float m_timer;
    
    public void EnterPhase()
    {
        StartCoroutine(OnEnterPhase());
    }

    public void ExitPhase()
    {
        for (int i = 0; i < m_hangingGun.Length; i++)
        {
            m_hangingGun[i].StopShoot();
        }
        m_gunGroup.SetActive(false);
    }

    private IEnumerator OnEnterPhase()
    {
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
        transform.parent.position = GetRandomPos();
    }

    private Vector3 GetRandomPos()
    {
        var randX = Random.Range(-7.5f, 7.5f);
        var randY = Random.Range(97, 100);
        return new Vector3(randX, randY, 0);
    }
    
    private void Update()
    {
        m_timer -= Time.deltaTime;
        if (m_timer <= 0)
        {
            m_timer = m_delayBeforeChangePos;
            transform.parent.position = GetRandomPos();
        }
    }
}
