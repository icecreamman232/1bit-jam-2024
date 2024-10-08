using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPhase2 : MonoBehaviour
{
    [SerializeField] private GameObject m_gunGroup;

    public void EnterPhase()
    {
        StartCoroutine(OnEnterPhase());
    }

    public void ExitPhase()
    {
        
    }

    private IEnumerator OnEnterPhase()
    {
        m_gunGroup.SetActive(true);
        yield return null;
    }
}
