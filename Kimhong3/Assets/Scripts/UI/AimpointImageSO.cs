using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[CreateAssetMenu(fileName = "New ImageObject", menuName = "Aimpoint/AimpointImage")]
public class AimpointImageSO : ScriptableObject
{
    //에임 포인트가 닿았을 때 이미지를 바꿔주기 위한 스크립트에 들어가는 이미지
    public Sprite TargetImageDefault;
    public Sprite TargetImageOnAim;
}
