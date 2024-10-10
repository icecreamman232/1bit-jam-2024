using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private float m_jumpHeight;
    [SerializeField] private float m_timeToJumpPeak;
    [SerializeField] private float m_jumpVelocity;

    private PlayerSoundBank m_soundBank;
    private Controller2D m_controller2D;
    private Animator m_animator;

    private int m_jumpAnimParam = Animator.StringToHash("Jumping");
    private bool m_isAllow;
    
    private void Start()
    {
        m_isAllow = true;
        m_animator = GetComponentInChildren<Animator>();
        m_controller2D = GetComponent<Controller2D>();
        m_soundBank = GetComponent<PlayerSoundBank>();
        ComputeJumpParams();
    }

    private void Update()
    {
        if (!m_isAllow)
        {
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && m_controller2D.CollisionInfos.CollideBelow)
        {
            m_controller2D.SetVerticalVelocity(m_jumpVelocity);
            m_soundBank.PlayJumpSFX();
        }

        UpdateAnimator();
    }

    private void ComputeJumpParams()
    {
        m_timeToJumpPeak = Mathf.Sqrt(2 * m_jumpHeight / Mathf.Abs(m_controller2D.Gravity));
        m_jumpVelocity = Mathf.Abs(m_controller2D.Gravity) * m_timeToJumpPeak;
    }

    private void UpdateAnimator()
    {
        m_animator.SetBool(m_jumpAnimParam,m_controller2D.Velocity.y != 0 && m_controller2D.IsGravityActive);
    }

    public void ToggleAllow(bool value)
    {
        m_isAllow = value;
    }
}
