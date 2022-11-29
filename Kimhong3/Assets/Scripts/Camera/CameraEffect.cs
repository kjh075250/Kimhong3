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
}
