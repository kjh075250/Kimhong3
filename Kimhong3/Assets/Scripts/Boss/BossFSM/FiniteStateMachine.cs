using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 
public abstract class State<T>
{ 
    //FSMŸ���� ��� StateMachine�� State�� ����ϴ� Ŭ���� Ÿ���� ��� ����
    protected StateMachine<T> stateMachine;
    protected T stateMachineClass;
    
    //�⺻ ������
    public State() { }
     
    //stateMachine�� state�� ���� OnAwake����
    internal void SetMachineWithClass(StateMachine<T> stateMachine, T stateMachineClass) 
    { 
        this.stateMachine = stateMachine; 
        this.stateMachineClass = stateMachineClass;
         
        OnAwake();
    }
     
    public virtual void OnAwake() { } 
    public virtual void OnStart() { }  
    public abstract void OnUpdate(float deltaTime); 
    public virtual void OnEnd() { }
}
 
public sealed class StateMachine<T>
{  
    // Ÿ��(T)�� �̿��� stateMachine����
    private T stateMachine;
    // state���� ��� ����Ʈ
    private Dictionary<System.Type, State<T>> stateLists  = new Dictionary<System.Type, State<T>>();

    //���� ����
    private State<T> nowState;  
    public State<T> getNowState => nowState;
     
    //���� ����
    private State<T> beforeState;
    public State<T> getBeforeState => beforeState;
     
    //state���� ����� �ð�
    private float stateDurationTime = 0.0f;
    public float getStateDurationTime => stateDurationTime;
     
    
    //�����ڷ� �ʱ�ȭ ����
    public StateMachine(T stateMachine, State<T> initState)  {
        this.stateMachine = stateMachine;
         
        AddStateList(initState); 
        nowState = initState; 
        nowState.OnStart();
    }
     
    //setMahicneWithClass ���ְ� list�� �־���
    public void AddStateList(State<T> state) { 
        state.SetMachineWithClass(this, stateMachine); 
        stateLists[state.GetType()] = state;
    } 

    //state���� �ð��� deltaTime�� ������
    public void Update(float deltaTime)
    { 
        stateDurationTime += deltaTime; 
        nowState.OnUpdate(deltaTime);
    }
    

    public Q ChangeState<Q>() where Q : State<T>
    { 
        var newType = typeof(Q);

        //nowState Ÿ���� Q�� ������
        if (nowState.GetType() == newType)  {  return nowState as Q;  }
         
        //null�� �ƴ϶�� ���� ������ OnEnd�� ����
        if (nowState != null)  {  nowState.OnEnd();  }
         
        //nowstate�� �������·θ���
        beforeState = nowState;
        //���� ���¸� ���ο� ���·� �ٲ�
        nowState = stateLists[newType];

        //���� ���� Onstart
        nowState.OnStart(); 
        stateDurationTime = 0.0f;

        return nowState as Q;
    } 
}
