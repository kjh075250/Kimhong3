using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossObstacle : MonoBehaviour
{
    //보스 패턴에 사용되는 장애물에 사용될 데이터들 so로 받아옴
    BossObsSO bossObsSO;

    //매쉬 랜더러, 콜라이더, 오브젝트등을 받아옴
    MeshRenderer mesh;
    Collider col;
    public GameObject go;

    void Awake()
    {
        //컴포넌트 캐싱
        mesh = GetComponent<MeshRenderer>();
        col = GetComponent<Collider>();
        //게임매니저에 있는 so데이터를 가져옴
        bossObsSO = GameManager.Instance.obsSO;
        go.SetActive(false);
    }
    void Start()
    {
        //현재 오브젝트의 매터리얼을 준비 매터리얼로 만들어줌
        mesh.material = bossObsSO.ReadyMat;
        //패턴이 시작하기 전엔 모두 꺼준다
        mesh.enabled = false;
        col.enabled = false;
    }
    //보스에서 실행되는 장애물 소환 코드
    public void StartPatternCo()
    {
        StartCoroutine(StartPattern());
    }
     IEnumerator StartPattern()
    {
        //장애물 오브젝트의 매쉬 랜더러와 게임오브젝트를 켜주고
        mesh.enabled = true;
        go.SetActive(true);
        yield return new WaitForSeconds(2f);
        //2초 뒤에 콜라이더를 켜주며 매쉬 랜더러의 메터리얼을 반투명한 매터리얼에서 아예 붉은색 매터리얼로 바꿔줌
        mesh.material = bossObsSO.AttackMat;
        col.enabled = true;
        //카메라 흔들리는 연출의 이벤트 호출
        GameManager.Instance.cameraShake.Invoke();
        yield return new WaitForSeconds(0.5f);
        //0.5초 뒤 게임오브젝트와 매쉬, 콜라이더 모두 꺼줌
        go.SetActive(false);
        mesh.material = bossObsSO.ReadyMat;
        mesh.enabled = false;
        col.enabled = false;
    }
}
