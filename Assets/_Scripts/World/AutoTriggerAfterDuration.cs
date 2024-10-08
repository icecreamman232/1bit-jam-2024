using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class AutoTriggerAfterDuration : MonoBehaviour
{
    [SerializeField] private float m_delayBeforeFirstStart;
    [SerializeField] private float m_frequentTrigger;
    [SerializeField] private UnityEvent m_triggerEvent;

    private float m_timer;
    private bool m_canUpdate;

    private IEnumerator Start()
    {
        m_canUpdate = false;
        yield return new WaitForSeconds(m_delayBeforeFirstStart);
        m_canUpdate = true;
    }

    private void Update()
    {
        if (!m_canUpdate) return;
        
        m_timer += Time.deltaTime;
        if (m_timer >= m_frequentTrigger)
        {
            m_timer = 0;
            m_triggerEvent?.Invoke();
        }
    }
}
