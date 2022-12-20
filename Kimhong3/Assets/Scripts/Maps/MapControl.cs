using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MapControl : MonoBehaviour
{
    int obsCount;
    Rigidbody rb;
    GameObject[] obs;
    GameObject nowObs;
    WaitForSeconds wait;
    public MapSO m_SO;
    private void Awake()
    {
        obsCount = 2;
        rb = GetComponent<Rigidbody>();
        obs = new GameObject[obsCount];
        wait = new WaitForSeconds(1f);
    }
    void Start()
    {
        nowObs = obs[0];
        obs[0] = Instantiate(m_SO.map_JumpObstacle, transform);
        obs[0].SetActive(false);

        obs[1] = Instantiate(m_SO.map_SlideObstacle, transform);
        obs[1].SetActive(false);
    }
    public void SpawnObstacle(int index)
    {
        obs[0].transform.DOMoveY(0, 0);
        obs[1].transform.DOMoveY(8, 0);
        for(int i = 0; i < obsCount; i++)
        {
            obs[i].SetActive(false);
        }
        switch (index)
        {
            case 0:
                obs[index].SetActive(true);
                nowObs = obs[index];
                break;
            case 1:
                obs[index].SetActive(true);
                nowObs = obs[index];
                break;
            default:
                break;
        }

    }
    public void SwitchObs()
    {
        if (nowObs == null) return;
        StartCoroutine(SwitchCoroutine());
    }
    IEnumerator SwitchCoroutine()
    {
        if (nowObs.transform.position.y < 8)
        {
            nowObs.transform.DOShakePosition(1f, 2f, 10, 90);
            yield return wait;
            nowObs.transform.DOMoveY(8, 0.07f);
            GameManager.Instance.cameraShake.Invoke();
        }
        else
        {
            nowObs.transform.DOShakePosition(1f, 2f, 10, 90);
            yield return wait;
            nowObs.transform.DOMoveY(0, 0.07f);
            nowObs.GetComponent<ParticleSystem>().Play();
            GameManager.Instance.cameraShake.Invoke();
        }
    }
    void FixedUpdate()
    {
        Vector3 moveVec = new Vector3(0, 0, Input.GetAxis("Vertical") * 300f);
        rb.AddForce(-moveVec, ForceMode.Acceleration);
        float minZ = -500f;
        if (GameManager.Instance.playerState == GameManager.PlayerState.normal || GameManager.Instance.playerState == GameManager.PlayerState.normal)
        {
            while (minZ <= -500f)
            {
                minZ += 1f;
            }
        }
        else
        {
            while (minZ >= -1000f)
            {
                minZ -= 1f;
            }
        }
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, Mathf.Clamp(rb.velocity.z, minZ, 0));
    }
}
