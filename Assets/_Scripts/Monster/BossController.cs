using System.Collections;
using SGGames.Scripts.ScriptableEvent;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossController : MonoBehaviour
{
    [SerializeField] private BossPhase1 m_phase1;
    [SerializeField] private BossPhase2 m_phase2;
    [SerializeField] private AudioSource m_laughSFX;
    [SerializeField] private ActionEvent m_playerDeadEvent;
    [SerializeField] private TriggerZone m_leftTriggerZone;
    [SerializeField] private TriggerZone m_rightTriggerZone;
    [SerializeField] private FinalLadder m_finalLadder;
    
    private Health m_health;
    private float m_laughSFXTimer;
    
    private void OnPlayerDeath()
    {
        StartCoroutine(OnResettingBoss());
    }

    private IEnumerator OnResettingBoss()
    {
        if (m_phase1.IsPlaying)
        {
            m_phase1.StopPhase();
        }
        
        if (m_phase2.IsPlaying)
        {
            m_phase2.StopPhase();
        }
        
        m_leftTriggerZone.ResetTriggerZone();
        m_rightTriggerZone.ResetTriggerZone();
        
        m_health.ResetHealth();

        yield return new WaitForSeconds(0.7f);
        transform.position = new Vector3(0, 100, 0);
    }

    private void Start()
    {
        m_health = GetComponent<Health>();
        m_health.OnDeath += OnBossDeath;
        m_playerDeadEvent.AddListener(OnPlayerDeath);
    }

    private void OnDestroy()
    {
        m_playerDeadEvent.RemoveListener(OnPlayerDeath);
    }

    private void Update()
    {
        
        if (!m_phase1.IsPlaying && !m_phase2.IsPlaying) return;
        m_laughSFXTimer -= Time.deltaTime;
        if (m_laughSFXTimer <= 0)
        {
            m_laughSFX.Play();
            m_laughSFXTimer = Random.Range(2 * 60, 3 * 60);
        }
    }


    private void OnBossDeath()
    {
        m_phase2.ExitPhase();
        
        m_phase1.gameObject.SetActive(false);
        m_phase2.gameObject.SetActive(false);
        m_laughSFX.Stop();
        SoundManager.Instance.StopMusic();
        
        m_finalLadder.gameObject.SetActive(true);
        m_finalLadder.TriggerFinalLadder();
    }

    public void StartFightFromLeft()
    {
        SoundManager.Instance.PlayBossBGM();
        m_phase1.EnterPhase(true);
    }
    
    public void StartFightFromRight()
    {
        SoundManager.Instance.PlayBossBGM();
        m_phase1.EnterPhase(false);
    }

    public void SwitchToPhase2()
    {
        m_phase1.gameObject.SetActive(false);
        m_phase2.EnterPhase();
    }
    
}
