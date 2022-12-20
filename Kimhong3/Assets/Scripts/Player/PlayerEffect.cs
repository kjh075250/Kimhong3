using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerEffect : MonoBehaviour
{
    [SerializeField]
    ParticleSystem slideEffects;   
    [SerializeField]
    ParticleSystem hoverEffects;
    [SerializeField]
    ParticleSystem breakEffects; 
    [SerializeField]
    ParticleSystem shieldEffects;
    [SerializeField]
    ParticleSystem[] bossEffects;   
    [SerializeField]
    ParticleSystem defenceEffects;
    private void Update()
    {
        if(GameManager.Instance.playerState == GameManager.PlayerState.overdrive)
        {
            slideEffects.Stop();
            hoverEffects.gameObject.SetActive(false);
            shieldEffects.Play();
        }
        else
        {
            shieldEffects.Stop();
        }
    }

    public void PlayerShake()
    {
        breakEffects.Play();
        transform.DOShakePosition(0.7f, 1, 10, 90).OnComplete(() => { gameObject.SetActive(false); Debug.Log("die"); });
        transform.DORotate(Vector3.up, 1f, RotateMode.FastBeyond360);
    }

    public void SlideEffect(bool isSlide)
    {
        if(isSlide) slideEffects.Play();
        else slideEffects.Stop();
    }

    public void HoverEffect(bool ishover)
    {
        hoverEffects.gameObject.SetActive(ishover);
    }

    public void BreakEffect()
    {
        defenceEffects.Play();
    }
    public void StartBossatkEffect()
    {
        StartCoroutine(bossAttackEffect());
    }
    IEnumerator bossAttackEffect()
    {
        yield return new WaitForSeconds(0.1f);
        bossEffects[0].Play();
        yield return new WaitForSeconds(1.9f);
        bossEffects[0].Stop();
        bossEffects[1].Play();
    }
}
