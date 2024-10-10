using System;
using SGGames.Scripts.ScriptableEvent;
using UnityEngine;

public class PlayerSoundBank : MonoBehaviour
{
    [SerializeField] private AudioSource m_jumpAudioSource;
    [SerializeField] private AudioSource m_shootAudioSource;
    [SerializeField] private AudioSource m_keyCollectAudioSource;
    [SerializeField] private AudioSource m_hurtAudioSource;
    [SerializeField] private ActionEvent m_keyCollectEvent;

    private Health m_health;

    private void Start()
    {
        m_keyCollectEvent.AddListener(OnCollectKey);
        m_health = GetComponent<Health>();
        m_health.OnHit += OnPlayerHurt;
    }
    
    private void OnDestroy()
    {
        m_keyCollectEvent.RemoveListener(OnCollectKey);
        m_health.OnHit -= OnPlayerHurt;
    }
    
    private void OnPlayerHurt(int obj)
    {
        m_hurtAudioSource.Play();
    }


    private void OnCollectKey()
    {
        m_keyCollectAudioSource.Play();
    }

    public void PlayJumpSFX()
    {
        m_jumpAudioSource.Play();
    }
    
    public void PlayShootSFX()
    {
        m_shootAudioSource.Play();
    }
}
