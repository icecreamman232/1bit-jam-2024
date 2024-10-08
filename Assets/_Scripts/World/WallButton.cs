using SGGames.Scripts.Managers;
using UnityEngine;
using UnityEngine.Events;

public class WallButton : MonoBehaviour
{
    [SerializeField] private LayerMask m_targetMask;
    [SerializeField] private UnityEvent m_ButtonTriggerEvent;
    [SerializeField] private bool m_triggerOnce = true;
    [SerializeField] private bool m_hasTriggered;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (LayerManager.IsInLayerMask(other.gameObject.layer, m_targetMask) && !m_hasTriggered)
        {
            if (m_triggerOnce)
            {
                m_hasTriggered = true;
            }
            m_ButtonTriggerEvent?.Invoke();
        }
    }
}
