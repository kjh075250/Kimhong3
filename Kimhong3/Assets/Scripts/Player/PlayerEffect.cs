using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerEffect : MonoBehaviour
{
    //이펙트들 인스펙터에서 받아옴
    [SerializeField]
    ParticleSystem slideEffects;   
    [SerializeField]
    ParticleSystem hoverEffects;
    [SerializeField]
    ParticleSystem dieEffects; 
    [SerializeField]
    ParticleSystem overDriveEffects;
    [SerializeField]
    ParticleSystem[] bossEffects;   
    [SerializeField]
    ParticleSystem obstacleBreakingEffects;

    private void Update()
    {
        //만약 폭주상태라면 슬라이딩 연출과 상승 연출 끄고 폭주 연출 키기
        if(GameManager.Instance.playerState == GameManager.PlayerState.overdrive)
        {
            slideEffects.Stop();
            hoverEffects.gameObject.SetActive(false);
            overDriveEffects.Play();
        }
        //폭주 상태 아니라면 폭주 연출 끄기
        else
        {
            overDriveEffects.Stop();
        }
    }

    //플레이어가 흔들리는 효과
    public void PlayerShake()
    {
        //플레이어 죽을 때 파티클 실행
        dieEffects.Play();
        //플레이어 흔들어주고 끝나면 setActive false
        transform.DOShakePosition(0.7f, 1, 10, 90).OnComplete(() => { gameObject.SetActive(false); Debug.Log("die"); });
        //차량이 돌아가면서 죽는 연출을 위해
        transform.DORotate(Vector3.up, 1f, RotateMode.FastBeyond360);
    }

    //슬라이딩을 연출을 키고 끄는 함수
    public void SlideEffect(bool isSlide)
    {
        //bool을 매개변수로 받아와 끄고키는것을 확인
        if(isSlide) slideEffects.Play();
        else slideEffects.Stop();
    }

    //상승(점프?) 연출을 키고 끄는 함수
    public void HoverEffect(bool ishover)
    {
        //파티클이 loop이기 때문에 setActive로 끄고켰다
        hoverEffects.gameObject.SetActive(ishover);
    }

    //폭주 상태에서 장애물과 부딪혔을 때 이펙트
    public void BreakEffect()
    {
        obstacleBreakingEffects.Play();
    }

    //보스를 공격 할 때 연출을 위한 함수
    public void StartBossatkEffect()
    {
        StartCoroutine(bossAttackEffect());
    }
    IEnumerator bossAttackEffect()
    {
        //카메라 연출이나 다른로직을 잠시 기다리고
        yield return new WaitForSeconds(0.1f);
        //차징 이펙트 연출 후
        bossEffects[0].Play();
        yield return new WaitForSeconds(1.9f);
        //대쉬 연출 실행
        bossEffects[0].Stop();
        bossEffects[1].Play();
    }
}
