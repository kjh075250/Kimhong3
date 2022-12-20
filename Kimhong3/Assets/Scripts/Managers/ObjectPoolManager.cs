using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance;

    [SerializeField]
    private GameObject poolingBullet;

    private Queue<GameObject> poolingBulletQueue = new Queue<GameObject>();

    [SerializeField]
    private GameObject poolingEnemy;

    private Queue<GameObject> poolingEnemyQueue = new Queue<GameObject>();

    private void Awake()
    {
        Instance = this;
    }

    private GameObject CreateNewBullet()
    {
        var newObj = Instantiate(poolingBullet, transform);
        newObj.transform.SetParent(transform);
        newObj.gameObject.SetActive(false);
        return newObj;
    }
    private GameObject CreateNewEnemy()
    {
        var newObj = Instantiate(poolingEnemy, transform);
        newObj.transform.SetParent(transform);
        newObj.gameObject.SetActive(false);
        return newObj;
    }

    public static GameObject GetBullet()
    {
        if (Instance.poolingBulletQueue.Count == 0)
        {
            var newObj = Instance.CreateNewBullet();
            newObj.transform.SetParent(Instance.transform);
            newObj.gameObject.SetActive(true);
            return newObj;
        }
        else
        {
            var obj = Instance.poolingBulletQueue.Dequeue();
            obj.transform.SetParent(Instance.transform);
            obj.gameObject.SetActive(true);
            return obj;
        }
    }
    public static GameObject GetEnemy()
    {
        if (Instance.poolingEnemyQueue.Count == 0)
        {
            var newObj = Instance.CreateNewEnemy();
            newObj.transform.SetParent(Instance.transform);
            newObj.gameObject.SetActive(true);
            return newObj;
        }
        else
        {
            var obj = Instance.poolingEnemyQueue.Dequeue();
            obj.transform.SetParent(Instance.transform);
            obj.gameObject.SetActive(true);
            return obj;
        }
    }

    public static void ReturnBullet(GameObject bullet)
    {
        bullet.gameObject.SetActive(false);
        bullet.transform.SetParent(Instance.transform);
        Instance.poolingBulletQueue.Enqueue(bullet);
        Debug.Log(Instance.poolingBulletQueue.Count);
    }
    
    public static void ReturnEnemy(GameObject enemy)
    {
        enemy.gameObject.SetActive(false);
        enemy.transform.SetParent(Instance.transform);
        Instance.poolingEnemyQueue.Enqueue(enemy);
        Debug.Log(Instance.poolingEnemyQueue.Count);
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
