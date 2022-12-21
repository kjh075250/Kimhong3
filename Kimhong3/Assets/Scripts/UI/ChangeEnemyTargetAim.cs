using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeEnemyTargetAim : MonoBehaviour
{
    //���� �̹���
    Image myImage;
    //Ÿ�� ���� �̹��� so
    public AimpointImageSO aimpoint;
    //���� ���� ����Ʈ�� ����ִ°�
    public bool IsOnAim;

    private void Start()
    {
        //�̹��� ĳ�� �� sprite�� so �� default�� ����
        myImage = GetComponent<Image>();
        myImage.sprite = aimpoint.TargetImageDefault;
    }

    private void FixedUpdate()
    {
        //�̹����� �ݶ��̴��� ���� ����Ʈ�� ������� �ʴٸ� default��
        myImage.sprite = aimpoint.TargetImageDefault;
    }

    public void ChangeAimImage()
    {
        //���� ����Ʈ�� ����ִٸ� OnAim sprite�� �ٲ���
        myImage.sprite = aimpoint.TargetImageOnAim;
    }
}
