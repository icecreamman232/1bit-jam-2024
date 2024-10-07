using System;
using System.Collections;
using SGGames.Scripts.Managers;
using UnityEngine;
using Random = UnityEngine.Random;

public class GhostMovement : MonoBehaviour
{
    [Header("Moving")]
    [SerializeField] private float m_speed;
    [SerializeField] private float m_leftPatrolDistance;
    [SerializeField] private float m_rightPatrolDistance;

    [Header("Disappear Behavior")] 
    [SerializeField] private SpriteRenderer m_spriteRenderer;
    [SerializeField] private float m_fadeTime;
    [SerializeField] private float m_minDelayToInvi;
    [SerializeField] private float m_maxDelayToInvi;
    [SerializeField] private float m_stayInviMinTime;
    [SerializeField] private float m_stayInviMaxTime;

    private float m_delayToInvi;
    
    private bool m_isMovingToRight;
    private float m_lerpValue;
    private bool m_isDisappear;
    private float m_fadeTimer;

    private Vector3 m_leftPoint;
    private Vector3 m_rightPoint;

    public Action<bool> OnDisappearCallback;
    
    private void Start()
    {
        m_leftPoint = transform.position - new Vector3(m_leftPatrolDistance, 0, 0);
        m_rightPoint = transform.position + new Vector3(m_rightPatrolDistance, 0, 0);

        m_delayToInvi = Random.Range(m_minDelayToInvi, m_maxDelayToInvi);
    }

    private void Update()
    {
        UpdateMovement();
        UpdateAppear();
    }

    private void UpdateMovement()
    {
        if (m_isMovingToRight)
        {
            m_lerpValue += Time.deltaTime * m_speed;
            if (m_lerpValue >= 1)
            {
                m_isMovingToRight = false;
                m_lerpValue = 1;
            }
        }
        else
        {
            m_lerpValue -= Time.deltaTime * m_speed;
            if (m_lerpValue <= 0)
            {
                m_isMovingToRight = true;
                m_lerpValue = 0;
            }
        }
        
        transform.position = Vector3.Lerp(m_leftPoint, m_rightPoint, m_lerpValue);
    }

    private void UpdateAppear()
    {
        if (m_isDisappear) return;
        m_delayToInvi -= Time.deltaTime;
        if (m_delayToInvi <= 0)
        {
            StartCoroutine(OnDisappear());
        }
    }
    
    private IEnumerator OnDisappear()
    {
        if (m_isDisappear)
        {
            yield break;
        }

        Color selfColor;
        m_isDisappear = true;
        OnDisappearCallback?.Invoke(true);
        //Disappear
        while (m_fadeTimer < m_fadeTime)
        {
            m_fadeTimer += Time.deltaTime;
            selfColor = m_spriteRenderer.color;
            selfColor.a = 1- MathHelpers.Remap(m_fadeTimer, 0, m_fadeTime, 0, 1);
            m_spriteRenderer.color = selfColor;
            yield return null;
        }
        m_fadeTimer = m_fadeTime;

        var randDisappearDuration = Random.Range(m_stayInviMinTime, m_stayInviMaxTime);
        
        yield return new WaitForSeconds(randDisappearDuration);
        
        //Appear
        while (m_fadeTimer > 0)
        {
            m_fadeTimer -= Time.deltaTime;
            selfColor = m_spriteRenderer.color;
            selfColor.a = 1 - MathHelpers.Remap(m_fadeTimer, 0, m_fadeTime, 0, 1);
            m_spriteRenderer.color = selfColor;
            yield return null;
        }

        //Random new delay
        m_delayToInvi = Random.Range(m_minDelayToInvi, m_maxDelayToInvi);
        OnDisappearCallback?.Invoke(false);
        m_isDisappear = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(m_leftPoint,Vector3.one * 0.6f);
        Gizmos.DrawCube(m_rightPoint,Vector3.one * 0.6f);
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position,m_leftPoint);
        Gizmos.DrawLine(transform.position,m_rightPoint);
    }
}
