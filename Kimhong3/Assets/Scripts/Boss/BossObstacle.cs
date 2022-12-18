using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossObstacle : MonoBehaviour
{
    BossObsSO bossObsSO;
    MeshRenderer mesh;
    Collider col;
    public GameObject go;
    void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
        col = GetComponent<Collider>();
        bossObsSO = GameManager.Instance.obsSO;
        go.SetActive(false);
    }
    void Start()
    {
        mesh.material = bossObsSO.ReadyMat;
        mesh.enabled = false;
        col.enabled = false;
    }
    public void StartPatternCo()
    {
        StartCoroutine(StartPattern());
    }
     IEnumerator StartPattern()
    {
        mesh.enabled = true;
        go.SetActive(true);
        yield return new WaitForSeconds(2f);
        mesh.material = bossObsSO.AttackMat;
        col.enabled = true;
        GameManager.Instance.cameraShake.Invoke();
        yield return new WaitForSeconds(0.5f);
        go.SetActive(false);
        mesh.material = bossObsSO.ReadyMat;
        mesh.enabled = false;
        col.enabled = false;
    }
}
