using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] private BossPhase1 m_phase1;
    [SerializeField] private BossPhase2 m_phase2;

    public void StartFightFromLeft()
    {
        m_phase1.EnterPhase(true);
    }
    
    public void StartFightFromRight()
    {
        m_phase1.EnterPhase(false);
    }
}
