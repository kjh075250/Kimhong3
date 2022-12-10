using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFSM : MonoBehaviour
{
    protected StateMachine<BossFSM> fsmManager;
    public StateMachine<BossFSM> FsmManager => fsmManager;

    protected Animator animator;

    protected virtual void Start()
    {
        fsmManager = new StateMachine<BossFSM>(this, new stateMove());
        fsmManager.AddStateList(new stateIdle());
        fsmManager.AddStateList(new stateFirstAttack());
        fsmManager.AddStateList(new stateSecondAttack());
    }

    protected virtual void Update()
    {
        fsmManager.Update(Time.deltaTime);
    }
}
