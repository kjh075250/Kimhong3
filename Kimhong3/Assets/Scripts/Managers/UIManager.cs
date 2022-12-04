using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    Text speedText;

    [SerializeField]
    Image gageImage;
    void Start()
    {

    }

    void Update()
    {
        gageImage.fillAmount = GameManager.Instance.ThunderGage * 0.01f;
        speedText.text = "현재 속도 : " + GameManager.Instance.GetSpeed();
    }
}
