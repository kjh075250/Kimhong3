using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MapControl : MonoBehaviour
{
    //맵에 붙어있는 장애물 갯수 정하기
    int obsCount;
    //움직임에 사용할 리지드 바디
    Rigidbody rb;
    //맵에 붙어있을 장애물
    GameObject[] obs;
    //현재 활성화 되어있는 장애물
    GameObject nowObs;
    //waitForSeconds 따로 빼두기
    WaitForSeconds wait;
    //장애물 데이터를 받아오기 위한 so
    public MapSO m_SO;

    private void Awake()
    {
        //캐싱과 장애물 숫자를 정함
        obsCount = 2;
        rb = GetComponent<Rigidbody>();
        obs = new GameObject[obsCount];
        wait = new WaitForSeconds(1f);
    }
    void Start()
    {
        //현재 장애물을 일단 obs[0]으로 초기화 해놓음
        nowObs = obs[0];
        //맵 오브젝트(자신)에 공중에 소환될 장애물을 만들고 obs[0]에 저장
        obs[0] = Instantiate(m_SO.map_JumpObstacle, transform);
        //비활성화 해둠
        obs[0].SetActive(false);

        //맵 오브젝트(자신)에 바닥에 소환될 장애물을 만들고 obs[1]에 저장
        obs[1] = Instantiate(m_SO.map_SlideObstacle, transform);
        //비활성화 해둠
        obs[1].SetActive(false);
    }

    //맵이 맨 뒤로 이동할 때 실행 될 함수
    //매개변수로 받아온 인덱스를 이용해 바닥과 공중 둘 중 하나의 장애물을 랜덤으로 소환
    public void SpawnObstacle(int index)
    {
        //장애물 위치 초기화 & 비활성화까지 다시 함
        obs[0].transform.DOMoveY(0, 0);
        obs[1].transform.DOMoveY(8, 0);
        for(int i = 0; i < obsCount; i++)
        {
            obs[i].SetActive(false);
        }

        //매개변수로 받아온 index를 이용해 switch 돌림
        switch (index)
        {
            //index가 0이라면 obs[0](공중 장애물)을 활성화 하고 nowObs에 이를 담음
            case 0:
                obs[index].SetActive(true);
                nowObs = obs[index];
                break;
            //index가 1이라면 obs[1](바닥 장애물)을 활성화 하고 nowObs에 이를 담음
            case 1:
                obs[index].SetActive(true);
                nowObs = obs[index];
                break;
            default:
                break;
        }

    }

    //보스 패턴 중 하나. 장애물끼리 서로 위치를 바꿈
    public void SwitchObs()
    {
        //nowObs가 null이면 리턴
        if (nowObs == null) return;
        StartCoroutine(SwitchCoroutine());
    }

    IEnumerator SwitchCoroutine()
    {
        //y값이 8보다 작다면(아래에 있다면)
        if (nowObs.transform.position.y < 8)
        {
            //흔들리는 연출 후에
            nowObs.transform.DOShakePosition(1f, 2f, 10, 90);
            yield return wait;
            //위치를 8로 이동(공중으로이동) 후 카메라 흔들림 연출
            nowObs.transform.DOMoveY(8, 0.07f);
            GameManager.Instance.cameraShake.Invoke();
        }
        //y값이 8보다 작지 않다면(공중에 있다면)
        else
        {
            //흔들리는 연출 후에
            nowObs.transform.DOShakePosition(1f, 2f, 10, 90);
            yield return wait;
            //위치를 8로 이동(아래로이동) 후 카메라 흔들림 연출
            nowObs.transform.DOMoveY(0, 0.07f);
            //그리고 파티클 시스템을 불러와 파티클 실행
            nowObs.GetComponent<ParticleSystem>().Play();
            GameManager.Instance.cameraShake.Invoke();
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        //GetAxis로 vectical 값을 받아오고 moveVec 값에 넣음
        Vector3 moveVec = new Vector3(0, 0, Input.GetAxis("Vertical") * 300f);
        //AddForce를 이용해 점차 빨라지게 움직임(맵이 플레이어 쪽으로 움직이는 것이니 마이너스로)
        rb.AddForce(-moveVec, ForceMode.Acceleration);
        //최대 속도
        float minZ;
        //만약 노말상태나 무적상태라면
        if (GameManager.Instance.playerState == GameManager.PlayerState.normal || GameManager.Instance.playerState == GameManager.PlayerState.godMod)
        {
            //최대 속도는 500
            minZ = -500f;
        }
        //폭주상태일땐 1000;
        else
        {
            minZ = -1000f;
        }

        //만약 보스를 공격중이라면 아예 바로 멈추게
        if (GameManager.Instance.playerState == GameManager.PlayerState.bossAttack)
        {
            rb.velocity = Vector3.zero;
        }
        //아니라면 clamp를 이용해 rigidbody에 담음
        else
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, Mathf.Clamp(rb.velocity.z, minZ, 0));
        }
    }
}
