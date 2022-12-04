using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DroneBossIntro : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(BossIntroMove());
    }

    IEnumerator BossIntroMove()
    {
        Vector3 introVec = new Vector3(transform.position.x, transform.position.y, 500);
        Tween tween = transform.DOMove(introVec, 4f, false);
        yield return tween.WaitForCompletion();
        introVec = new Vector3(transform.position.x, 30f, 120f);
    }
    void Update()
    {
        transform.LookAt(GameManager.Instance.Player.transform);
    }
}
