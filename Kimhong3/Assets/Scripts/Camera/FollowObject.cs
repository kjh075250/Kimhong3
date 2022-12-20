using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FollowObject : MonoBehaviour
{
    //플레이어 부드럽게 따라가는 클래스
    //카메라 오브젝트에 직접 붙이니 화면 흔들림 효과랑 겹쳐서 화면이 흔들리지 않아 따로 뺐습니다

    //목표 오브젝트의 위치
    [SerializeField]
    Transform trans;

    Vector3 lastVec;

    void LateUpdate()
    {
        //Vector3 smoothDamp를 이용해 현재 위치와 목표 위치를 이용해 damping
        Vector3 smoothVec = Vector3.SmoothDamp(transform.position, trans.position, ref lastVec, .3f);
        transform.position = smoothVec;
    }

}
