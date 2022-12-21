using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    //���� ������Ʈ�� �ڷ� ��� �����̰� �ϴ� �ڵ�
    void Update()
    {
        //���� ��ġ�� 30���� Ŭ��(�÷��̾�� ������ ���� ������)
        if (transform.position.z > 30)
        {
            //�÷��̾� �ӵ��� �ݺ���� �ڷ� ���� �ӵ��� ��������.
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + Time.deltaTime * (60f - GameManager.Instance.GetSpeed() * 0.1f));
        }
    }
}
