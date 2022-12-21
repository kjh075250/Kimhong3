using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SObject", menuName = "Boss/BossObj")]
public class BossObsSO : ScriptableObject
{
    //보스 패턴에 사용될 보스의 장애물 데이터
    public Material ReadyMat;
    public Material AttackMat;
    public GameObject Attack1;
    public GameObject Attack2;
}
