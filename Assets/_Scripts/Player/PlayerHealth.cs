using SGGames.Scripts.ScriptableEvent;
using UnityEngine;

public class PlayerHealth : Health
{
    [SerializeField] private bool m_godMode;
    [SerializeField] private IntEvent m_updateHealthBarEvent;

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
}
