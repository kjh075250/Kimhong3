using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class stateMove : State<BossFSM>
{
    //���� ������Ʈ�� ������
    GameObject boss;
    //���� ó�� �������� �� �������� ���� tween�� �̿�
    Tween tween;
    public override void OnAwake()
    {
        //���� ������Ʈ �����͸� ���ӸŴ������� ������
        boss = GameManager.Instance.Boss;
    }
    public override void OnStart()
    {
        //ó�� ���� �� �̵��� ��ġ�� ���ϰ� �̵���
        Vector3 introVec = new Vector3(boss.transform.position.x, boss.transform.position.y, 600);
        tween = boss.transform.DOMove(introVec, 4f, false);
   }
    public override void OnUpdate(float deltaTime)
    {
        //�÷��̾ �ٶ󺸸� �����̰� ��
        boss.transform.LookAt(GameManager.Instance.Player.transform);
        //�÷��� ������ Ʈ�� kill �� idle ���·� ��
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
