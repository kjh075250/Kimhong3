using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New MapObject", menuName = "Maps/MapObject")]
public class MapSO : ScriptableObject
{
    public GameObject map_Default;
    public GameObject map_SlideObstacle;
    public GameObject map_JumpObstacle;
    public GameObject map_BossObstacle;
}
