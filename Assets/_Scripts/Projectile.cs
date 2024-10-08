using SGGames.Scripts.Managers;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float m_speed;
    [SerializeField] private SpriteRenderer m_spriteRenderer;
    [SerializeField] private LayerMask m_obstacleMask;
    [SerializeField] private DamageHandler m_damageHandler;
    [SerializeField] private Vector2 m_direction;
    private bool m_isAlive;

    private void OnEnable()
    {
        m_damageHandler.OnHit += OnHitTarget;
    }

    private void OnDisable()
    {
        m_damageHandler.OnHit -= OnHitTarget;
    }

    private void OnHitTarget(GameObject target)
    {
        SelfDestroy();
    }

    public void Spawn(Vector3 position, Vector2 shootDirection)
    {
        transform.position = position;
        m_spriteRenderer.transform.rotation = Quaternion.identity;
        m_direction = shootDirection;
        m_isAlive = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (LayerManager.IsInLayerMask(other.gameObject.layer, m_obstacleMask))
        {
            SelfDestroy();
        }
    }
    
    private void Update()
    {
        if (!m_isAlive) return;
        
        transform.Translate(m_direction * (Time.deltaTime * m_speed));
    }

    private void SelfDestroy()
    {
        m_isAlive = false;
        this.gameObject.SetActive(false);
    }
}
