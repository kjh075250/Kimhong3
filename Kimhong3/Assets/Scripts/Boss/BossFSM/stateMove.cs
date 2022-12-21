using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class stateMove : State<BossFSM>
{
    //보스 오브젝트를 가져옴
    GameObject boss;
    //보스 처음 등장했을 때 움직임을 위해 tween을 이용
    Tween tween;
    public override void OnAwake()
    {
        //보스 오브젝트 데이터를 게임매니저에서 가져옴
        boss = GameManager.Instance.Boss;
    }
    public override void OnStart()
    {
        //처음 등장 시 이동할 위치를 정하고 이동함
        Vector3 introVec = new Vector3(boss.transform.position.x, boss.transform.position.y, 600);
        tween = boss.transform.DOMove(introVec, 4f, false);
   }
    public override void OnUpdate(float deltaTime)
    {
        //플레이어를 바라보며 움직이게 함
        boss.transform.LookAt(GameManager.Instance.Player.transform);
        //플레이 끝나면 트윈 kill 후 idle 상태로 감
        if(!tween.IsPlaying())
        {
            tween.Kill();
            stateMachine.ChangeState<stateIdle>();
        }
    }
    public override void OnEnd()
    {
    }
}
