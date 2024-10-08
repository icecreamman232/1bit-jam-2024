using System;
using System.Collections;
using SGGames.Scripts.Managers;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class CameraZoom : Singleton<CameraZoom>
{
    [SerializeField] private Vector2 m_zoomInValue;
    [SerializeField] private Vector2 m_zoomOutValue;
    [SerializeField] private Vector3 m_lockCameraPos;
    
    private PixelPerfectCamera m_pixelPerfectCamera;
    private CameraFollowing m_cameraFollowing;
    private bool m_isZooming;

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
            ZoomOut();
        }
    }
    #endif

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
