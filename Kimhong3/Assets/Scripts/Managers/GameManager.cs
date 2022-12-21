using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //게임 매니저 싱글톤
    private static GameManager instance;
    public static GameManager Instance => instance;

    //게임매니저 오브젝트에 붙어있는 맵매니저
    MapManager mapManager;
    //다른 오브젝트에서 참조할 수 있게 프로퍼티
    public MapManager _MapManager => mapManager;

    //게임 매니저 오브젝트에 붙어있는 UI매니저
    UIManager uiManager;

    //플레이어 위치를 가져옴
    [SerializeField]
    Transform player;
    //다른 오브젝트에서 참조할 수 있게 프로퍼티
    public Transform Player => player;

    //보스 오브젝트를 사용하기 위해 가져옴
    [SerializeField]
    GameObject boss;
    //다른 오브젝트에서 참조할 수 있게 프로퍼티
    public GameObject Boss => boss;

    //플레이어와 보스의 거리값을 저장하기 위한 float
    float playerBossDistance;
    public float PlayerBossDistance => playerBossDistance;

    //화면 흔들림 연출 이벤트
    public UnityEvent cameraShake;
    //플레이어 흔들림 연출 이벤트
    public UnityEvent playerShake;
    //보스를 공격 할 때 연출 이벤트
    public UnityEvent bossAttack;
    public UnityEvent GameClear;

    //보스가 공격할 떄 사용할 오브젝트 정보를 so로 받아옴
    public BossObsSO obsSO;
    //보스가 공격할 떄 소환할 오브젝트 
    public GameObject[] bossObs;

    //코루틴 많이 쓰는 waitforseconds 따로 빼두기
    WaitForSeconds wait;

    //기술 쓸 때 필요한 에너지
    public float thunderGage;
    public float ThunderGage => thunderGage;

    //현재 플레이어 상태, { 노말, 폭주, 폭주 끝나고 잠시 무적, 보스를 공격중 }
    public enum PlayerState { normal, overdrive, godMod, bossAttack };
    //처음엔 노말 상태로
    public PlayerState playerState = PlayerState.normal;

    //연출을 위해 빛과 색을 가져옴
    public Light light;
    Color dashColor;

    void Awake()
    {
        //싱글톤) 만약 instance가 null이라면 현재 오브젝트를 instance로 만듬
        if(instance == null)
        {
            instance = this;
        }
        //캐싱과 변수들 초기화
        mapManager = GetComponent<MapManager>();
        uiManager = GetComponent<UIManager>();
        thunderGage = 0;
        playerState = PlayerState.normal;
        wait = new WaitForSeconds(0.1f);
        dashColor = new Color(0, 0.3f, 0.3f);

        //보스가 공격에 사용할 오브젝트 so에서 가져옴
        bossObs[0] = Instantiate(obsSO.Attack1);
        bossObs[1] = Instantiate(obsSO.Attack2);

    }
    void Update()
    {
        //플레이어와 보스의 위치를 구하고 거리를 변수에 저장(ui에 사용)
        playerBossDistance = Vector3.Distance(player.position, boss.transform.position);

        //R을 눌러서 재시작 할 수 있게
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("KjhScene");
            Time.timeScale = 1;
        }

        //만약 에너지 게이지가 99이상이라면, F를 눌렀을 떄 폭주모드가 가능하게
        if(thunderGage >= 99)
        {
            //F를 누르라는 UI텍스트 띄우기
            uiManager.GageTextActive(true);
            if(Input.GetKey(KeyCode.F))
            {
                playerState = PlayerState.overdrive;
            }
        }
        //만약 폭주모드 상태일때 에너지가 1이하로 내려가면
        else if(thunderGage <= 1 && playerState == PlayerState.overdrive)
        {
            //폭주 후 잠깐의 무적시간을 주는 코루틴 실행
            StartCoroutine(GodMod());
        }
        else
        {
            //F 누르라는 UI텍스트 끄기
            uiManager.GageTextActive(false);
        }
    }

    //맵매니저에서 현재 가장 뒤에있는(가장 마지막으로 이동한) 오브젝트의 리지드바디의 velocity를 받아옴
    //다른 오브젝트 들에서 참조할 수 있게 해줌
    public float GetSpeed()
    {
        return Mathf.Clamp(Mathf.Abs(mapManager.lastObject.GetComponent<Rigidbody>().velocity.z),0, Mathf.Infinity);
    }

    //적 공격에 맞았을 때 게이지 감소하게 해주는 함수
    public void DecreaseThunderGage(float value)
    {
        thunderGage -= value;
    }
    public IEnumerator SetThunderGage()
    {
        while (true)
        {
            //만약 현재 상태가 노말상태라면
            if (playerState == PlayerState.normal)
            {
                //현재 속도에 비례해 0.1f + 현재 속도 비율값으로 게이지를 점점 채운다
                thunderGage += 0.1f + Mathf.Clamp(GetSpeed() * 0.005f, 0, 0.4f);
                light.color = Color.black;
            }
            //만약 폭주상태라면
            else if (playerState == PlayerState.overdrive)
            {
                //에너지 게이지를 점차 감소 시킨다
                thunderGage -= 0.5f + Mathf.Clamp(GetSpeed() * 0.005f, 0, 0.4f);
                light.color = dashColor;
            }
            //0과 100을 넘지 않도록
            thunderGage = Mathf.Clamp(thunderGage, 0, 100);
            yield return wait;
        }
    }

    IEnumerator GodMod()
    {
        //폭주 끝나고 잠깐으 무적을 위해
        playerState = PlayerState.godMod;
        yield return new WaitForSeconds(1f);
        playerState = PlayerState.normal;
    }

    //보스를 공격하는 상태로 바꿔주는 함수(보스 공격 상태로 바꿔 다른 로직이 작동하지 않게 하려고)
    public void SetBossAttackState()
    {
        StartCoroutine(BossAttackState());
    }

    IEnumerator BossAttackState()
    {
        //상태를 바꿔주고
        playerState = PlayerState.bossAttack;
        yield return new WaitForSeconds(2.2f);

        //화면 흔들리는 이벤트 호출
        cameraShake.Invoke();
        //보스 오브젝트에 파티클시스템이 있다면 실행
        boss?.GetComponentInChildren<ParticleSystem>().Play();
        yield return new WaitForSeconds(0.2f);
        //보스 없애기(보스가 죽어 사라지는 것)
        boss.SetActive(false);
    }

    //게임 오버 시 게임오버 이미지를 띄우게 ui매니저 부르는 함수
    public void GameOver()
    {
        uiManager.GameOverImage();
    }
    //게임 클리어 시 게임클리어 이미지를 띄우게 ui매니저 부르는 함수
    public void GameClearUI()
    {
        //시간 느리게 가는 연출
        Time.timeScale = 0.5f;
        uiManager.GameClearImage();
    }
}
