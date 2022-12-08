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
}
