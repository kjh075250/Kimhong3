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
        InvokeRepeating("Die", 0.5f, 5f);
    }

    void Update()
    {
        transform.DOMove(plaPos, 0.6f);
        //transform.position = Vector3.MoveTowards(transform.position, plaPos, 0.7f);
    }

    void Die()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Die();
    }
}
