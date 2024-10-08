using System;
using System.Collections;
using SGGames.Scripts.Managers;
using UnityEngine;

public class SecretPassageRevealer : MonoBehaviour
{
    [SerializeField] private float m_revealDuration;
    [SerializeField] private bool m_hasReveal;
    
    private SpriteRenderer m_spriteRenderer;
    
    private void Start()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Reveal()
    {
        StartCoroutine(OnReveal());
    }

    private IEnumerator OnReveal()
    {
        if (m_hasReveal)
        {
            yield break;
        }

        m_hasReveal = true;
        var timer = m_revealDuration;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            var curColor = m_spriteRenderer.color;
            curColor.a = MathHelpers.Remap(timer, 0, m_revealDuration, 0, 1);
            m_spriteRenderer.color = curColor;
            yield return null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawCube(transform.position,transform.localScale * 2);
    }
}
