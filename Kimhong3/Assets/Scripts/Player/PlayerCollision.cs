using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Obstacle"))
        {
            if(GameManager.Instance.playerState == GameManager.PlayerState.normal)
            {
                Debug.Log("Die");
            }
            else
            {
                Debug.Log("Break");
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        float tg = GameManager.Instance.thunderGage;
        if (other.gameObject.CompareTag("Bullet"))
        {
            tg = Mathf.Clamp(tg, 0f, 100f);
            tg -= 30f;
            GameManager.Instance.thunderGage = tg;
        }
    }
}
