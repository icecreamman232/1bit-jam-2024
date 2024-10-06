using SGGames.Scripts.Managers;
using UnityEngine;

public class DamageHandler : MonoBehaviour
{
    [SerializeField] protected LayerMask m_targetMask;
    [SerializeField] protected int m_damage;
    [SerializeField] protected float m_invulnerableTime;
    [SerializeField] protected bool m_isApplyForceOnHit;
    [SerializeField] protected Vector2 m_forceApplyOnHit;

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (LayerManager.IsInLayerMask(other.gameObject.layer, m_targetMask))
        {
            CauseDamage(other.gameObject);
        }
    }

    protected virtual void CauseDamage(GameObject target)
    {
        var health = target.GetComponent<Health>();
        if (health is PlayerHealth)
        {
            health.TakeDamage(m_damage,this.gameObject,m_invulnerableTime);
            if (m_isApplyForceOnHit)
            {
                var controller = target.GetComponent<Controller2D>();
                ApplyForceOnHit(controller);
            }
        }
    }

    private void ApplyForceOnHit(Controller2D controller2D)
    {
        var forceToApply = Vector3.zero;
        //Player is on the left
        if (controller2D.transform.position.x < transform.position.x)
        {
            forceToApply = m_forceApplyOnHit;
            forceToApply.x *= -1;
        }
        
        controller2D.SetVelocity(forceToApply);
    }
}
