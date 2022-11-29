using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeEnemyTargetAim : MonoBehaviour
{
    Image myImage;
    public AimpointImageSO aimpoint;
    public bool IsOnAim;

    private void Start()
    {
        myImage = GetComponent<Image>();
        myImage.sprite = aimpoint.TargetImageDefault;
    }
    private void FixedUpdate()
    {
        myImage.sprite = aimpoint.TargetImageDefault;
    }
    public void ChangeAimImage()
    {
        myImage.sprite = aimpoint.TargetImageOnAim;
    }
}
