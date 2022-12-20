using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraEffect : MonoBehaviour
{
    //���� ī�޶� ���� ī�޶� ����
    Camera cam;
    public void Start()
    {
        //���� ķ ĳ��
        cam = Camera.main;
    }
    public void Update()
    {
        CameraFOV();
    }
    public void CameraShake()
    {
        //Ʈ���� �̿��� ī�޶� ȭ�� ��鸮�� ����
        cam.transform.DOShakePosition(0.3f, 1, 10, 90, false, true, ShakeRandomnessMode.Full);
    }
    //�ӵ��� ���� FOV�� �ٲٱ�
    public void CameraFOV()
    {
        //Clamp�� ����� �ִ� fov��
        float maxFov = 54f;
        //���� ���� ���¶�� �ִ밪 70���� ����
        if (GameManager.Instance.playerState == GameManager.PlayerState.overdrive)
        {
            maxFov = 70f;
        }
        //40���� maxFOv�� ���� ���� �ӵ��� �޾ƿ� ������ ������
        float fov = Mathf.Clamp(40 + GameManager.Instance.GetSpeed() * 0.05f, 40, maxFov);
        cam.fieldOfView = fov;
    }
}
