using System.Collections;
using UnityEngine;

public class BossDoor : MonoBehaviour
{
    [SerializeField] private bool m_isOpen;
    [SerializeField] private float m_openAnimDuration;
    [SerializeField] private float m_closeAnimDuration;
    private Animator m_animator;
    private BoxCollider2D m_collider2D;

    private void Start()
    {
        m_collider2D = GetComponent<BoxCollider2D>();
        m_animator = GetComponentInChildren<Animator>();
    }

    public void Open()
    {
        if (m_isOpen) return;
        SoundManager.Instance.StopMusic();
        StartCoroutine(OnOpen());
    }

    public void Close()
    {
        if (!m_isOpen) return;
        StartCoroutine(OnClose());
    }

    private IEnumerator OnOpen()
    {
        m_animator.SetTrigger("Open");
        yield return new WaitForSeconds(m_openAnimDuration);
        m_isOpen = true;
        m_collider2D.enabled = false;
    }
    
    private IEnumerator OnClose()
    {
        m_animator.SetTrigger("Close");
        yield return new WaitForSeconds(m_closeAnimDuration);
        m_isOpen = false;
        m_collider2D.enabled = true;
    }
}
