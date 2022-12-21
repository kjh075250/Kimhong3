using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossObstacle : MonoBehaviour
{
    //���� ���Ͽ� ���Ǵ� ��ֹ��� ���� �����͵� so�� �޾ƿ�
    BossObsSO bossObsSO;

    //�Ž� ������, �ݶ��̴�, ������Ʈ���� �޾ƿ�
    MeshRenderer mesh;
    Collider col;
    public GameObject go;

    void Awake()
    {
        //������Ʈ ĳ��
        mesh = GetComponent<MeshRenderer>();
        col = GetComponent<Collider>();
        //���ӸŴ����� �ִ� so�����͸� ������
        bossObsSO = GameManager.Instance.obsSO;
        go.SetActive(false);
    }
    void Start()
    {
        //���� ������Ʈ�� ���͸����� �غ� ���͸���� �������
        mesh.material = bossObsSO.ReadyMat;
        //������ �����ϱ� ���� ��� ���ش�
        mesh.enabled = false;
        col.enabled = false;
    }
    //�������� ����Ǵ� ��ֹ� ��ȯ �ڵ�
    public void StartPatternCo()
    {
        StartCoroutine(StartPattern());
    }
     IEnumerator StartPattern()
    {
        //��ֹ� ������Ʈ�� �Ž� �������� ���ӿ�����Ʈ�� ���ְ�
        mesh.enabled = true;
        go.SetActive(true);
        yield return new WaitForSeconds(2f);
        //2�� �ڿ� �ݶ��̴��� ���ָ� �Ž� �������� ���͸����� �������� ���͸��󿡼� �ƿ� ������ ���͸���� �ٲ���
        mesh.material = bossObsSO.AttackMat;
        col.enabled = true;
        //ī�޶� ��鸮�� ������ �̺�Ʈ ȣ��
        GameManager.Instance.cameraShake.Invoke();
        yield return new WaitForSeconds(0.5f);
        //0.5�� �� ���ӿ�����Ʈ�� �Ž�, �ݶ��̴� ��� ����
        go.SetActive(false);
        mesh.material = bossObsSO.ReadyMat;
        mesh.enabled = false;
        col.enabled = false;
    }
}
