using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stateSecondAttack : State<BossFSM>
{
    //현재 상태의 진행된 시간을 체크함
    float time;
    public override void OnAwake()
    {

    }
    public override void OnStart()
    {
        //이 상태가 시작할때 time을 0으로 만들고 swtichMAp함수를 실행
        time = 0;
        GameManager.Instance._MapManager.SwitchMap();
    }
    public override void OnUpdate(float deltaTime)
    {
        time += deltaTime;
        //시간이 3초 이상 지나면 idle로 돌아감
        if (time > 3f)
        {
            stateMachine.ChangeState<stateIdle>();
        }
    }
    public override void OnEnd()
    {

    }
}
