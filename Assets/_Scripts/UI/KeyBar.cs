using SGGames.Scripts.ScriptableEvent;
using UnityEngine;
using UnityEngine.UI;

public class KeyBar : MonoBehaviour
{
    [SerializeField] private ActionEvent m_collectKeyEvent;
    [SerializeField] private Image[] m_keyIcons;
    private int m_lastIndex;

    private void Start()
    {
        m_collectKeyEvent.AddListener(OnCollectKey);
        for (int i = 0; i < m_keyIcons.Length; i++)
        {
            m_keyIcons[i].color = Color.black;
        }
    }

    private void OnDestroy()
    {
        m_collectKeyEvent.RemoveListener(OnCollectKey);
    }

    private void OnCollectKey()
    {
        m_keyIcons[m_lastIndex].color = Color.white;
        m_lastIndex++;
    }
}
