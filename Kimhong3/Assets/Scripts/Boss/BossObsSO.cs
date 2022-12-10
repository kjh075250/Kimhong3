using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SObject", menuName = "Boss/BossObj")]
public class BossObsSO : ScriptableObject
{
    public Material ReadyMat;
    public Material AttackMat;
    public GameObject Attack1;
    public GameObject Attack2;
}
