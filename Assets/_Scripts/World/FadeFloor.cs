using System.Collections;
using SGGames.Scripts.Managers;
using UnityEngine;

public class FadeFloor : MonoBehaviour
{
    [SerializeField] private float m_delayBeforeFade;
    [SerializeField] private float m_fadeDuration;
    private BoxCollider2D m_collider2D;
    private SpriteRenderer m_spriteRenderer;
    private bool m_isProcess;

    private void Start()
    {
        m_collider2D = GetComponent<BoxCollider2D>();
        m_spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            StartCoroutine(StartFade());
        }
    }

    private IEnumerator StartFade()
    {
        if (m_isProcess)
        {
            yield break;
        }

        m_isProcess = true;
        var timer = m_delayBeforeFade;
        var originalColor = m_spriteRenderer.color;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            var curColor = m_spriteRenderer.color;
            curColor.a = MathHelpers.Remap(timer, 0, m_delayBeforeFade, 0, 1);
            m_spriteRenderer.color = curColor;
            yield return null;
        }
        
        m_collider2D.enabled = false;

        yield return new WaitForSeconds(m_fadeDuration);
        m_collider2D.enabled = true;
        m_spriteRenderer.color = originalColor;
        
        m_isProcess = false;
    }
}
