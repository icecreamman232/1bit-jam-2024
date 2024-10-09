using System;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] private BossPhase1 m_phase1;
    [SerializeField] private BossPhase2 m_phase2;

    private Health m_health;

    private void Start()
    {
        m_health = GetComponent<Health>();
        m_health.OnDeath += OnBossDeath;
    }

    private void OnBossDeath()
    {
        m_phase2.ExitPhase();
        //Show ending screen
    }

    public void StartFightFromLeft()
    {
        m_phase1.EnterPhase(true);
    }
    
    public void StartFightFromRight()
    {
        m_phase1.EnterPhase(false);
    }

    public void SwitchToPhase2()
    {
        m_phase2.EnterPhase();
    }

    public void SwitchToPhase1()
    {
        m_phase1.EnterPhase(LevelManager.Instance.Player.transform.position.x < 0);
    }
}
