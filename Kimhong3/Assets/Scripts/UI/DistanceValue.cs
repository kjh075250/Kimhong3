using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceValue : MonoBehaviour
{
    Slider slider;
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = 1;
    }

    void Update()
    {
        SetValue();
    }
    void SetValue()
    {
        float sliderValue =  1 -GameManager.Instance.PlayerBossDistance * 0.001f;
        Debug.Log(sliderValue);
        slider.value = sliderValue;
    }
}
