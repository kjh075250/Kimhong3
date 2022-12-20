using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraEffect : MonoBehaviour
{
    //메인 카메라 담을 카메라 변수
    Camera cam;
    public void Start()
    {
        //메인 캠 캐싱
        cam = Camera.main;
    }
    public void Update()
    {
        CameraFOV();
    }
    public void CameraShake()
    {
        //트윈을 이용해 카메라 화면 흔들리는 연출
        cam.transform.DOShakePosition(0.3f, 1, 10, 90, false, true, ShakeRandomnessMode.Full);
    }
    //속도에 따라 FOV값 바꾸기
    public void CameraFOV()
    {
        //Clamp에 사용할 최대 fov값
        float maxFov = 54f;
        //만약 폭주 상태라면 최대값 70으로 높임
        if (GameManager.Instance.playerState == GameManager.PlayerState.overdrive)
        {
            maxFov = 70f;
        }
        //40부터 maxFOv값 까지 현재 속도를 받아와 조끔식 더해줌
        float fov = Mathf.Clamp(40 + GameManager.Instance.GetSpeed() * 0.05f, 40, maxFov);
        cam.fieldOfView = fov;
    }
}
