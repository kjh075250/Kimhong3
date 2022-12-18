using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{

    void Update()
    {
        if (transform.position.z > 30)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + Time.deltaTime * (60f - GameManager.Instance.GetSpeed() * 0.1f));
        }
    }
}
