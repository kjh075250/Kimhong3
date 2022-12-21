using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stateFirstAttack : State<BossFSM>
{
    //���� ���Ͽ� ����� ������Ʈ��
    GameObject[] obs;
    //���� ���� ����ð�
    float time;
    public override void OnAwake()
    {
        //������Ʈ���� �����͸� ���ӸŴ������� ������
        obs = GameManager.Instance.bossObs;
    }
    public override void OnStart()
    {
        //�ð� 0���� �ʱ�ȭ ���ְ�,
        time=0;
        //�������� ����, ������ �ΰ��� ��ֹ� �� �ϳ��� ��ֹ��� ��ȯ�ϴ� �Լ��� ����
        obs[Random.Range(0, 2)].GetComponent<BossObstacle>().StartPatternCo();
    }
    public override void OnUpdate(float deltaTime)
    {
        time += deltaTime;

        //�ð��� 2�� �����ٸ� idle�� �ٲ�
        if (time > 2f)
        {
            stateMachine.ChangeState<stateIdle>();
        }
    }
    public override void OnEnd()
    {

    }
}
