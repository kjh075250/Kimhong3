using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DroneBossIntro : MonoBehaviour
{
    void Start()
    {
    }


    void Update()
    {
        transform.LookAt(GameManager.Instance.Player.transform);
    }
}
