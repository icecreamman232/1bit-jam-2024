using System;
using System.Collections;
using SGGames.Scripts.Managers;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private GameObject m_playerPrefab;
    [SerializeField] private int m_curCheckPtsIndex;
    [SerializeField] private bool m_debugMode;
    [SerializeField] private Transform m_debugTransform;
    [SerializeField] private Transform[] m_checkPoint;

    private GameObject m_player;
    
    private void Start()
    {
        StartCoroutine(SpawnPlayerProcess());
    }

    private IEnumerator SpawnPlayerProcess()
    {
        ScreenFader.Instance.FadeOut(isInstant:true);
        if (m_debugMode)
        {
            m_player = Instantiate(m_playerPrefab, m_debugTransform.position, Quaternion.identity);
        }
        else
        {
            m_player = Instantiate(m_playerPrefab, m_checkPoint[m_curCheckPtsIndex].position, Quaternion.identity);
        }
        yield return new WaitForSeconds(0.25f);
        ScreenFader.Instance.FadeIn();
        yield return new WaitForSeconds(0.5f);
    }

    private IEnumerator OnRevivePlayer()
    {
        ScreenFader.Instance.FadeOut();
        yield return new WaitForSeconds(0.5f);
        Destroy(m_player);
        StartCoroutine(SpawnPlayerProcess());
    }

    public void RevivePlayer()
    {
        Debug.Log("<color=orange>Revive Player</color>");
        StartCoroutine(OnRevivePlayer());
    }

    public void SaveCheckPoint(int index)
    {
        Debug.Log($"<color=orange>Save at checkpoint {index}</color>");
        m_curCheckPtsIndex = index;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (m_checkPoint == null || m_checkPoint.Length <= 0) return;
        for (int i = 0; i < m_checkPoint.Length; i++)
        {
            Gizmos.DrawCube(m_checkPoint[i].position,Vector3.one * 0.8f);
        }
    }
}
