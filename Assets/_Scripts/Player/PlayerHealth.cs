using SGGames.Scripts.ScriptableEvent;
using UnityEngine;

public class PlayerHealth : Health
{
    [SerializeField] private IntEvent m_updateHealthBarEvent;
    
    protected override void UpdateHealthBar()
    {
        m_updateHealthBarEvent.Raise(m_currentHealth);
        base.UpdateHealthBar();
    }
}
