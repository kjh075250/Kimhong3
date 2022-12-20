using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //이미지 인스펙터에서 받아오기
    [SerializeField]
    Text speedText;
    [SerializeField]
    Text gageText;
    [SerializeField]
    Image speedImage;
    [SerializeField]
    Image gageImage;
    [SerializeField]
    Image gameOverImage;
    [SerializeField]
    Image gameClearImage;

    private void Start()
    {
        gageText.color = Color.black;
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
    public void GameClearImage()
    {
        //클리어 했을 때 나오는 이미지오브젝트
        gameClearImage.gameObject.SetActive(true);
    }
    public void GageTextActive(bool active)
    {
        //F 누르라는 텍스트 띄우기
        gageText.gameObject.SetActive(active);
    }
}
