using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask m_obstacleMask;
    [SerializeField] private PlayerMovement m_playerMovement;
    [SerializeField] private float m_raycastDistance;
    [SerializeField] private Vector3 m_offsetGround;

    [Header("Status")] 
    [SerializeField] private float m_gravityPower;
    [SerializeField] private bool m_isGrounded;
    [SerializeField] private float m_groundY;

    [Header("Movement")] 
    [SerializeField] private PlayerJump m_playerJump;

    private void Update()
    {
        CheckGround();

        if (!m_isGrounded)
        {
            var curPos = transform.position;
            curPos.y -= Time.deltaTime * m_gravityPower;
            transform.position = curPos;
        }
    }

    private void CheckGround()
    {
        var ground_1 = Physics2D.Raycast(transform.position - m_offsetGround,
            Vector2.down, m_raycastDistance, m_obstacleMask);
        var ground_2 = Physics2D.Raycast(transform.position,
            Vector2.down, m_raycastDistance, m_obstacleMask);
        
        var ground_3 = Physics2D.Raycast(transform.position + m_offsetGround,
            Vector2.down, m_raycastDistance, m_obstacleMask);
        m_isGrounded = (ground_1 && ground_2) || (ground_1 && ground_3) || (ground_2 && ground_3);

        if (m_isGrounded)
        {
            if (ground_2.collider != null)
            {
                m_groundY = ground_2.point.y;
                m_playerJump.SetGround(m_groundY);
                var curPos = transform.position;
                curPos.y = m_groundY;
                transform.position = curPos;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position - m_offsetGround,transform.position - m_offsetGround + Vector3.down * m_raycastDistance);
        Gizmos.DrawLine(transform.position ,transform.position + Vector3.down * m_raycastDistance);
        Gizmos.DrawLine(transform.position + m_offsetGround,transform.position + m_offsetGround + Vector3.down * m_raycastDistance);
    }
}
