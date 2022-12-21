using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stateFirstAttack : State<BossFSM>
{
    //보스 패턴에 사용할 오브젝트들
    GameObject[] obs;
    //현재 상태 진행시간
    float time;
    public override void OnAwake()
    {
        //오브젝트들의 데이터를 게임매니저에서 가져옴
        obs = GameManager.Instance.bossObs;
    }
    public override void OnStart()
    {
        //시간 0으로 초기화 해주고,
        time=0;
        //랜덤으로 왼쪽, 오른쪽 두개의 장애물 중 하나의 장애물을 소환하는 함수를 실행
        obs[Random.Range(0, 2)].GetComponent<BossObstacle>().StartPatternCo();
    }
    public override void OnUpdate(float deltaTime)
    {
        time += deltaTime;

        //시간이 2초 지났다면 idle로 바꿈
        if (time > 2f)
        {
            stateMachine.ChangeState<stateIdle>();
        }
    }
    public override void OnEnd()
    {

    }
}
