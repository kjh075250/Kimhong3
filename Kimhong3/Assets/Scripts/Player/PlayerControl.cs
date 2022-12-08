using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class PlayerControl : MonoBehaviour
{

    private Rigidbody rigidbody;
    Vector3 dashTargetPos;
    public bool isDashing = false;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    public void FixedUpdate()
    {
        Move();
    }
    void Move()
    {
        Vector3 horVec = new Vector3(Input.GetAxisRaw("Horizontal") * 15f, transform.position.y, transform.position.z);
        Vector3 rotateVec = new Vector3(0, 0, Input.GetAxisRaw("Horizontal") * 30f);
        transform.DOMoveX(transform.position.x + horVec.x, 1f, false);
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
            Debug.Log("´ë½¬ ÄðÅÁ¤±");
            return;
        }
        StartCoroutine(Dashing());
    }
    IEnumerator Dashing()
    {
        isDashing = true;
        Debug.Log("´ë½¬");
        yield return new WaitForSeconds(1f);
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

