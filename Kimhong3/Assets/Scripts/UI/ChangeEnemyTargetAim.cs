using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeEnemyTargetAim : MonoBehaviour
{
    //현재 이미지
    Image myImage;
    //타겟 에임 이미지 so
    public AimpointImageSO aimpoint;
    //현재 에임 포인트와 닿아있는가
    public bool IsOnAim;

    private void Start()
    {
        //이미지 캐싱 후 sprite를 so 속 default로 만듬
        myImage = GetComponent<Image>();
        myImage.sprite = aimpoint.TargetImageDefault;
    }

    private void FixedUpdate()
    {
        //이미지에 콜라이더가 에임 포인트와 닿아있지 않다면 default로
        myImage.sprite = aimpoint.TargetImageDefault;
    }

    public void ChangeAimImage()
    {
        //에임 포인트가 닿아있다면 OnAim sprite로 바꿔줌
        myImage.sprite = aimpoint.TargetImageOnAim;
    }
}
