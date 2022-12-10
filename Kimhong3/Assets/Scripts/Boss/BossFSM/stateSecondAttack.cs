using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stateSecondAttack : State<BossFSM>
{
    float time;
    public override void OnAwake()
    {

    }
    public override void OnStart()
    {
        time = 0;
        GameManager.Instance._MapManager.SwitchMap();
    }
    public override void OnUpdate(float deltaTime)
    {
        time += deltaTime;

        if (time > 5f)
        {
            stateMachine.ChangeState<stateIdle>();
        }
    }
    public override void OnEnd()
    {

    }
}
