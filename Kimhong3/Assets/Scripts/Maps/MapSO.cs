using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New MapObject", menuName = "Maps/MapObject")]
public class MapSO : ScriptableObject
{
    //���� ��ֹ� �����͸� �ҷ��ö� ����� so
    public GameObject map_SlideObstacle;
    public GameObject map_JumpObstacle;
}
