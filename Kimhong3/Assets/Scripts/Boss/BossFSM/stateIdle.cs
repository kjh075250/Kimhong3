using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stateIdle : State<BossFSM>
{
    //���� ���°� ����� �ð�
    float time;
    public override void OnAwake()
    {
    }
    public override void OnStart()
    {
        //�ð� 0���� �ʱ�ȭ
        time = 0f;
    }
    public override void OnUpdate(float deltaTime)
    {
        time += deltaTime;
        int randomAttack = Random.Range(0,2);
        //�ð��� 3�ʰ� �����ٸ� randomAttack�� ���� ���� �������� ù��° Ȥ�� �ι�° ������ ������
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
