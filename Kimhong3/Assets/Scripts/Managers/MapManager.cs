using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    //맵 갯수 확인
    public int mapCount;
    //가장 앞에있는 맵의 인덱스
    int firstMap;
    //맵의 장애물 소환에 사용할 인덱스
    int index;
    //기본 맵 오브젝트
    public GameObject defaultMap;
    //현재 가장 뒤에있는 맵 오브젝트
    public GameObject lastObject;
    //맵들을 담고있는 배열
    GameObject[] maps;
    //맵이 끝까지 갔을 떄 이동할 위치
    Vector3 nextVec;

    private void Awake()
    {
        //위치 0으로 초기화
        nextVec = Vector3.zero;
        //가장 앞에있는 맵은 0(0번째에 있는 맵)
        firstMap = 0;
        //맵 배열 초기화
        maps = new GameObject[mapCount];
    }

    private void Start()
    {
        //맵 배열에 0번째 인덱스에 기본 맵 오브젝트를 소환함 
        maps[0] = Instantiate(defaultMap);

        //맵 갯수 만큼 소환
        for (int i = 1; i < mapCount; i++)
        {
            //다음에 맵을 소환 할 위치는 현재 가장 뒤에있는 맵 위치에 + 150(맵의 크기)
            nextVec = new Vector3(maps[i-1].transform.position.x, maps[i-1].transform.position.y, maps[i-1].transform.position.z + 150f);
            maps[i] = Instantiate(defaultMap, nextVec, Quaternion.identity);
        }
        //lastObject에 배열 마지막 인덱스에있는 오브젝트를 넣어줌
        lastObject = maps[mapCount - 1];
        //맵 생성 완료 후 lastObject값까지 넣어준 뒤 게임매니저 게이지 함수 실행(lastObject가 없으면 오류가 생겨서)
        GameManager.Instance.StartCoroutine(GameManager.Instance.SetThunderGage());
    }

    public void MoveMaps()
    {
        //가장 앞에있는 맵의 인덱스가 6의 배수라면 장애물을 소환함
        if (firstMap % 6 == 0)
        {
            //공중 장애물, 바닥 장애물 둘중에 하나를 소환하기 위해 인덱스를 랜덤으로 정해줌
            index = Random.Range(0, 2);
        }
        //아니라면 소환하지 않음(index를 -1)
        else
        {   
            index = -1;
        }
        //가장 앞에있는 맵(끝까지 이동한 맵)은 현재 가장 뒤에있는 맵에 +150(맵의 크기) 만큼에 위치로 이동함
        maps[firstMap].transform.position = new Vector3(lastObject.transform.position.x, lastObject.transform.position.y, lastObject.transform.position.z + 150f);
        //방금 이동시킨 맵을 lastObject에 넣어줌
        lastObject = maps[firstMap];

        //방금 이동시킨 맵의 장애물 소환 함수를 실행
        maps[firstMap].GetComponent<MapControl>().SpawnObstacle(index);
        //가장 앞에있는 맵 인덱스 값 ++(다음 인덱스로)
        firstMap++;

        //만약 인덱스값이 맵의 최대갯수 - 1과 같아지면 0으로 초기화
        if (firstMap == mapCount - 1)
        {
            firstMap = 0;
        }
    }

    //보스 패턴 공격중 맵의 장애물을 바꾸는 함수
    public void SwitchMap()
    {
        //현재 존재하는 맵들의 SwitchObs(장애물 위치를 바꾸는 함수)를 실행시킴
        for(int i = 0; i < mapCount - 1; i++)
        {
            maps[i].GetComponent<MapControl>().SwitchObs();
        }
    }

    //만약 가장 앞에있는 맵의 위치가 -90보다 작다면(끝에 도달했다면)
    private void FixedUpdate()
    {
        if (maps[firstMap].transform.position.z < -90)
        {
            //맵 이동시키는 함수 실행
            MoveMaps();
        }
    }
}
