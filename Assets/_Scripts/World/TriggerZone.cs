using System;
using System.Collections;
using System.Collections.Generic;
using SGGames.Scripts.Managers;
using UnityEngine;
using UnityEngine.Events;

public class TriggerZone : MonoBehaviour
{
    [SerializeField] private LayerMask m_targetMask;
    [SerializeField] private UnityEvent m_triggerEvent;
    [SerializeField] private bool m_triggerOnce;
    private bool m_hasTrigger;

    public void ResetTriggerZone()
    {
        m_hasTrigger = false;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (LayerManager.IsInLayerMask(other.gameObject.layer, m_targetMask) && !m_hasTrigger)
        {
            m_triggerEvent?.Invoke();
            if (m_triggerOnce)
            {
                m_hasTrigger = true;
            }
        }
    }
    
}
