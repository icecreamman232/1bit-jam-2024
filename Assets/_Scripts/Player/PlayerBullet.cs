using System;
using System.Collections;
using SGGames.Scripts.Managers;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private float m_speed;
    [SerializeField] private SpriteRenderer m_spriteRenderer;
    [SerializeField] private LayerMask m_obstacleMask;
    [SerializeField] private DamageHandler m_damageHandler;
    
    [SerializeField] private Animator m_animator;

    private int m_explodeAnimParam = Animator.StringToHash("Explode");
    private Vector2 m_direction;
    private bool m_isAlive;

    private void OnEnable()
    {
        m_spriteRenderer.enabled = true;
        m_animator.Play("Idle");
        m_damageHandler.OnHit += OnHitTarget;
    }

    private void OnDisable()
    {
        m_damageHandler.OnHit -= OnHitTarget;
    }

    private void OnHitTarget(GameObject target)
    {
        StartCoroutine(OnDestroying());
    }

    private IEnumerator OnDestroying()
    {
        m_isAlive = false;
        m_spriteRenderer.enabled = false;
        m_animator.SetTrigger(m_explodeAnimParam);
        yield return new WaitForSeconds(0.35f);
        SelfDestroy();
    }

    public void Spawn(Vector3 position, Vector2 shootDirection)
    {
        transform.position = position;
        m_spriteRenderer.transform.rotation = Quaternion.identity;
        m_direction = shootDirection;
        if (m_direction == Vector2.up || m_direction == Vector2.down)
        {
            m_spriteRenderer.transform.rotation = Quaternion.AngleAxis(90,Vector3.forward);
        }

        RotateExplodeParticle();
        m_isAlive = true;
    }

    private void RotateExplodeParticle()
    {
        if (m_direction == Vector2.left)
        {
            m_animator.gameObject.transform.rotation = Quaternion.AngleAxis(90, Vector3.forward);
            return;
        }
        
        if (m_direction == Vector2.right)
        {
            m_animator.gameObject.transform.rotation = Quaternion.AngleAxis(-90, Vector3.forward);
            return;
        }
        
        if (m_direction == Vector2.up)
        {
            m_animator.gameObject.transform.rotation = Quaternion.AngleAxis(0, Vector3.forward);
            return;
        }
        
        if (m_direction == Vector2.down)
        {
            m_animator.gameObject.transform.rotation = Quaternion.AngleAxis(180, Vector3.forward);
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (LayerManager.IsInLayerMask(other.gameObject.layer, m_obstacleMask))
        {
            StartCoroutine(OnDestroying());
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
