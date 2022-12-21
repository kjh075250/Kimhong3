using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //���� �Ŵ��� �̱���
    private static GameManager instance;
    public static GameManager Instance => instance;

    //���ӸŴ��� ������Ʈ�� �پ��ִ� �ʸŴ���
    MapManager mapManager;
    //�ٸ� ������Ʈ���� ������ �� �ְ� ������Ƽ
    public MapManager _MapManager => mapManager;

    //���� �Ŵ��� ������Ʈ�� �پ��ִ� UI�Ŵ���
    UIManager uiManager;

    //�÷��̾� ��ġ�� ������
    [SerializeField]
    Transform player;
    //�ٸ� ������Ʈ���� ������ �� �ְ� ������Ƽ
    public Transform Player => player;

    //���� ������Ʈ�� ����ϱ� ���� ������
    [SerializeField]
    GameObject boss;
    //�ٸ� ������Ʈ���� ������ �� �ְ� ������Ƽ
    public GameObject Boss => boss;

    //�÷��̾�� ������ �Ÿ����� �����ϱ� ���� float
    float playerBossDistance;
    public float PlayerBossDistance => playerBossDistance;

    //ȭ�� ��鸲 ���� �̺�Ʈ
    public UnityEvent cameraShake;
    //�÷��̾� ��鸲 ���� �̺�Ʈ
    public UnityEvent playerShake;
    //������ ���� �� �� ���� �̺�Ʈ
    public UnityEvent bossAttack;
    public UnityEvent GameClear;

    //������ ������ �� ����� ������Ʈ ������ so�� �޾ƿ�
    public BossObsSO obsSO;
    //������ ������ �� ��ȯ�� ������Ʈ 
    public GameObject[] bossObs;

    //�ڷ�ƾ ���� ���� waitforseconds ���� ���α�
    WaitForSeconds wait;

    //��� �� �� �ʿ��� ������
    public float thunderGage;
    public float ThunderGage => thunderGage;

    //���� �÷��̾� ����, { �븻, ����, ���� ������ ��� ����, ������ ������ }
    public enum PlayerState { normal, overdrive, godMod, bossAttack };
    //ó���� �븻 ���·�
    public PlayerState playerState = PlayerState.normal;

    //������ ���� ���� ���� ������
    public Light light;
    Color dashColor;

    void Awake()
    {
        //�̱���) ���� instance�� null�̶�� ���� ������Ʈ�� instance�� ����
        if(instance == null)
        {
            instance = this;
        }
        //ĳ�̰� ������ �ʱ�ȭ
        mapManager = GetComponent<MapManager>();
        uiManager = GetComponent<UIManager>();
        thunderGage = 0;
        playerState = PlayerState.normal;
        wait = new WaitForSeconds(0.1f);
        dashColor = new Color(0, 0.3f, 0.3f);

        //������ ���ݿ� ����� ������Ʈ so���� ������
        bossObs[0] = Instantiate(obsSO.Attack1);
        bossObs[1] = Instantiate(obsSO.Attack2);

    }
    void Update()
    {
        //�÷��̾�� ������ ��ġ�� ���ϰ� �Ÿ��� ������ ����(ui�� ���)
        playerBossDistance = Vector3.Distance(player.position, boss.transform.position);

        //R�� ������ ����� �� �� �ְ�
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("KjhScene");
            Time.timeScale = 1;
        }

        //���� ������ �������� 99�̻��̶��, F�� ������ �� ���ָ�尡 �����ϰ�
        if(thunderGage >= 99)
        {
            //F�� ������� UI�ؽ�Ʈ ����
            uiManager.GageTextActive(true);
            if(Input.GetKey(KeyCode.F))
            {
                playerState = PlayerState.overdrive;
            }
        }
        //���� ���ָ�� �����϶� �������� 1���Ϸ� ��������
        else if(thunderGage <= 1 && playerState == PlayerState.overdrive)
        {
            //���� �� ����� �����ð��� �ִ� �ڷ�ƾ ����
            StartCoroutine(GodMod());
        }
        else
        {
            //F ������� UI�ؽ�Ʈ ����
            uiManager.GageTextActive(false);
        }
    }

    //�ʸŴ������� ���� ���� �ڿ��ִ�(���� ���������� �̵���) ������Ʈ�� ������ٵ��� velocity�� �޾ƿ�
    //�ٸ� ������Ʈ �鿡�� ������ �� �ְ� ����
    public float GetSpeed()
    {
        return Mathf.Clamp(Mathf.Abs(mapManager.lastObject.GetComponent<Rigidbody>().velocity.z),0, Mathf.Infinity);
    }

    //�� ���ݿ� �¾��� �� ������ �����ϰ� ���ִ� �Լ�
    public void DecreaseThunderGage(float value)
    {
        thunderGage -= value;
    }
    public IEnumerator SetThunderGage()
    {
        while (true)
        {
            //���� ���� ���°� �븻���¶��
            if (playerState == PlayerState.normal)
            {
                //���� �ӵ��� ����� 0.1f + ���� �ӵ� ���������� �������� ���� ä���
                thunderGage += 0.1f + Mathf.Clamp(GetSpeed() * 0.005f, 0, 0.4f);
                light.color = Color.black;
            }
            //���� ���ֻ��¶��
            else if (playerState == PlayerState.overdrive)
            {
                //������ �������� ���� ���� ��Ų��
                thunderGage -= 0.5f + Mathf.Clamp(GetSpeed() * 0.005f, 0, 0.4f);
                light.color = dashColor;
            }
            //0�� 100�� ���� �ʵ���
            thunderGage = Mathf.Clamp(thunderGage, 0, 100);
            yield return wait;
        }
    }

    IEnumerator GodMod()
    {
        //���� ������ ����� ������ ����
        playerState = PlayerState.godMod;
        yield return new WaitForSeconds(1f);
        playerState = PlayerState.normal;
    }

    //������ �����ϴ� ���·� �ٲ��ִ� �Լ�(���� ���� ���·� �ٲ� �ٸ� ������ �۵����� �ʰ� �Ϸ���)
    public void SetBossAttackState()
    {
        StartCoroutine(BossAttackState());
    }

    IEnumerator BossAttackState()
    {
        //���¸� �ٲ��ְ�
        playerState = PlayerState.bossAttack;
        yield return new WaitForSeconds(2.2f);

        //ȭ�� ��鸮�� �̺�Ʈ ȣ��
        cameraShake.Invoke();
        //���� ������Ʈ�� ��ƼŬ�ý����� �ִٸ� ����
        boss?.GetComponentInChildren<ParticleSystem>().Play();
        yield return new WaitForSeconds(0.2f);
        //���� ���ֱ�(������ �׾� ������� ��)
        boss.SetActive(false);
    }

    //���� ���� �� ���ӿ��� �̹����� ���� ui�Ŵ��� �θ��� �Լ�
    public void GameOver()
    {
        uiManager.GameOverImage();
    }
    //���� Ŭ���� �� ����Ŭ���� �̹����� ���� ui�Ŵ��� �θ��� �Լ�
    public void GameClearUI()
    {
        //�ð� ������ ���� ����
        Time.timeScale = 0.5f;
        uiManager.GameClearImage();
    }
}
