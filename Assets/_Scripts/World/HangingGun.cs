using System;
using System.Collections;
using SGGames.Scripts.Managers;
using UnityEngine;

public class HangingGun : MonoBehaviour
{
    [SerializeField] private ObjectPooler m_bulletPooler;
    [SerializeField] private float m_delayBetween2Shot;
    [SerializeField] private Transform m_shootPivot;
    [SerializeField] private Vector2 m_shootDirection;
    
    private void Start()
    {
        Shoot();
    }
    
    private void Shoot()
    {
        var bulletGO = m_bulletPooler.GetPooledGameObject();
        var bullet = bulletGO.GetComponent<Projectile>();
        bullet.Spawn(m_shootPivot.transform.position,m_shootDirection);
        
        
        StartCoroutine(OnDelayAfterShoot());
    }

    private IEnumerator OnDelayAfterShoot()
    {
        yield return new WaitForSeconds(m_delayBetween2Shot);
        Shoot();
    }
}
