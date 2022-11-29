using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[CreateAssetMenu(fileName = "New ImageObject", menuName = "Aimpoint/AimpointImage")]
public class AimpointImageSO : ScriptableObject
{
    public Sprite TargetImageDefault;
    public Sprite TargetImageOnAim;
}
