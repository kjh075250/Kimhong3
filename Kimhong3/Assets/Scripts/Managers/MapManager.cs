using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public int mapCount;
    int firstMap;
    int index;
    public GameObject defaultMap;
    public GameObject lastObject;
    GameObject[] maps;
    Vector3 nextVec;
    private void Awake()
    {
        nextVec = Vector3.zero;
        firstMap = 0;
        maps = new GameObject[mapCount];
    }

    private void Start()
    {
        maps[0] = Instantiate(defaultMap);
        for (int i = 1; i < mapCount; i++)
        {
            nextVec = new Vector3(maps[i-1].transform.position.x, maps[i-1].transform.position.y, maps[i-1].transform.position.z + 150f);
            maps[i] = Instantiate(defaultMap, nextVec, Quaternion.identity);
        }
        lastObject = maps[mapCount - 1];
        GameManager.Instance.StartCoroutine(GameManager.Instance.SetThunderGage());
    }

    public void MoveMaps()
    {
        if (firstMap % 6 == 0)
        {
            index = Random.Range(0, 2);
        }
        else
        {   
            index = 5;
        }
        maps[firstMap].transform.position = new Vector3(lastObject.transform.position.x, lastObject.transform.position.y, lastObject.transform.position.z + 150f);
        lastObject = maps[firstMap];

        maps[firstMap].GetComponent<MapControl>().SpawnObstacle(index);
        firstMap++;
        if (firstMap == mapCount - 1)
        {
            firstMap = 0;
        }
    }

    public void SwitchMap()
    {
        for(int i = 0; i < mapCount - 1; i++)
        {
            maps[i].GetComponent<MapControl>().SwitchObs();
        }
    }
    private void FixedUpdate()
    {
        if (maps[firstMap].transform.position.z < -90)
        {
            MoveMaps();
        }
    }
}
