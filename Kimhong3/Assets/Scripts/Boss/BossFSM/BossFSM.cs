using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFSM : MonoBehaviour
{
    //stateMachine BossFSMŸ���� ���� stateMachine�� ����
    protected StateMachine<BossFSM> fsmManager;
    public StateMachine<BossFSM> FsmManager => fsmManager;

    protected virtual void Start()
    {
        //stateMove�� init�ϸ鼭 new stateMachine�������
        fsmManager = new StateMachine<BossFSM>(this, new stateMove());
        //AddStateList�� idle, firstAttack, SecondAttack State�� �־���
        fsmManager.AddStateList(new stateIdle());
        fsmManager.AddStateList(new stateFirstAttack());
        fsmManager.AddStateList(new stateSecondAttack());
    }

    protected virtual void Update()
    {
        //state Update�� time.deltaTime�� �Ű������� ������
        fsmManager.Update(Time.deltaTime);
    }
}
