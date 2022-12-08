using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class stateIdle : State<BossFSM>
{
    GameObject player;
    GameObject boss;
    float time;
    public override void OnAwake()
    {
        player = GameManager.Instance.Player;
        boss = GameManager.Instance.Boss;
    }
    public override void OnStart()
    {
        boss.transform.DOMoveY(boss.transform.position.y + 15, 2f).SetLoops(-1, LoopType.Yoyo);
        time = 0f;
    }
    public override void OnUpdate(float deltaTime)
    {
        time += deltaTime;
        int randomAttack = Random.Range(0,1);
        if(time > 7f)
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
        DOTween.Clear();
    }
}
