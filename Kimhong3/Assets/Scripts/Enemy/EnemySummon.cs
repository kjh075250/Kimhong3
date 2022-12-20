using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySummon : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    public int limit = 5;

    private void Start()
    {
        InvokeRepeating("SetPos", 5f, 15f);
    }
    private void Update()
    {
        
    }

    private void SetPos()
    {
        transform.position = new Vector3((Random.Range(18f, 25f)), (Random.Range(6f, 15f)), player.transform.position.z + 85);
        StartCoroutine("SummonEnemy");
    }

    IEnumerator SummonEnemy()
    {
        for (int i = 0; i < limit; i++)
        {
            var enemy = ObjectPoolManager.GetEnemy();
            enemy.transform.position = transform.position;
            yield return new WaitForSeconds(0.2f);
        }
    }
}
