using System;
using UnityEngine;

public class CheckPointZone : MonoBehaviour
{
    [SerializeField] private int m_checkPointIndex;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            LevelManager.Instance.SaveCheckPoint(m_checkPointIndex);
        }
    }
}
