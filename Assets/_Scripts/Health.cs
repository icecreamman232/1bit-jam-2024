using System;
using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] protected int m_maxHealth;
    [SerializeField] protected int m_currentHealth;

    protected bool m_isInvulnerable;
    
    public Action OnDeath;

    protected virtual void Start()
    {
        m_currentHealth = m_maxHealth;
    }

    public virtual void TakeDamage(int damage, GameObject instigator, float invulnerableDuration)
    {
        if (damage <= 0) { return; }

        if (m_isInvulnerable) return;

        m_currentHealth -= damage;
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

    protected virtual void UpdateHealthBar()
    {
        
    }

    protected virtual IEnumerator OnInvulnerable(float duration)
    {
        m_isInvulnerable = true;
        yield return new WaitForSeconds(duration);
        m_isInvulnerable = false;
    }

    protected virtual void Kill()
    {
        OnDeath?.Invoke();
        this.gameObject.SetActive(false);
    }
}
