using SGGames.Scripts.ScriptableEvent;
using UnityEngine;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private HeartSlotUI[] m_heartList;
    [SerializeField] private IntEvent m_updateHealthBarEvent;

    private void Awake()
    {
        m_updateHealthBarEvent.AddListener(OnUpdateHealthBar);
        for (int i = 0; i < m_heartList.Length; i++)
        {
            m_heartList[i].SetState(HeartSlotUI.HeartState.FULL);
        }
    }

    private void OnDestroy()
    {
        m_updateHealthBarEvent.RemoveListener(OnUpdateHealthBar);
    }

    private void OnUpdateHealthBar(int currentHealth)
    {
        var count = 0;
        for (int i = 0; i < m_heartList.Length; i++)
        {
            if (count < currentHealth)
            {
                m_heartList[i].SetState(HeartSlotUI.HeartState.FULL);
            }
            else
            {
                m_heartList[i].SetState(HeartSlotUI.HeartState.EMPTY);
            }

            count++;
        }
    }
}
