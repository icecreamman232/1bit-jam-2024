using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private bool m_isAllow;
    [SerializeField] private float m_moveSpeed;
    [SerializeField] private bool m_isFlip;
    [SerializeField] private Vector2 m_moveDirection;
    [Header("Animation")]
    [SerializeField] private SpriteRenderer m_spriteRenderer;
    [SerializeField] private Animator m_animator;
    
    private float m_jumpTimer;
    
    public void ToggleAllow(bool value)
    {
        m_isAllow = value;
    }
    
    private void Start()
    {
        m_isAllow = true;
    }
    
    private void Update()
    {
        if (!m_isAllow) return;
        
        HandleInput();
        UpdateMovement();
        Flip();
        UpdateAnimator();
    }

    private void HandleInput()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            m_moveDirection = Vector2.right;
            m_isFlip = false;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            m_moveDirection = Vector2.left;
            m_isFlip = true;
        }
        else
        {
            m_moveDirection = Vector2.zero;
        }
    }
    
    private void Flip()
    {
        m_spriteRenderer.flipX = m_isFlip;
    }
    
    private void UpdateMovement()
    {
        transform.Translate( m_moveDirection * (Time.deltaTime * m_moveSpeed));
    }
    
    
    private void UpdateAnimator()
    {
        m_animator.SetBool("Running",m_moveDirection!= Vector2.zero);
    }
}
