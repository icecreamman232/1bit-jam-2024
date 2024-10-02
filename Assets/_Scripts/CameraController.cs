using System;
using System.Collections;
using SGGames.Scripts.Managers;
using SGGames.Scripts.ScriptableEvent;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private FloatEvent m_moveUpEvent;

    private void Start()
    {
        m_moveUpEvent.AddListener(MoveUp);
    }

    private void OnDestroy()
    {
        m_moveUpEvent.RemoveListener(MoveUp);
    }

    private void MoveUp(float y)
    {
        StartCoroutine(OnMoveUp(y));
    }

    private IEnumerator OnMoveUp(float y)
    {
        var timer = 0.0f;
        var pos = transform.position;
        var originalY = pos.y;
        
        while (timer < 0.5f)
        {
            timer += Time.deltaTime;
            pos.y = Mathf.Lerp(originalY, y, MathHelpers.Remap(timer, 0, 0.5f, 0, 1));
            transform.position = pos;
            yield return null;
        }
    }
}
