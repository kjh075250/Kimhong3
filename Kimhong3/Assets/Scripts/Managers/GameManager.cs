using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //간단한 싱글톤(나중에 시간되면 바꿀게)
    private static GameManager instance;
    public static GameManager Instance => instance;

    MapManager mapManager;
    UIManager uiManager;

    [SerializeField]
    GameObject player;
    public GameObject Player => player;
    [SerializeField]
    GameObject boss;
    public GameObject Boss => boss;

    //기술 쓸 때 필요한 에너지
    float thunderGage;
    public float ThunderGage => thunderGage;

    public enum PlayerState { normal, overdrive };
    public PlayerState playerState = PlayerState.normal;

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
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            thunderGage = 80;
        }
        if(thunderGage >= 99 && Input.GetKey(KeyCode.F))
        {
            playerState = PlayerState.overdrive;
        }
        else if(thunderGage <= 1)
        {
            playerState = PlayerState.normal;
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
        while(true)
        {
            if(playerState == PlayerState.normal)
            {
                thunderGage += 0.1f + Mathf.Clamp(GetSpeed() * 0.005f, 0, 0.4f);
            }
            else if(playerState == PlayerState.overdrive)
            {
                thunderGage -= 0.1f + Mathf.Clamp(GetSpeed() * 0.005f, 0, 0.4f);
            }
            thunderGage = Mathf.Clamp(thunderGage, 0, 100);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
