using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class PlayerControl : MonoBehaviour
{
    //코루틴에 사용할 waitforseconds 따로 빼기(new 계속 안하려고)
    WaitForSeconds wait;
    //플레이어 이펙트들 담은 스크립트
    PlayerEffect playerEffect;
    //마지막 폭발 연출을 위한 rigidbody
    Rigidbody rb;

    //대쉬 중인지 체크하는 bool
    protected bool isDashing = false;
    //보스가 사정거리 이내에 있는지 체크하는 bool
    protected bool canBossAttack = false;

    private void Start()
    {
        //wait 시간 정해주고, 캐싱
        wait = new WaitForSeconds(2f);
        rb = GetComponent<Rigidbody>();
        playerEffect = GetComponentInChildren<PlayerEffect>();
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.playerState != GameManager.PlayerState.bossAttack)
        {
            Move();
        }
        //만약 플레이어 상태가 폭주 모드이고 쉬프트나 스페이스를 누르면 대쉬
        if (GameManager.Instance.playerState == GameManager.PlayerState.overdrive)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.Space))
            {
                Dash();
            }
        }

    }
    void Move()
    {
        //왼쪽 오른쪽 움직이기위해 인풋값을 받아옴
        Vector3 horVec = new Vector3(Input.GetAxis("Horizontal") * 15f, 0, 0);

        //왼쪽 오른쪽 움직일때 차가 기울어지는 연출을 위해 
        Vector3 rotateVec = new Vector3(0, 0, Input.GetAxisRaw("Horizontal") * 30f);
        Tweener rotTween = transform.DORotate(-rotateVec, 1f);
        rotTween.Play();

        //자동차의 높이를 지정하기 위해 바닥에 ray를 쏘고 높이를 조정함
        RaycastHit hit;
        //ray가 바닥에 맞았다면
        if (Physics.Raycast(transform.position, -transform.up, out hit, 100f, LayerMask.GetMask("Ground")))
        {
            //아무런 행동도 하지 않거나 폭주모드 일 땐 2.5f
            float PlayerY = hit.point.y + 2.5f;
            //만약 노말모드 일 때
            if (GameManager.Instance.playerState == GameManager.PlayerState.normal)
            {
                //쉬프트를 누르면 슬라이딩
                if (Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.Space))
                {
                    PlayerY = hit.point.y + 0.4f;
                    playerEffect.SlideEffect(true);
                }
                else playerEffect.SlideEffect(false);

                //스페이스바를 누르면 상승
                if (Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.LeftShift))
                {
                    PlayerY = hit.point.y + 5f;
                    playerEffect.HoverEffect(true);
                }
                else playerEffect.HoverEffect(false);
            }
            transform.DOMoveY(PlayerY, 0.2f);
        }
        //벡터에 담고 translate로 움직여줌
        Vector3 playerVec = (horVec * Time.deltaTime * 2.5f);
        transform.Translate(playerVec);
    }


    void Dash()
    {
        //현재 대쉬중이라면 리턴
        if (isDashing)
        {
            return;
        }
        StartCoroutine(Dashing());
    }

    IEnumerator Dashing()
    {
        isDashing = true;
        //만약 보스가 사정거리 이내에 있다면 보스어택 이벤트 호출
        if (canBossAttack)
        {
            Debug.Log("BossAttack");
            GameManager.Instance.bossAttack.Invoke();
        }
        //움직인 후에 wait만큼 기다려줌
        transform.DOMoveZ(20f, 0.2f).OnComplete(() => transform.DOMoveZ(0f, 1.5f));
        yield return wait;
        isDashing = false;
    }

    public void StartBossAttackDashing()
    {
        StartCoroutine(BossAttackDashing());
    }

    IEnumerator BossAttackDashing()
    {
        yield return wait;
        transform.DOMoveZ(40f, 0.2f).OnComplete(() =>
        {
            GameManager.Instance.GameClear.Invoke();
            transform.DOMove((Vector3.back + Vector3.up) * 50f, 3f);
        });

    }
    private void OnTriggerStay(Collider col)
    {
        //현재 보스 트리거 이내에 들어와 있다면
        if (col.gameObject.CompareTag("Boss"))
        {
            canBossAttack = true;
        }
    }
    void OnTriggerExit(Collider col)
    {
        //보스 트리거 안에서 나가거나 들어와있지 않다면
        if (col.gameObject.CompareTag("Boss"))
        {
            canBossAttack = false;
        }
    }
}

