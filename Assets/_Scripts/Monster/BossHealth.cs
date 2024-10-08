using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : EnemyHealth
{
    private bool m_isFinalHealth;

    public void SetFinalHealth()
    {
        m_isFinalHealth = true;
    }


    public override void TakeDamage(int damage, GameObject instigator, float invulnerableDuration)
    {
        base.TakeDamage(damage, instigator, invulnerableDuration);
    }

    protected override void Kill()
    {
        OnDeath?.Invoke();
        if (m_isFinalHealth)
        {
            this.gameObject.SetActive(false);
        }
    }
}
