using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    Text speedText;
    [SerializeField]
    Image speedImage;
    [SerializeField]
    Image gageImage;
    [SerializeField]
    Image gameOverImage;

    void Start()
    {

    }

    void Update()
    {
        gageImage.fillAmount = GameManager.Instance.ThunderGage * 0.01f;
        speedText.text = GameManager.Instance.GetSpeed().ToString();
        speedImage.fillAmount = GameManager.Instance.GetSpeed() * 0.001f;
    }

    public void GameOverImage()
    {
        gameOverImage.gameObject.SetActive(true);
    }

}
