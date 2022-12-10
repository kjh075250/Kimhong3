using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stateIdle : State<BossFSM>
{
    GameObject boss;
    float time;
    public override void OnAwake()
    {
        boss = GameManager.Instance.Boss;
    }
    public override void OnStart()
    {
        time = 0f;
    }
    public override void OnUpdate(float deltaTime)
    {
        time += deltaTime;
        int randomAttack = Random.Range(0,2);
        if(time > 5f)
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
