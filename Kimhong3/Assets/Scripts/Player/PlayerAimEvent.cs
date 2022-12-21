using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DigitalRuby.LightningBolt;
using UnityEngine.UI;

public class PlayerAimEvent : MonoBehaviour
{
    //번개 공격 연출 할 떄 사용 할 에셋 스크립트
    LightningBoltScript lbScript;

    //번개 공격 연출 할 떄 사용 할 라인 렌더러 불러오기
    LineRenderer lineRenderer;

    //waitforseconds 자주 사용하는 것 미리 저장
    WaitForSeconds wait = new WaitForSeconds(0.2f);

    //공격 오디오
    [SerializeField]
    AudioClip shootAudios;
    void Start()
    {
        //캐싱
        lbScript = GetComponent<LightningBoltScript>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }
    void Update()
    {
        //현재 마우스 위치(화면 중앙)에 Ray를 쏨
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            //만약 Ray를 맞은 collider가 TargetAim Tag라면
            if(hit.collider.CompareTag("TargetAim"))
            {
                //콜라이더에 ChangeEnemyTargetAim 이라는 스크립트가 있다면 
                //타겟 이미지를 바꿔주는 함수 실행(에임점이 이미지에 닿았다면 닿았다는 느낌을 주기 위해)
                hit.collider?.GetComponent<ChangeEnemyTargetAim>().ChangeAimImage();

                //번개 공격 연출을 위해 라인 랜더러의 목표 지점을 TargetAIm tag를 가진 오브젝트의 위치로 지정
                lbScript.EndObject = hit.collider.gameObject;

                //만약 마우스 좌클릭과 에너지 게이지가 5 이상이라면
                if (Input.GetMouseButtonDown(0) && GameManager.Instance.ThunderGage >= 5f)
                {
                    //공격에 성공했다는 뜻이니
                    //에너지 깎는 함수에 음수(-10)를 이용해 에너지를 10 상승시킴
                    GameManager.Instance.DecreaseThunderGage(-10f);

                    //화면 흔들림 연출 이벤트 호출
                    GameManager.Instance.cameraShake.Invoke();

                    //사운드 실행
                    SoundManager.instance.SFXPlay("shoot", shootAudios);

                    //번개 연출과 적 죽는 효과를 위한 코루틴 호출
                    //collider가 적의 이미지에 붙어있어서 부모 오브젝트에 있는 EnemyFSM을 찾아 그 오브젝트를 넘겨줌
                    StartCoroutine(ShootingEffect(hit.collider.gameObject.GetComponentInParent<Canvas>().
                        GetComponentInParent<EnemyFSM>().gameObject));

                }
            }
        }
    }
    IEnumerator ShootingEffect(GameObject obj)
    {
        //라인 렌더러 활성화 하고
        lineRenderer.enabled = true;
        //목표 오브젝트에 파티클 시스템이 있다면 실행
        obj?.GetComponent<ParticleSystem>().Play();
        yield return wait;
        //풀매니저의 적 사망하게 하는 함수 실행
        ObjectPoolManager.ReturnEnemy(obj);
        //라인 렌더러 비활성화 해준다. (번개 공격 연출 끄기)
        lineRenderer.enabled = false;
    }
}
