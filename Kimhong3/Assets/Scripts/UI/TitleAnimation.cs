using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TitleAnimation : MonoBehaviour
{
    [SerializeField]
    private Image title;
    static Sequence sequenceFadeInOut;

    void Start()
    {
        sequenceFadeInOut = DOTween.Sequence()
        .SetAutoKill(false)
        .OnRewind(() =>
        {
            title.enabled = true;
        })
        .Append(title.DOFade(1.0f, 0.2f))
        .Append(title.DOFade(0.0f, 0.2f))
        .OnComplete(() =>
        {
            title.enabled = false;
        }).SetLoops(-1, LoopType.Incremental);
    }

    void Update()
    {
        
    }
}
