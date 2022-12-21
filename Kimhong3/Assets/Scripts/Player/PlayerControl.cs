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
        //보스 공격 연출중이 아니면 움직임 코드 활성화
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
                //누르지 않고있으면 이펙트 꺼줌
                else playerEffect.SlideEffect(false);

                //스페이스바를 누르면 상승
                if (Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.LeftShift))
                {
                    PlayerY = hit.point.y + 5f;
                    playerEffect.HoverEffect(true);
                }
                //누르지 않고있으면 이펙트 꺼줌
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
        //현재 대쉬중이 아니라면 대쉬 시작 
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
        //보스가 공격 가능 범위 안에 있을 때
        StartCoroutine(BossAttackDashing());
    }

    IEnumerator BossAttackDashing()
    {
        //보스 공격 연출을 위해 잠시 기다린 뒤
        yield return wait;
        //플레이어 앞으로 움직이는 연출
        transform.DOMoveZ(40f, 0.2f).OnComplete(() =>
        {
            //앞으로 움직이는 연출이 끝나면 게임매니저의 게임오버 이벤트 호출, 뒤로 날아가는 연출
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

    private void OnTriggerEnter(Collider other)
    {
        //게임매니저에서 현재 에너지 게이지 값을 가져옴
        float tg = GameManager.Instance.thunderGage;

        //만약 폭주 상태가 아니고 적의 공격"Bullet"에 맞는다면 에너지 게이지를 깎는 로직
        if (other.gameObject.CompareTag("Bullet") && GameManager.Instance.playerState == GameManager.PlayerState.normal)
        {
            //게이지 값을 0 아래로 가거나 100이상으로 가지 않게 Clamp
            tg = Mathf.Clamp(tg, 0f, 100f);
            //게이지 값을 깎는다
            tg -= 15f;
            //깎은 값을 적용
            GameManager.Instance.thunderGage = tg;
        }

        //장애물에 부딪혔다면
        if (other.gameObject.CompareTag("Obstacle"))
        {
            //노말 상태라면(폭주, 보스 연출모드가 아님)
            if (GameManager.Instance.playerState == GameManager.PlayerState.normal)
            {
                //playerShake라는 연출 이벤트를 호출하고
                GameManager.Instance.playerShake.Invoke();
                //게임오버 하는 코루틴 호출
                StartCoroutine(DieEffect());
            }
            else
            {
                //노말 상태가 아니라면

                //장애물 부수는 파티클 켜고 연출 코루틴 함수 호출
                playerEffect.BreakEffect();
                StartCoroutine(BreakingEffect(other.gameObject));
                
                //화면 흔들림 효과 이벤트 호출
                GameManager.Instance.cameraShake.Invoke();
            }
        }
    }

    IEnumerator BreakingEffect(GameObject obj)
    {
        //부딪힌 장애물에 파티클시스템을 가져오고 있다면 플레이
        obj.gameObject?.GetComponent<ParticleSystem>().Play();
        //시간 잠깐 느려지게 연출
        Time.timeScale = 0.5f;
        yield return new WaitForSecondsRealtime(0.2f);
        //장애물을 없앰
        obj.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
    IEnumerator DieEffect()
    {
        //시간 멈추는 연출
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(0.2f);
        Time.timeScale = 0.3f;
        //게임 매니저의 게임오버 함수 호출(UI 띄우기 등등)
        GameManager.Instance.GameOver();
    }

}

