using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFSM : MonoBehaviour
{
    //stateMachine BossFSM타입을 가진 stateMachine을 만듬
    protected StateMachine<BossFSM> fsmManager;
    public StateMachine<BossFSM> FsmManager => fsmManager;

    protected virtual void Start()
    {
        //stateMove로 init하면서 new stateMachine만들어줌
        fsmManager = new StateMachine<BossFSM>(this, new stateMove());
        //AddStateList에 idle, firstAttack, SecondAttack State에 넣어줌
        fsmManager.AddStateList(new stateIdle());
        fsmManager.AddStateList(new stateFirstAttack());
        fsmManager.AddStateList(new stateSecondAttack());
    }

    protected virtual void Update()
    {
        //state Update에 time.deltaTime을 매개변수로 전해줌
        fsmManager.Update(Time.deltaTime);
    }
}
