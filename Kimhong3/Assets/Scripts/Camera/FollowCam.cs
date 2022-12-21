using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    //ī�޶� ��ġ�� ���� �� �� ����� ������Ʈ�� transform�� ������
    public Transform Campos;
    //������ ���� ������ bool
    bool isBossAttack;

    //������ ���� �� �� ��� �� ������ ���� ī�޶�
    [SerializeField]
    Camera subcam;

    //���콺�� ��ġ�� ���� ����
    Vector2 mouseVec;

    void Awake()
    {
        //Ŀ�� ��ġ ���
        Cursor.lockState = CursorLockMode.Locked;
        //������ ���� ī�޶� ���α�
        subcam.gameObject.SetActive(false);
    }
    void LateUpdate()
    {
        //������ �������� �ƴ϶�� ȭ���� ���콺�� ������ �� �ְ�
        if(!isBossAttack) SetRotation();
    }
    void SetRotation()
    {
        //GetAxis�� �̿��� ���콺 ��ġ�� �޾ƿ�
        mouseVec.x = Input.GetAxis("Mouse X");
        mouseVec.y = -Input.GetAxis("Mouse Y");

        //mouseVec�� ���̰� 0�� �ƴ϶��
        if(mouseVec.magnitude != 0)
        {
            //���ʹϾ� ���� ȸ���� �����صΰ�
            Quaternion q = Campos.rotation;
            //������ ���� ���콺 ���Ͱ��� ������
            q.eulerAngles = new Vector3(q.eulerAngles.x + mouseVec.y * 3f, q.eulerAngles.y + mouseVec.x * 3f, q.eulerAngles.z);
            //�̸� �ٽ� campPos�� ȸ�������� �������
            Campos.rotation = q;
        }
    }

    //������ �����ϴ� ������ ���� ������
    public void StartBossAttack()
    {
        StartCoroutine(bossAttack());
    }

    IEnumerator bossAttack()
    {
        //���� ������ true�� ����� ȭ�� ȸ���� �� �̻� �����ʰ�
        isBossAttack = true;
        //���� ����ϴ� ��ƼŬ ����(ȭ�� ���߼� ���� ��) ����
        ParticleSystem pa = GetComponentInChildren<ParticleSystem>();
        pa.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.1f);

        //������ ���� ī�޶� ������Ʈ ����
        subcam.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.8f);

        //���� ������ ī�޶� ������Ʈ ����
        subcam.gameObject.SetActive(false); 
        pa.gameObject.SetActive(true);
        isBossAttack = false;   
    }
}
