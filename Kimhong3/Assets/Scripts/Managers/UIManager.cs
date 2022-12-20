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
        //게이지 이미지를 게임매니저의 에너지게이지와 맞춰줌
        gageImage.fillAmount = GameManager.Instance.ThunderGage * 0.01f;
        //속도 텍스트를 게임매니저의 속도를 가져오는 함수를 이용해 맞춤
        speedText.text = Mathf.Floor(GameManager.Instance.GetSpeed()).ToString();
        //스피트 이미지를 속도를 이용해 맞춤

        speedImage.fillAmount = GameManager.Instance.GetSpeed() * 0.001f;
    }

    public void GameOverImage()
    {
        //죽었을 때 나오는 이미지오브젝트
        gameOverImage.gameObject.SetActive(true);
    }

}
