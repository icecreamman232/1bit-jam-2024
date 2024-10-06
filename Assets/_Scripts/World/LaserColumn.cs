using System.Collections;
using UnityEngine;

public class LaserColumn : MonoBehaviour
{
    [SerializeField] private Animator m_animator;
    [SerializeField] private float m_disappearAnimDuration;
    [SerializeField] private float m_appearAnimDuration;
    [SerializeField] private bool m_isOpen = true;

    private bool m_isProcess;

    [ContextMenu("Turn on")]
    private void TestTurnOn()
    {
        TurnOn();
    }
    
    [ContextMenu("Turn off")]
    private void TestTurnOff()
    {
        TurnOff();
    }
    
    public void TurnOn()
    {
        if (m_isOpen) return;
        StartCoroutine(OnTurningOn());
    }
    
    public void TurnOff()
    {
        if (!m_isOpen) return;
        StartCoroutine(OnTurningOff());
    }

    private IEnumerator OnTurningOn()
    {
        if (m_isProcess)
        {
            yield break;
        }

        m_isProcess = true;
        m_isOpen = false;
        m_animator.SetTrigger("Appear");
        yield return new WaitForSeconds(m_appearAnimDuration);
        gameObject.layer = LayerMask.NameToLayer("Obstacle");
        m_isOpen = true;
        m_isProcess = false;
    }
    
    private IEnumerator OnTurningOff()
    {
        if (m_isProcess)
        {
            yield break;
        }

        m_isProcess = true;
        m_isOpen = true;
        m_animator.SetTrigger("Disappear");
        yield return new WaitForSeconds(m_disappearAnimDuration);
        gameObject.layer = LayerMask.NameToLayer("Default");
        m_isOpen = false;
        m_isProcess = false;
    }
}
