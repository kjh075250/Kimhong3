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

}
