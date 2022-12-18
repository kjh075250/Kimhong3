using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FollowObject : MonoBehaviour
{
    public Transform trans;
    Tweener tweener;
    Sequence seq;
    Vector3 lastVec;
    void LateUpdate()
    {
        Vector3 smoothVec = Vector3.SmoothDamp(transform.position, trans.position, ref lastVec, .3f);
        transform.position = smoothVec;
    }

}
