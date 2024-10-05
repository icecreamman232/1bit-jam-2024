using SGGames.Scripts.Managers;
using UnityEngine;

public class DamageHandler : MonoBehaviour
{
    [SerializeField] protected LayerMask m_targetMask;
    [SerializeField] protected int m_damage;
    [SerializeField] protected float m_invulnerableTime;

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
        }
    }
}
