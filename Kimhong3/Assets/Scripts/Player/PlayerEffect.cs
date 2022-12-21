using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerEffect : MonoBehaviour
{
    //����Ʈ�� �ν����Ϳ��� �޾ƿ�
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
        //���� ���ֻ��¶�� �����̵� ����� ��� ���� ���� ���� ���� Ű��
        if(GameManager.Instance.playerState == GameManager.PlayerState.overdrive)
        {
            slideEffects.Stop();
            hoverEffects.gameObject.SetActive(false);
            overDriveEffects.Play();
        }
        //���� ���� �ƴ϶�� ���� ���� ����
        else
        {
            overDriveEffects.Stop();
        }
    }

    //�÷��̾ ��鸮�� ȿ��
    public void PlayerShake()
    {
        //�÷��̾� ���� �� ��ƼŬ ����
        dieEffects.Play();
        //�÷��̾� �����ְ� ������ setActive false
        transform.DOShakePosition(0.7f, 1, 10, 90).OnComplete(() => { gameObject.SetActive(false); Debug.Log("die"); });
        //������ ���ư��鼭 �״� ������ ����
        transform.DORotate(Vector3.up, 1f, RotateMode.FastBeyond360);
    }

    //�����̵��� ������ Ű�� ���� �Լ�
    public void SlideEffect(bool isSlide)
    {
        //bool�� �Ű������� �޾ƿ� ����Ű�°��� Ȯ��
        if(isSlide) slideEffects.Play();
        else slideEffects.Stop();
    }

    //���(����?) ������ Ű�� ���� �Լ�
    public void HoverEffect(bool ishover)
    {
        //��ƼŬ�� loop�̱� ������ setActive�� �����״�
        hoverEffects.gameObject.SetActive(ishover);
    }

    //���� ���¿��� ��ֹ��� �ε����� �� ����Ʈ
    public void BreakEffect()
    {
        obstacleBreakingEffects.Play();
    }

    //������ ���� �� �� ������ ���� �Լ�
    public void StartBossatkEffect()
    {
        StartCoroutine(bossAttackEffect());
    }
    IEnumerator bossAttackEffect()
    {
        //ī�޶� �����̳� �ٸ������� ��� ��ٸ���
        yield return new WaitForSeconds(0.1f);
        //��¡ ����Ʈ ���� ��
        bossEffects[0].Play();
        yield return new WaitForSeconds(1.9f);
        //�뽬 ���� ����
        bossEffects[0].Stop();
        bossEffects[1].Play();
    }
}
