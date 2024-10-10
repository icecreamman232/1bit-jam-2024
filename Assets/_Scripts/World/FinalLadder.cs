using System;
using System.Collections;
using SGGames.Scripts.Managers;
using UnityEngine;

public class FinalLadder : MonoBehaviour
{
    [SerializeField] private float m_timerForLadderDown;
    [SerializeField] private SpriteRenderer m_spriteRenderer;
    [SerializeField] private Vector3 m_destination;
    
    public void TriggerFinalLadder()
    {
        StartCoroutine(OnTrigger());
    }

    private IEnumerator OnTrigger()
    {
        LevelManager.Instance.BlockPlayerInput();

        yield return new WaitForSeconds(1.5f);
        
        var timer = .0f;
        var originalPos = transform.position;
        while (timer < m_timerForLadderDown)
        {
            timer += Time.deltaTime;
            transform.position = Vector3.Lerp(originalPos, m_destination, MathHelpers.Remap(timer, 0, m_timerForLadderDown, 0, 1));
            yield return null;
        }

        transform.position = m_destination;
        LevelManager.Instance.UnlockPlayerInput();
    }
}
