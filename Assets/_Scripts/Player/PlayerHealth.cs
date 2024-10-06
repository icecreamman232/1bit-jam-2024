using System.Collections;
using SGGames.Scripts.ScriptableEvent;
using UnityEngine;

public class PlayerHealth : Health
{
    [SerializeField] private bool m_godMode;
    [SerializeField] private IntEvent m_updateHealthBarEvent;
    [SerializeField] private float m_deadAnimDuration;
    
    private Animator m_animator;
    private BoxCollider2D m_collider2D;
    private PlayerHorizontalMovement m_horizontalMovement;
    
    protected override void Start()
    {
        m_horizontalMovement = GetComponent<PlayerHorizontalMovement>();
        m_collider2D = GetComponent <BoxCollider2D>();
        m_animator = GetComponentInChildren<Animator>();
        base.Start();
    }

    public override void TakeDamage(int damage, GameObject instigator, float invulnerableDuration)
    {
        #if UNITY_EDITOR
                if (m_godMode) return;
        #endif
        base.TakeDamage(damage, instigator, invulnerableDuration);
    }

    protected override void UpdateHealthBar()
    {
        m_updateHealthBarEvent.Raise(m_currentHealth);
        base.UpdateHealthBar();
    }

    protected override void Kill()
    {
        StartCoroutine(OnKillPlayer());
    }

    private IEnumerator OnKillPlayer()
    {
        m_collider2D.enabled = false;
        m_horizontalMovement.ToggleCamera(false);
        m_animator.SetTrigger("Dead");
        yield return new WaitForSeconds(m_deadAnimDuration);
        this.gameObject.SetActive(false);
    }
}
