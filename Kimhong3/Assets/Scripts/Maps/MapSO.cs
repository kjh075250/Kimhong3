using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New MapObject", menuName = "Maps/MapObject")]
public class MapSO : ScriptableObject
{
    //맵의 장애물 데이터를 불러올때 사용할 so
    public GameObject map_SlideObstacle;
    public GameObject map_JumpObstacle;
}
