using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //������ �̱���(���߿� �ð��Ǹ� �ٲܰ�)
    private static GameManager instance;
    public static GameManager Instance => instance;

    [SerializeField]
    GameObject player;
    public GameObject Player => player;

    //�� ���� �ݺ� ������ �� ������Ʈ
    public GameObject LastMapObject;

    //��� �� �� �ʿ��� ������
    float thunderGage;
    public float ThunderGage => thunderGage;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        thunderGage = 0;
        StartCoroutine(SetThunderGage());
    }
    void Update()
    {
    }

    public float GetSpeed()
    {
        return Mathf.Clamp(Mathf.Abs(LastMapObject.GetComponent<Rigidbody>().velocity.z),0, Mathf.Infinity);
    }
    public void DecreaseThunderGage(float value)
    {
        thunderGage -= value;
    }
    IEnumerator SetThunderGage()
    {
        while(true)
        {
            thunderGage += 0.1f + Mathf.Clamp(GetSpeed() * 0.005f, 0, 0.4f);
            thunderGage = Mathf.Clamp(thunderGage, 0, 100);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
