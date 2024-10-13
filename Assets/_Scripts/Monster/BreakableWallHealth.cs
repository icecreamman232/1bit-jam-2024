using UnityEngine;

public class BreakableWallHealth : EnemyHealth
{
    [SerializeField] private Sprite m_brokenSprite;
    private SpriteRenderer m_spriteRenderer;

    protected override void Start()
    {
        m_spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        base.Start();
    }

    public override void TakeDamage(int damage, GameObject instigator, float invulnerableDuration)
    {
        if (damage <= 0) { return; }

        if (m_isInvulnerable) return;

        m_currentHealth -= damage;
        
        m_spriteRenderer.sprite = m_brokenSprite; 
        OnHit?.Invoke(m_currentHealth);
        UpdateHealthBar();
        
        if (m_currentHealth <= 0)
        {
            Kill();
        }
        else
        {
            StartCoroutine(OnInvulnerable(invulnerableDuration));
        }
    }
}
