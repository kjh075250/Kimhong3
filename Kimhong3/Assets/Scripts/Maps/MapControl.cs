using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MapControl : MonoBehaviour
{
    int obsCount;
    Rigidbody rb;
    GameObject[] obs;
    public MapSO m_SO;
    private void Awake()
    {
        obsCount = 3;
        rb = GetComponent<Rigidbody>();
        obs = new GameObject[obsCount];
    }
    void Start()
    {
        obs[0] = Instantiate(m_SO.map_JumpObstacle, transform);
        obs[0].SetActive(false);

        obs[1] = Instantiate(m_SO.map_SlideObstacle, transform);
        obs[1].SetActive(false);

        obs[2] = Instantiate(m_SO.map_BossObstacle, transform);
        obs[2].SetActive(false);

    }
    public void SpawnObstacle(int index)
    {
        for(int i = 0; i < obsCount; i++)
        {
            obs[i].SetActive(false);
        }
        switch (index)
        {
            case 0:
                obs[index].SetActive(true);
                Debug.Log("jump");
                break;
            case 1:
                obs[index].SetActive(true);
                Debug.Log("slide");
                break;
            case 2:
                obs[index].SetActive(true);
                break;
            default:
                break;
        }

    }
    void FixedUpdate()
    {
        Vector3 moveVec = new Vector3(0, 0, Input.GetAxis("Vertical") * 300f);
        rb.AddForce(-moveVec, ForceMode.Acceleration);
        float minZ = -800f;
        if (GameManager.Instance.playerState == GameManager.PlayerState.normal)
        {
            while (minZ < -799f)
            {
                minZ += 1f;
            }
        }
        else
        {
            while (minZ > -1301f)
            {
                minZ -= 1f;
            }
        }
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, Mathf.Clamp(rb.velocity.z, minZ, 0));
    }
}
