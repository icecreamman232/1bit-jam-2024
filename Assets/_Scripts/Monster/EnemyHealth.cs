using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    [SerializeField] private bool m_noDamage;

    public void SetNoDamage(bool value)
    {
        m_noDamage = value;
    }

    public override void TakeDamage(int damage, GameObject instigator, float invulnerableDuration)
    {
        if (m_noDamage) return;
        base.TakeDamage(damage, instigator, invulnerableDuration);
    }
}
