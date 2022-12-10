using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FollowObject : MonoBehaviour
{
    public Transform trans;

    void LateUpdate()
    {
        transform.DOMoveX(trans.position.x,0.3f);
        transform.DOMoveY(trans.position.y,0.5f);
        transform.DOMoveZ(trans.position.z,2f);
    }
}
