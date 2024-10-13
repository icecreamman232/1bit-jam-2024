using SGGames.Scripts.Core;
using SGGames.Scripts.Managers;
using UnityEngine;

namespace SGGames.Scripts.Player
{
    public class PlayerLadder : PlayerBehavior
    {
        private CameraFollowing m_cameraFollowing;
        [SerializeField] private float m_climbingVelocity;

        private PlayerHealth m_health;
        private Animator m_animator;
        private Controller2D m_controller2D;
        private Ladder m_currentLadder;
        private bool m_isEnterFromBot;
        private bool m_isClimbing;
        public bool IsOnLadder => m_currentLadder != null;

        private int m_OnLadderAnimParam = Animator.StringToHash("OnLadder");
        private int m_IsClimbingAnimParam = Animator.StringToHash("Climbing");

        protected override void Start()
        {
            m_health = GetComponent<PlayerHealth>();
            m_controller2D = GetComponent<Controller2D>();
            m_animator = GetComponentInChildren<Animator>();
            m_cameraFollowing = Camera.main.GetComponent<CameraFollowing>();
            m_health.OnDeath += HandleClimbingOnDead;
            base.Start();
        }

        private void OnDestroy()
        {
            m_health.OnDeath -= HandleClimbingOnDead;
        }

        private void HandleClimbingOnDead()
        {
            m_currentLadder = null;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Interact")
                && other.CompareTag("Interact/Ladder"))
            {
                //Player was standing on ladder but didnt move or they just jump on ladder,
                //we disable gravity to keep them there
                if (m_controller2D.IsGrounded)
                {
                    m_controller2D.SetGravityActive(false);
                }
                m_controller2D.SetVerticalVelocity(0);
                m_currentLadder = other.GetComponent<Ladder>();
                //Determine enter direction
                if (transform.position.y <= m_currentLadder.transform.position.y)
                {
                    m_isEnterFromBot = true;
                }
                else
                {
                    m_isEnterFromBot = false;
                }
               
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (m_currentLadder!=null &&  other.gameObject == m_currentLadder.gameObject)
            {
                DetachFromLadder();
            }
        }

        private void Update()
        {
            if(m_currentLadder == null) return;
            m_isClimbing = false;
            m_controller2D.SetVerticalVelocity(0);
            if (Input.GetKey(KeyCode.UpArrow))
            {
                m_controller2D.SetVerticalVelocity(m_climbingVelocity);
                m_isEnterFromBot = true;
                m_isClimbing = true;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                m_controller2D.SetVerticalVelocity(-m_climbingVelocity);
                m_isEnterFromBot = false;
                m_isClimbing = true;
            }

            if (m_controller2D.Velocity.y != 0 && m_currentLadder!= null
                && m_isClimbing)
            {
                m_controller2D.SetGravityActive(false);
                m_currentLadder.gameObject.layer = LayerMask.NameToLayer("Default");
                m_animator.SetBool(m_OnLadderAnimParam,true);
            }

            //Snap player to edge position upon exiting ladder from top
            if (m_controller2D.Velocity.y != 0 && m_isEnterFromBot)
            {
                var playerHeadPos = transform.position + Vector3.up;
                if (playerHeadPos.y >= m_currentLadder.TopPoint.position.y)
                {
                    m_animator.SetBool(m_OnLadderAnimParam,false);
                    transform.position = m_controller2D.Velocity.x < 0
                        ? m_currentLadder.LeftPoint.position
                        : m_currentLadder.RightPoint.position;
                    m_cameraFollowing.ResetSmoothValue();
                    //Player is near the top
                    DetachFromLadder();
                }
            }

            UpdateAnimator();
        }

        private void AttachToLadder(GameObject ladder)
        {
            m_currentLadder = ladder.GetComponent<Ladder>();
            m_controller2D.SetGravityActive(false);
            m_controller2D.SetClimbing(true);
            
            //Determine enter direction
            if (transform.position.y <= m_currentLadder.transform.position.y)
            {
                m_isEnterFromBot = true;
            }
            else
            {
                m_isEnterFromBot = false;
            }
        }

        private void DetachFromLadder()
        {
            m_currentLadder.gameObject.layer = LayerMask.NameToLayer("Interact");
            m_currentLadder = null;
            m_controller2D.SetGravityActive(true);
            m_controller2D.SetClimbing(false);
            m_animator.SetBool(m_OnLadderAnimParam,false);
        }

        private void UpdateAnimator()
        {
            m_animator.SetBool(m_IsClimbingAnimParam,m_controller2D.Velocity.y != 0 && m_isClimbing);
        }
    }
}
