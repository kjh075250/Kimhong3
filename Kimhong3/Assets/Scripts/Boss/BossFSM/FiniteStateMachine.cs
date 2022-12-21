using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 
public abstract class State<T>
{ 
    //FSM타입을 담는 StateMachine과 State를 상속하는 클래스 타입을 담는 변수
    protected StateMachine<T> stateMachine;
    protected T stateMachineClass;
    
    //기본 생성자
    public State() { }
     
    //stateMachine과 state를 블러와 OnAwake해줌
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
    // 타입(T)를 이용한 stateMachine변수
    private T stateMachine;
    // state들을 담는 리스트
    private Dictionary<System.Type, State<T>> stateLists  = new Dictionary<System.Type, State<T>>();

    //현재 상태
    private State<T> nowState;  
    public State<T> getNowState => nowState;
     
    //이전 상태
    private State<T> beforeState;
    public State<T> getBeforeState => beforeState;
     
    //state들의 진행된 시간
    private float stateDurationTime = 0.0f;
    public float getStateDurationTime => stateDurationTime;
     
    
    //생성자로 초기화 해줌
    public StateMachine(T stateMachine, State<T> initState)  {
        this.stateMachine = stateMachine;
         
        AddStateList(initState); 
        nowState = initState; 
        nowState.OnStart();
    }
     
    //setMahicneWithClass 해주고 list에 넣어줌
    public void AddStateList(State<T> state) { 
        state.SetMachineWithClass(this, stateMachine); 
        stateLists[state.GetType()] = state;
    } 

    //state진행 시간에 deltaTime을 더해줌
    public void Update(float deltaTime)
    { 
        stateDurationTime += deltaTime; 
        nowState.OnUpdate(deltaTime);
    }
    

    public Q ChangeState<Q>() where Q : State<T>
    { 
        var newType = typeof(Q);

        //nowState 타입이 Q와 같은지
        if (nowState.GetType() == newType)  {  return nowState as Q;  }
         
        //null이 아니라면 현재 상태의 OnEnd를 실행
        if (nowState != null)  {  nowState.OnEnd();  }
         
        //nowstate를 이전상태로만듬
        beforeState = nowState;
        //현재 상태를 새로운 상태로 바꿈
        nowState = stateLists[newType];

        //현재 상태 Onstart
        nowState.OnStart(); 
        stateDurationTime = 0.0f;

        return nowState as Q;
    } 
}
