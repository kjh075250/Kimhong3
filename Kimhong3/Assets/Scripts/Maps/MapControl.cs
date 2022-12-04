using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MapControl : MonoBehaviour
{
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 moveVec = new Vector3(0, 0, Input.GetAxis("Vertical") * 30f);

        rb.AddForce(-moveVec, ForceMode.Acceleration);
        rb.velocity = new Vector3(rb.velocity.x,rb.velocity.y, Mathf.Clamp(rb.velocity.z, -800, 0));
        if (transform.position.z <= -90)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, GameManager.Instance.LastMapObject.transform.position.z + 150) ;
            GameManager.Instance.LastMapObject = this.gameObject;
        }
    }
}
