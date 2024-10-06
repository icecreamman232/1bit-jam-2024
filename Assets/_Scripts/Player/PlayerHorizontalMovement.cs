using System;
using System.Collections;
using SGGames.Scripts.Managers;
using UnityEngine;

public class PlayerHorizontalMovement : MonoBehaviour
{
    [SerializeField] private float m_moveSpeed;
    [SerializeField] private CameraFollowing m_cameraFollowing;
    
    private readonly int m_runningAnimParam = Animator.StringToHash("Running");

    private PlayerHealth m_health;
    private Animator m_animator;
    private SpriteRenderer m_spriteRenderer;
    private Controller2D m_controller2D;
    private float m_velocityXSmoothing;
    private Vector2 m_rawInputValue;
    private bool m_isFlip;
    private bool m_isAllow;

    public bool IsFlip => m_isFlip;
    
    private void Start()
    {
        m_isAllow = true;
        m_health = GetComponent<PlayerHealth>();
        m_spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        m_animator = GetComponentInChildren<Animator>();
        m_controller2D = GetComponent<Controller2D>();
        m_cameraFollowing.SetCameraPosition(transform.position);

        m_health.OnDeath += HandleHorizontalMovementOnDead;
    }

    private void OnDestroy()
    {
        m_health.OnDeath -= HandleHorizontalMovementOnDead;
    }

    private void HandleHorizontalMovementOnDead()
    {
        m_controller2D.SetGravityActive(false);
        m_isAllow = false;
    }

    private void Update()
    {
        if (!m_isAllow) return;
        m_rawInputValue = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (m_rawInputValue.x == 0)
        {
            m_controller2D.SetHorizontalVelocity(0);
        }
        else
        {
            //Smooth horizontal movement
            float targetVelocityX = m_rawInputValue.x * m_moveSpeed;
            m_controller2D.SetHorizontalVelocity(Mathf.SmoothDamp(
                m_controller2D.Velocity.x, targetVelocityX,
                ref m_velocityXSmoothing, 0.1f));
            
            //Flip character sprite if go to the left
            Flip(m_controller2D.Velocity.x < 0);
        }

        UpdateAnimator();
    }

    private void Flip(bool isFlip)
    {
        m_isFlip = isFlip;
        m_spriteRenderer.flipX = isFlip;
        m_cameraFollowing.Flip(isFlip);
    }

    private void UpdateAnimator()
    {
        m_animator.SetBool(m_runningAnimParam,m_controller2D.Velocity.x != 0);
    }

    private IEnumerator OnStopCameraForDuration(float duration)
    {
        m_cameraFollowing.SetPermission(false);
        yield return new WaitForSeconds(duration);
        m_cameraFollowing.SetPermission(true);
        m_cameraFollowing.ResetSmoothValue();
    }
    
    public void StopCameraForDuration(float duration)
    {
        StartCoroutine(OnStopCameraForDuration(duration));
    }

    public void ToggleCamera(bool value)
    {
        m_cameraFollowing.SetPermission(value);
    }
}
