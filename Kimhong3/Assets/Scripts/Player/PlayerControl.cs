using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class PlayerControl : MonoBehaviour
{

    private Rigidbody rigidbody;
    WaitForSeconds wait;
    PlayerEffect playerEffect;
    Vector3 dashTargetPos;
    protected bool isDashing = false;
    protected bool canBossAttack = false;
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        wait = new WaitForSeconds(2f);
        playerEffect = GetComponentInChildren<PlayerEffect>();
        canBossAttack = false;
    }

    void Move()
    {
        Vector3 horVec = new Vector3(Input.GetAxis("Horizontal") * 15f, 0, 0);
        Vector3 rotateVec = new Vector3(0, 0, Input.GetAxisRaw("Horizontal") * 30f);
        Tweener rotTween = transform.DORotate(-rotateVec, 1f);
        rotTween.Play();
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, 100f, LayerMask.GetMask("Ground")))
        {
            float PlayerY = hit.point.y + 2.5f;
            if (GameManager.Instance.playerState == GameManager.PlayerState.normal)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    PlayerY = hit.point.y + 0.4f;
                    playerEffect.SlideEffect(true);
                }
                else playerEffect.SlideEffect(false);
                if (Input.GetKey(KeyCode.Space))
                {
                    PlayerY = hit.point.y + 5f;
                }
            }
            transform.DOMoveY(PlayerY, 0.2f);
        }
        Vector3 playerVec = (horVec * Time.deltaTime * 2.5f);
        transform.Translate(playerVec);
    }
    private void FixedUpdate()
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
        if(canBossAttack)
        {
            Debug.Log("BossAttack");
        }
        transform.DOMoveZ(20f, 0.2f).OnComplete(()=> transform.DOMoveZ(0f, 1.5f));
        yield return wait;
        isDashing = false;
    }
    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("Boss"))
        {
            canBossAttack = true;
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Boss"))
        {
            canBossAttack = false;
        }
    }
}

