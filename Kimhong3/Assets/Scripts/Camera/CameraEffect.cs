using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraEffect : MonoBehaviour
{
    public void CameraShake()
    {
        transform.DOShakePosition(0.3f, 1, 10, 90, false, true, ShakeRandomnessMode.Full);
    }
    private void Update()
    {
        CameraZoomOut();
    }
    public void CameraZoomOut()
    {
        float a = Mathf.Clamp(40f + (GameManager.Instance.GetSpeed() * 0.05f),40,55);
        Camera.main.DOFieldOfView(a, 0.2f);
    }
}
