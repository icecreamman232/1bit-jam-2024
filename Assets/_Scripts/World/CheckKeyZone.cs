using UnityEngine;
using UnityEngine.Events;

public class CheckKeyZone : MonoBehaviour
{
    [SerializeField] private UnityEvent m_triggerEvent;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")
            && LevelManager.Instance.CollectedKeyNumber == 3)
        {
            m_triggerEvent?.Invoke();
        }
    }
}
