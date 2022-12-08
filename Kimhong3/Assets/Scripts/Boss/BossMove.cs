using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    Transform player;
    void Start()
    {
        player = GameManager.Instance.Player.transform;
    }

    void Update()
    {
        if (transform.position.z > 50)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + Time.deltaTime * (15f - GameManager.Instance.GetSpeed() * 0.03f));

        }
    }
}
