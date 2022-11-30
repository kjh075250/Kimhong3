using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody rigidbody;

    public Transform dashTarget;
    Vector3 dashTargetPos;

    public bool isDashing = false;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.centerOfMass = new Vector3(0, -1, 0);
    }
    public void FixedUpdate()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Dash();
        }
    }
    void Move()
    {
       
        Vector3 horVec = new Vector3(Input.GetAxisRaw("Horizontal") * 10f, transform.position.y, transform.position.z);
        Vector3 rotateVec = new Vector3(0, 0, Input.GetAxisRaw("Horizontal") * 30f);
        transform.DOMoveX(transform.position.x + horVec.x, 1f, false);
        transform.DORotate(-rotateVec, 0.7f);
    }

    void Dash()
    {
        if (isDashing) return;
        dashTargetPos = dashTarget.position;
        StartCoroutine(Dashing());
    }

    IEnumerator Dashing()
    {
        isDashing = true;
        transform.DOMoveY(transform.position.y + 2f, 0.5f, false);
        transform.DOLookAt(dashTargetPos, 0.7f, AxisConstraint.None);
        rigidbody.velocity = rigidbody.velocity * 0.5f;
        yield return new WaitForSecondsRealtime(0.7f);

        rigidbody.freezeRotation = true;
        rigidbody.AddForce(transform.forward * 100000f, ForceMode.Impulse);
        yield return new WaitForSecondsRealtime(0.2f);

        rigidbody.freezeRotation = false;
        rigidbody.velocity = rigidbody.velocity * 0.1f;
        isDashing = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isDashing) return;
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Hit");
        }
    }
}

