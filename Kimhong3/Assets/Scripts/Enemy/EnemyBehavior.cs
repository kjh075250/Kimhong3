using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    float minXPos;
    float minYPos;
    float maxXPos;
    float maxYPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Move()
    {

    }

    private void Attack()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            //¿Ã∆Â∆Æ ø¨√‚
            Destroy(gameObject);
        }
    }
}
