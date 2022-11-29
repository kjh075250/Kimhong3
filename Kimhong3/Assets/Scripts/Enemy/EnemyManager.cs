using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject enemy_1;
    public int limit = 5;

    private void Start()
    {
        Invoke("SetPos", 5f);
    }
    private void Update()
    {
        
    }

    private void SetPos()
    {
        transform.position = new Vector3((Random.Range(-22f, 24f)), (Random.Range(10f, 24f)), player.transform.position.z + 30);
        StartCoroutine("SummonEnemy");

    }

    IEnumerator SummonEnemy()
    {
        for (int i = 0; i < limit; i++)
        {
            GameObject.Instantiate(enemy_1, gameObject.transform);
            yield return new WaitForSeconds(0.2f);
        }
    }
}
