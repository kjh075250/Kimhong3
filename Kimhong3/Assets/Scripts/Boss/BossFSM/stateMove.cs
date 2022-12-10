using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class stateMove : State<BossFSM>
{
    GameObject boss;
    Tween tween;
    public override void OnAwake()
    {
        boss = GameManager.Instance.Boss;
    }
    public override void OnStart()
    {
        Vector3 introVec = new Vector3(boss.transform.position.x, boss.transform.position.y, 200);
        tween = boss.transform.DOMove(introVec, 4f, false);
   }
    public override void OnUpdate(float deltaTime)
    {
        boss.transform.LookAt(GameManager.Instance.Player.transform);
        if(!tween.IsPlaying())
        {
            tween.Kill();
            stateMachine.ChangeState<stateIdle>();
        }
    }
    public override void OnEnd()
    {
        DOTween.Clear();
    }
}
