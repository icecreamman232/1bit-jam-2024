using SGGames.Scripts.ScriptableEvent;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private ActionEvent m_collectKeyEvent;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            m_collectKeyEvent.Raise();
            Destroy(this.gameObject);
        }
    }
}
