using SGGames.Scripts.Managers;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask m_obstacleMask;
    [SerializeField] private PlayerMovement m_playerMovement;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (LayerManager.IsInLayerMask(other.gameObject.layer, m_obstacleMask))
        {
            Hit();
        }
    }

    private void Hit()
    {
        m_playerMovement.ToggleAllow(false);
    }
}
