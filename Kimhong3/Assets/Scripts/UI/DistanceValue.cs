using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceValue : MonoBehaviour
{
    //보스와 플레이어 거리를 나타내기 위한 슬라이더
    Slider slider;

    void Start()
    {
        //캐싱 후 초기화
        slider = GetComponent<Slider>();
        slider.value = 1;
    }

    void Update()
    {
        SetValue();
    }
    void SetValue()
    {
        //게임 매니저에서 거리를 가져와 value에 적용해줌
        float sliderValue =  1 - GameManager.Instance.PlayerBossDistance * 0.001f;
        slider.value = sliderValue;
    }
}
