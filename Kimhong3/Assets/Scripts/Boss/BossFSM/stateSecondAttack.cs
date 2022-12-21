using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stateSecondAttack : State<BossFSM>
{
    //���� ������ ����� �ð��� üũ��
    float time;
    public override void OnAwake()
    {

    }
    public override void OnStart()
    {
        //�� ���°� �����Ҷ� time�� 0���� ����� swtichMAp�Լ��� ����
        time = 0;
        GameManager.Instance._MapManager.SwitchMap();
    }
    public override void OnUpdate(float deltaTime)
    {
        time += deltaTime;
        //�ð��� 3�� �̻� ������ idle�� ���ư�
        if (time > 3f)
        {
            stateMachine.ChangeState<stateIdle>();
        }
    }
    public override void OnEnd()
    {

    }
}
