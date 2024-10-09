using System.Collections;
using SGGames.Scripts.Managers;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    [SerializeField] private float m_delayBetween2Shot;
    [SerializeField] private ObjectPooler m_bulletPooler;
    [SerializeField] private Transform m_shootPivot;
    [SerializeField] private Vector2 m_aimDirection = Vector2.right;
    
    private PlayerSoundBank m_soundBank;
    private bool m_isAllow;
    private bool m_isDelay;
    private Animator m_animator;
    private PlayerHorizontalMovement m_horizontalMovement;
    private PlayerHealth m_health;
    private void Start()
    {
        m_isAllow = true;
        m_soundBank = GetComponent<PlayerSoundBank>();
        m_health = GetComponent<PlayerHealth>();
        m_animator = GetComponentInChildren<Animator>();
        m_horizontalMovement = GetComponent<PlayerHorizontalMovement>();

        m_health.OnDeath += HandleGunOnPlayerDead;
        LevelManager.Instance.OnPauseChange += OnPauseGame;
    }
    

    private void OnDestroy()
    {
        m_health.OnDeath -= HandleGunOnPlayerDead;
        LevelManager.Instance.OnPauseChange -= OnPauseGame;
    }
    
    private void OnPauseGame(bool isPause)
    {
        m_isAllow = !isPause;
    }
    

    private void HandleGunOnPlayerDead()
    {
        m_isAllow = false;
    }

    private void Update()
    {
        if (!m_isAllow) return;
        
        if (Input.GetKeyDown(KeyCode.F))
        {
            var input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (input.x > 0)
            {
                m_aimDirection = Vector2.right;
                m_shootPivot.position = new Vector3(0.5f, 0.5f, 0f);
            }
            else if (input.x < 0)
            {
                m_aimDirection = Vector2.left;
                m_shootPivot.position = new Vector3(-0.5f, 0.5f, 0f);
            }
            
            if (input.y > 0)
            {
                m_aimDirection = Vector2.up;
                m_shootPivot.position = new Vector3(0, 1.5f, 0f);
            }
            else if (input.y < 0)
            {
                m_aimDirection = Vector2.down;
                m_shootPivot.position = new Vector3(0, -0.5f, 0f);
            }

            if (input.x == 0 && input.y == 0)
            {
                m_aimDirection = m_horizontalMovement.IsFlip ?Vector2.left : Vector2.right;
                m_shootPivot.position = new Vector3(m_horizontalMovement.IsFlip ? -0.5f : 0.5f, 0.5f, 0f);
            }
            
            Shoot();
        }
    }

    private void Shoot()
    {
        if (m_isDelay) return;
        
        m_soundBank.PlayShootSFX();
        
        var bulletGO = m_bulletPooler.GetPooledGameObject();
        var bullet = bulletGO.GetComponent<PlayerBullet>();
        bullet.Spawn(transform.position + m_shootPivot.transform.position, m_aimDirection);
        if (m_aimDirection.x != 0)
        {
            m_animator.SetTrigger("ShootHorizontal");
        }
        else
        {
            m_animator.SetTrigger("ShootVertical");
        }
        
        StartCoroutine(OnDelayBetween2Shot());
    }

    private IEnumerator OnDelayBetween2Shot()
    {
        m_isDelay = true;
        yield return new WaitForSeconds(m_delayBetween2Shot);
        m_isDelay = false;
    }
}
