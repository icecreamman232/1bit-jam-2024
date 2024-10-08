using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossPhase1 : MonoBehaviour
{
    [SerializeField] private int m_limitHealthToExitPhase;
    [SerializeField] private float m_speed;
    [SerializeField] private SpriteRenderer m_spriteRenderer;
    [SerializeField] private Transform m_leftPoint;
    [SerializeField] private Transform m_rightPoint;

    [Header("Spike")] 
    [SerializeField] private int m_healthLimitToTrigger;
    [SerializeField] private float m_delayTimeBeforeTriggerSpike;
    [SerializeField] private Transform m_spikeGroup1;
    [SerializeField] private Transform m_spikeGroup2;
    [SerializeField] private Transform m_spikeGroup3;
    [SerializeField] private Transform m_lowestSpikePivot;
    [SerializeField] private Transform m_highestSpikePivot;
    [SerializeField] private bool m_spikeHasBeenStarted;

    private BossHealth m_health;
    private float m_spikeTimer;
    private bool m_isMovingToRight;
    private bool m_isPlaying;
    private float m_lerpValue;

    private void Start()
    {
        m_health = GetComponentInParent<BossHealth>();
    }

    [ContextMenu("Test Phase 1")]
    public void EnterPhase(bool isPlayerAtLeft)
    {
        m_health.OnHit += OnBossHit;
        if (isPlayerAtLeft)
        {
            m_lerpValue = 1;
            m_isMovingToRight = false;
        }
        
        StartCoroutine(OnEnterPhase());
    }
    
    public void ExitPhase()
    {
        m_isPlaying = false;
        m_health.OnHit -= OnBossHit;
        StopAllCoroutines();
        m_spikeGroup1.gameObject.SetActive(false);
        m_spikeGroup2.gameObject.SetActive(false);
        m_spikeGroup3.gameObject.SetActive(false);
    }

    private void OnBossHit(int currentHealth)
    {
        if (currentHealth <= m_limitHealthToExitPhase)
        {
            ExitPhase();
            return;
        }
        
        //Start spike
        if (currentHealth <= m_healthLimitToTrigger && !m_spikeHasBeenStarted)
        {
            m_spikeHasBeenStarted = true;
            TriggerSpike();
        }
    }

    private void Update()
    {
        if (!m_isPlaying) return;
        
        UpdateMovement();
    }

    private void TriggerSpike()
    {
        var rand = Random.Range(0, 2);
        switch (rand)
        {
            case 0:
                Debug.Log("<color=orange>Trigger Group 1 and 3</color>");
                StartCoroutine(OnTriggerSpike(m_spikeGroup1));
                StartCoroutine(OnTriggerSpike(m_spikeGroup3));
                break;
            case 1:
                Debug.Log("<color=orange>Trigger Group 2</color>");
                StartCoroutine(OnTriggerSpike(m_spikeGroup2));
                break;
        }
    }

    private IEnumerator OnTriggerSpike(Transform spikeGroupTF)
    {
        var timer = 0f;
        var speed = 0.3f;
        var duration = 5;
        while (timer < 1)
        {
            timer += Time.deltaTime * speed;
            var curPos = spikeGroupTF.position;
            curPos.y = Mathf.Lerp(m_lowestSpikePivot.position.y, m_highestSpikePivot.position.y, timer);
            spikeGroupTF.position = curPos;
            yield return null;
        }

        var tempHigh = spikeGroupTF.position;
        tempHigh.y = m_highestSpikePivot.position.y;
        spikeGroupTF.position = tempHigh;
        timer = 1;

        yield return new WaitForSeconds(duration);
        while (timer > 0)
        {
            timer -= Time.deltaTime * speed;
            var curPos = spikeGroupTF.position;
            curPos.y = Mathf.Lerp(m_lowestSpikePivot.position.y, m_highestSpikePivot.position.y, timer);
            spikeGroupTF.position = curPos;
            yield return null;
        }
        var tempLow = spikeGroupTF.position;
        tempLow.y = m_lowestSpikePivot.position.y;
        spikeGroupTF.position = tempLow;

        TriggerSpike();
    }

    private void UpdateMovement()
    {
        if (m_isMovingToRight)
        {
            m_lerpValue += Time.deltaTime * m_speed;
            if (m_lerpValue >= 1)
            {
                m_lerpValue = 1;
                m_isMovingToRight = false;
            }
        }
        else
        {
            m_lerpValue -= Time.deltaTime * m_speed;
            if (m_lerpValue <= 0)
            {
                m_lerpValue = 0;
                m_isMovingToRight = true;
            }
        }
        transform.parent.position = Vector3.Lerp(m_leftPoint.position, m_rightPoint.position, m_lerpValue);
    }
    

    private IEnumerator OnEnterPhase()
    {
        m_spriteRenderer.enabled = false;
        yield return new WaitForEndOfFrame();
        
        if (m_isMovingToRight)
        {
            transform.parent.position = m_leftPoint.position;
        }
        else
        {
            transform.parent.position = m_rightPoint.position;
        }
        m_spriteRenderer.enabled = true;
        m_isPlaying = true;
    }
}
