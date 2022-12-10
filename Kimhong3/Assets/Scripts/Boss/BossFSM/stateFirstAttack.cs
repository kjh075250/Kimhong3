using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stateFirstAttack : State<BossFSM>
{
    GameObject[] obs;
    float time;
    public override void OnAwake()
    {
        obs = GameManager.Instance.bossObs;
    }
    public override void OnStart()
    {
        time=0;
        obs[Random.Range(0, 2)].GetComponent<BossObstacle>().StartPatternCo();
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
