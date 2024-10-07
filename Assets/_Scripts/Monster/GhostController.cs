using UnityEngine;

public class GhostController : MonoBehaviour
{
    private EnemyHealth m_health;
    private GhostMovement m_movement;

    private void Start()
    {
        m_health = GetComponent<EnemyHealth>();  
        m_movement = GetComponent<GhostMovement>();
        m_movement.OnDisappearCallback += OnDisappear;
    }

    private void OnDestroy()
    {
        m_movement.OnDisappearCallback -= OnDisappear;
    }

    private void OnDisappear(bool isInvi)
    {
        //On disappear ghost wont take damage
        m_health.SetNoDamage(isInvi);
    }
}
