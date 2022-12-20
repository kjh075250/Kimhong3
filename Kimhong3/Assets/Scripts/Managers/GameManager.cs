using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;

    MapManager mapManager;
    public MapManager _MapManager => mapManager;
    UIManager uiManager;

    [SerializeField]
    GameObject player;
    public GameObject Player => player;

    [SerializeField]
    GameObject boss;
    public GameObject Boss => boss;

    float playerBossDistance;
    public float PlayerBossDistance => playerBossDistance;

    public UnityEvent cameraShake;
    public UnityEvent playerShake;
    public UnityEvent bossAttack;

    public BossObsSO obsSO;
    public GameObject[] bossObs;

    WaitForSeconds wait;

    //기술 쓸 때 필요한 에너지
    public float thunderGage;
    public float ThunderGage => thunderGage;

    public enum PlayerState { normal, overdrive, godMod };
    public PlayerState playerState = PlayerState.normal;

    public Light light;
    public Color dashColor;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        mapManager = GetComponent<MapManager>();
        uiManager = GetComponent<UIManager>();
        thunderGage = 0;
        playerState = PlayerState.normal;
        bossObs[0] = Instantiate(obsSO.Attack1);
        bossObs[1] = Instantiate(obsSO.Attack2);
        wait = new WaitForSeconds(0.1f);
        dashColor = new Color(0, 0.3f, 0.3f);
    }
    void Update()
    {
        playerBossDistance = Vector3.Distance(player.transform.position, boss.transform.position);

        if(Input.GetKeyDown(KeyCode.M))
        {
            thunderGage = 90;
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("KjhScene");
            Time.timeScale = 1;
        }
        if(thunderGage >= 99 && Input.GetKey(KeyCode.F))
        {
            playerState = PlayerState.overdrive;
        }
        else if(thunderGage <= 1 && playerState == PlayerState.overdrive)
        {
            StartCoroutine(GodMod());
        }
    }


    public float GetSpeed()
    {
        return Mathf.Clamp(Mathf.Abs(mapManager.lastObject.GetComponent<Rigidbody>().velocity.z),0, Mathf.Infinity);
    }

    public void DecreaseThunderGage(float value)
    {
        thunderGage -= value;
    }
    public IEnumerator SetThunderGage()
    {
        while (true)
        {
            if (playerState == PlayerState.normal)
            {
                thunderGage += 0.1f + Mathf.Clamp(GetSpeed() * 0.005f, 0, 0.4f);
                light.color = Color.black;
            }
            else if (playerState == PlayerState.overdrive)
            {
                thunderGage -= 0.5f + Mathf.Clamp(GetSpeed() * 0.005f, 0, 0.4f);
                light.color = dashColor;
            }
            thunderGage = Mathf.Clamp(thunderGage, 0, 100);
            yield return wait;
        }
    }

    IEnumerator GodMod()
    {
        playerState = PlayerState.godMod;
        yield return new WaitForSeconds(1f);
        playerState = PlayerState.normal;
    }
    public void GameOver()
    {
        uiManager.GameOverImage();
    }
}
