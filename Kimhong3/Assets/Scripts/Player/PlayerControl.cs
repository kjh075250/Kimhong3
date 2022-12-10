using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class PlayerControl : MonoBehaviour
{

    private Rigidbody rigidbody;
    WaitForSeconds wait;
    Vector3 dashTargetPos;
    protected bool isDashing = false;
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        wait = new WaitForSeconds(2f);
    }

    void Move()
    {
        Vector3 horVec = new Vector3(Input.GetAxis("Horizontal") * 15f, transform.position.y, transform.position.z);
        Vector3 rotateVec = new Vector3(0, 0, Input.GetAxisRaw("Horizontal") * 30f);
        transform.Translate(horVec * Time.deltaTime * 2.5f);
        transform.DORotate(-rotateVec, 0.7f);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, 100f, LayerMask.GetMask("Ground")))
        {
            float PlayerY = hit.point.y + 2.5f;
            if (GameManager.Instance.playerState == GameManager.PlayerState.normal)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    PlayerY = hit.point.y + 0.4f;

                }
                if (Input.GetKey(KeyCode.Space))
                {
                    PlayerY = hit.point.y + 5f;
                }
            }
            transform.DOMoveY(PlayerY, 0.2f);
        }
    }
    private void Update()
    {
        Move();
        if (GameManager.Instance.playerState == GameManager.PlayerState.overdrive)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.Space))
            {
                Dash();
            }
        }
    }

    void Dash()
    {
        if (isDashing)
        {
            return;
        }
        StartCoroutine(Dashing());
    }

    IEnumerator Dashing()
    {
        isDashing = true;
        transform.DOMoveZ(20f, 0.2f).OnComplete(()=> transform.DOMoveZ(0f, 1.5f));
        yield return wait;
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, -transform.up * 2f);
    }
}

