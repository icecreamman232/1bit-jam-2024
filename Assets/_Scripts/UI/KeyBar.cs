using SGGames.Scripts.ScriptableEvent;
using UnityEngine;

public class KeyBar : MonoBehaviour
{
    [SerializeField] private ActionEvent m_collectKeyEvent;
    [SerializeField] private KeyIconUIView[] m_keyIcons;
    private int m_lastIndex;

    private void Start()
    {
        m_collectKeyEvent.AddListener(OnCollectKey);
        for (int i = 0; i < m_keyIcons.Length; i++)
        {
            m_keyIcons[i].ShowDisappear();
        }
    }

    private void OnDestroy()
    {
        m_collectKeyEvent.RemoveListener(OnCollectKey);
    }

    private void OnCollectKey()
    {
        m_keyIcons[m_lastIndex].ShowAppear();
        m_lastIndex++;
    }
}
