using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceValue : MonoBehaviour
{
    //������ �÷��̾� �Ÿ��� ��Ÿ���� ���� �����̴�
    Slider slider;

    void Start()
    {
        //ĳ�� �� �ʱ�ȭ
        slider = GetComponent<Slider>();
        slider.value = 1;
    }

    void Update()
    {
        SetValue();
    }
    void SetValue()
    {
        //���� �Ŵ������� �Ÿ��� ������ value�� ��������
        float sliderValue =  1 - GameManager.Instance.PlayerBossDistance * 0.001f;
        slider.value = sliderValue;
    }
}
