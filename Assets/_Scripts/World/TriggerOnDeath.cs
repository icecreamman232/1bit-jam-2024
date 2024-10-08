using UnityEngine;
using UnityEngine.Events;

public class TriggerOnDeath : MonoBehaviour
{
    [SerializeField] private UnityEvent m_triggerEvent;
    private EnemyHealth m_health;

    private void Start()
    {
        m_health = GetComponent<EnemyHealth>();
        m_health.OnDeath += OnDeath;
    }

    private void OnDeath()
    {
        m_health.OnDeath -= OnDeath;
        m_triggerEvent?.Invoke();
    }
}
