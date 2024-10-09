using System;
using System.Collections;
using SGGames.Scripts.Managers;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using Random = UnityEngine.Random;

public class CameraController : Singleton<CameraController>
{
    [Header("Zoom")]
    [SerializeField] private Vector2 m_zoomInValue;
    [SerializeField] private Vector2 m_zoomOutValue;
    [SerializeField] private Vector3 m_lockCameraPos;
    [Header("Shake")]
    [SerializeField] private float m_shakeDuration;
    [SerializeField] private float m_shakePower;
    [SerializeField] private float m_shakeFrequency;
    
    private PixelPerfectCamera m_pixelPerfectCamera;
    private CameraFollowing m_cameraFollowing;
    private bool m_isZooming;
    private bool m_isShaking;

    private void Start()
    {
        m_pixelPerfectCamera = GetComponent<PixelPerfectCamera>();
        m_cameraFollowing = GetComponent<CameraFollowing>();
    }

    #if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Shake();
        }
    }
    #endif

    public void Shake()
    {
        StartCoroutine(OnShake());
    }

    private IEnumerator OnShake()
    {
        if (m_isShaking)
        {
            yield break;
        }

        m_isShaking = true;
        
        var startPos = m_pixelPerfectCamera.transform.position;
        var duration = m_shakeDuration;
        while (duration > 0)
        {
            Vector3 randomPos = Random.insideUnitCircle * m_shakePower;
            randomPos.z = -10;
            m_pixelPerfectCamera.transform.position = startPos + randomPos;
            yield return new WaitForSeconds(m_shakeFrequency);
            duration -= m_shakeFrequency;
        }

        m_pixelPerfectCamera.transform.position = startPos;
        m_isShaking = false;
    }
    

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
