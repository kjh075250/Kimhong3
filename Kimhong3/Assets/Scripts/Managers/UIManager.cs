using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //�̹��� �ν����Ϳ��� �޾ƿ���
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
        //������ �̹����� ���ӸŴ����� �������������� ������
        gageImage.fillAmount = GameManager.Instance.ThunderGage * 0.01f;
        //�ӵ� �ؽ�Ʈ�� ���ӸŴ����� �ӵ��� �������� �Լ��� �̿��� ����
        speedText.text = Mathf.Floor(GameManager.Instance.GetSpeed()).ToString();
        //����Ʈ �̹����� �ӵ��� �̿��� ����
        speedImage.fillAmount = GameManager.Instance.GetSpeed() * 0.001f;
    }

    public void GameOverImage()
    {
        //�׾��� �� ������ �̹���������Ʈ
        gameOverImage.gameObject.SetActive(true);
    }
    public void GameClearImage()
    {
        //Ŭ���� ���� �� ������ �̹���������Ʈ
        gameClearImage.gameObject.SetActive(true);
    }
    public void GageTextActive(bool active)
    {
        //F ������� �ؽ�Ʈ ����
        gageText.gameObject.SetActive(active);
    }
}
