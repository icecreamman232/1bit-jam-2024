using System;
using System.Collections;
using SGGames.Scripts.Data;
using SGGames.Scripts.Managers;
using SGGames.Scripts.ScriptableEvent;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using Random = UnityEngine.Random;

public class CameraController : MonoBehaviour
{
    [Header("Zoom")]
    [SerializeField] private Vector2 m_zoomInValue;
    [SerializeField] private Vector2 m_zoomOutValue;
    [SerializeField] private Vector3 m_lockCameraPos;
    [Header("Shake")]
    [SerializeField] private ShakeEvent m_shakeEvent;
    
    private PixelPerfectCamera m_pixelPerfectCamera;
    private CameraFollowing m_cameraFollowing;
    private bool m_isZooming;
    private bool m_isShaking;

    private void Start()
    {
        m_pixelPerfectCamera = GetComponent<PixelPerfectCamera>();
        m_cameraFollowing = GetComponent<CameraFollowing>();
        
        m_shakeEvent.AddListener(OnReceiveShakeEvent);
    }

    private void OnDestroy()
    {
        m_shakeEvent.RemoveListener(OnReceiveShakeEvent);
    }

    #region Shaking
    private void OnReceiveShakeEvent(ShakeProfile profile)
    {
        StartCoroutine(OnShake(profile));
    }

    private IEnumerator OnShake(ShakeProfile profile)
    {
        if (m_isShaking)
        {
            yield break;
        }

        m_isShaking = true;
        
        var startPos = m_pixelPerfectCamera.transform.position;
        var duration = profile.Duration;
        while (duration > 0)
        {
            Vector3 randomPos = Random.insideUnitCircle * profile.Power;
            randomPos.z = -10;
            m_pixelPerfectCamera.transform.position = startPos + randomPos;
            yield return new WaitForSeconds(profile.Frequency);
            duration -= profile.Frequency;
        }

        m_pixelPerfectCamera.transform.position = startPos;
        m_isShaking = false;
    }
    
    #endregion

    public void ZoomOut()
    {
        if (m_isZooming) return;
        m_cameraFollowing.SetPermission(false);
        StartCoroutine(OnZoomingOut());
    }

    private IEnumerator OnZoomingOut()
    {
        LevelManager.Instance.FreezePlayer();
        ScreenFader.Instance.FadeOut();
        yield return new WaitForSeconds(0.5f);
        
        m_cameraFollowing.SetPermission(false);//Not following player
        m_cameraFollowing.SetCameraPosition(m_lockCameraPos);
        
        m_pixelPerfectCamera.refResolutionX = (int)m_zoomOutValue.x;
        m_pixelPerfectCamera.refResolutionY = (int)m_zoomOutValue.y;
        ScreenFader.Instance.FadeIn();
        yield return new WaitForSeconds(0.5f);
        LevelManager.Instance.UnfreezePlayer();
    }
}
