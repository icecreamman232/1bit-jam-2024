using System.Collections;
using SGGames.Scripts.Managers;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [Header("Jump")]
    [SerializeField] private float m_jumpDuration;
    [SerializeField] private float m_groundY;
    [SerializeField] private float m_jumpPowerUp;
    [SerializeField] private float m_jumpPowerDown;
    [SerializeField] private float m_jumpHeight;
    [SerializeField] private float m_airTime;
    [Header("Animation")]
    [SerializeField] private Animator m_animator;

    private float m_jumpTimer;
    private float m_airTimer;

    private void Update()
    {
        HandleInput();
        UpdateMovement();
        UpdateAnimator();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && m_jumpTimer == 0)
        {
            Jump();
        }
    }
    
    private void UpdateMovement()
    {
        if (m_jumpTimer == 0) return;
        var pos = transform.position;
        var t = MathHelpers.Remap(m_jumpTimer, 0, m_jumpDuration / 2, 0, 1);
        pos.y = Mathf.Lerp(m_groundY, m_jumpHeight + m_groundY, t);
            
        transform.position = pos;
    }
    
    private void Jump()
    {
        StartCoroutine(OnJumping());
        UpdateAnimator();
    }
    
    private IEnumerator OnJumping()
    {
        while (m_jumpTimer < m_jumpDuration/2)
        {
            m_jumpTimer += Time.deltaTime * m_jumpPowerUp;
            yield return null;
        }

        while (m_airTimer < m_airTime)
        {
            m_airTimer += Time.deltaTime;
            yield return null;
        }

        m_airTimer = 0;

        while (m_jumpTimer > 0)
        {
            m_jumpTimer -= Time.deltaTime * m_jumpPowerDown;
            yield return null;
        }

        m_jumpTimer = 0;
    }
    
    private void UpdateAnimator()
    {
        m_animator.SetBool("Jumping",m_jumpTimer != 0);
    }
}
