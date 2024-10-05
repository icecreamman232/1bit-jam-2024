using System;
using SGGames.Scripts.Managers;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private float m_speed;
    [SerializeField] private SpriteRenderer m_spriteRenderer;
    [SerializeField] private LayerMask m_obstacleMask;
    
    private Vector2 m_direction;
    private bool m_isAlive;
    
    public void Spawn(Vector3 position, Vector2 shootDirection)
    {
        transform.position = position;
        m_spriteRenderer.transform.rotation = Quaternion.identity;
        m_direction = shootDirection;
        if (m_direction == Vector2.up || m_direction == Vector2.down)
        {
            m_spriteRenderer.transform.rotation = Quaternion.AngleAxis(90,Vector3.forward);
        }
        m_isAlive = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (LayerManager.IsInLayerMask(other.gameObject.layer, m_obstacleMask))
        {
            SelfDestroy();
        }
    }
    
    private void Update()
    {
        if (!m_isAlive) return;
        
        transform.Translate(m_direction * (Time.deltaTime * m_speed));
    }

    private void SelfDestroy()
    {
        m_isAlive = false;
        this.gameObject.SetActive(false);
    }
}
