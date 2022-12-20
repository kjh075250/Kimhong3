using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FollowObject : MonoBehaviour
{
    //�÷��̾� �ε巴�� ���󰡴� Ŭ����
    //ī�޶� ������Ʈ�� ���� ���̴� ȭ�� ��鸲 ȿ���� ���ļ� ȭ���� ��鸮�� �ʾ� ���� �����ϴ�

    //��ǥ ������Ʈ�� ��ġ
    [SerializeField]
    Transform trans;

    Vector3 lastVec;

    void LateUpdate()
    {
        //Vector3 smoothDamp�� �̿��� ���� ��ġ�� ��ǥ ��ġ�� �̿��� damping
        Vector3 smoothVec = Vector3.SmoothDamp(transform.position, trans.position, ref lastVec, .3f);
        transform.position = smoothVec;
    }

}
