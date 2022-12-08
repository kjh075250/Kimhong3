using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BulletMove : MonoBehaviour
{
    private Vector3 plaPos;

    private void Start()
    {
        plaPos = GameManager.Instance.Player.transform.position;
        transform.LookAt(plaPos);
    }

    void Update()
    {
        //transform.position = Vector3.MoveTowards(transform.position, plaPos, 0.2f);
        transform.DOMove(plaPos, 0.6f);
        if (transform.position == plaPos) Destroy(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player")) Destroy(this);
    }
}
