using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraEffect : MonoBehaviour
{
    Camera cam;
    public void Start()
    {
        cam = Camera.main;
    }
    public void Update()
    {
        CameraFOV();
    }
    public void CameraShake()
    {
        cam.transform.DOShakePosition(0.3f, 1, 10, 90, false, true, ShakeRandomnessMode.Full);
    }
    public void CameraFOV()
    {
        float maxFov = 54f;
        if (GameManager.Instance.playerState == GameManager.PlayerState.overdrive)
        {
            maxFov = 70f;
        }
        float fov = Mathf.Clamp(40 + GameManager.Instance.GetSpeed() * 0.05f, 40, maxFov);
        cam.fieldOfView = fov;
    }
}
