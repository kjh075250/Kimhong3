using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerEffect : MonoBehaviour
{
    [SerializeField]
    ParticleSystem slideEffects;
    [SerializeField]
    ParticleSystem breakEffects; 
    [SerializeField]
    ParticleSystem shieldEffects;   
    [SerializeField]
    ParticleSystem defenceEffects;
    private void Update()
    {
        if(GameManager.Instance.playerState == GameManager.PlayerState.overdrive)
        {
            slideEffects.Stop();
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
        transform.DOShakePosition(0.3f, 1, 10, 90);
        transform.DORotate(Vector3.up, 1f, RotateMode.FastBeyond360);
    }
    public void SlideEffect(bool isSlide)
    {
        if(isSlide) slideEffects.Play();
        else slideEffects.Stop();
    }
    public void BreakEffect()
    {
        defenceEffects.Play();
    }
}
