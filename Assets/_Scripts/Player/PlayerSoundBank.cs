using System;
using SGGames.Scripts.ScriptableEvent;
using UnityEngine;

public class PlayerSoundBank : MonoBehaviour
{
    [SerializeField] private AudioSource m_jumpAudioSource;
    [SerializeField] private AudioSource m_shootAudioSource;
    [SerializeField] private AudioSource m_keyCollectAudioSource;
    [SerializeField] private ActionEvent m_keyCollectEvent;

    private void Start()
    {
        m_keyCollectEvent.AddListener(OnCollectKey);
    }

    private void OnDestroy()
    {
        m_keyCollectEvent.RemoveListener(OnCollectKey);
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
