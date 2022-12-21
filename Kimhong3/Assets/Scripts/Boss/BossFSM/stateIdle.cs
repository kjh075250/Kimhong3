using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stateIdle : State<BossFSM>
{
    //현재 상태가 진행된 시간
    float time;
    public override void OnAwake()
    {
    }
    public override void OnStart()
    {
        //시간 0으로 초기화
        time = 0f;
    }
    public override void OnUpdate(float deltaTime)
    {
        time += deltaTime;
        int randomAttack = Random.Range(0,2);
        //시간이 3초가 지났다면 randomAttack의 값에 따라 랜덤으로 첫번째 혹은 두번째 공격을 실행함
        if(time > 3f)
        {
            switch(randomAttack)
            {
                case 0:
                    stateMachine.ChangeState<stateFirstAttack>();
                    break;
                case 1:
                    stateMachine.ChangeState<stateSecondAttack>();
                    break;
            }
        }
    }
    public override void OnEnd()
    {
    }
}
