using UnityEngine;

public class GhostController : MonoBehaviour
{
    [SerializeField] private AudioSource m_hurtSFX;
    [SerializeField] private AudioSource m_deadSFX;
    
    private EnemyHealth m_health;
    private GhostMovement m_movement;

    private void Start()
    {
        m_health = GetComponent<EnemyHealth>();  
        m_movement = GetComponent<GhostMovement>();
        m_movement.OnDisappearCallback += OnDisappear;
        m_health.OnHit += OnEnemyHit;
        m_health.OnDeath += OnEnemyDead;
    }

    private void OnEnemyDead()
    {
        m_deadSFX.Play();
    }

    private void OnEnemyHit(int obj)
    {
        m_hurtSFX.Play();
    }

    private void OnDestroy()
    {
        m_health.OnHit -= OnEnemyHit;
        m_health.OnDeath -= OnEnemyDead;
        m_movement.OnDisappearCallback -= OnDisappear;
    }

    private void OnDisappear(bool isInvi)
    {
        //On disappear ghost wont take damage
        m_health.SetNoDamage(isInvi);
    }
}
