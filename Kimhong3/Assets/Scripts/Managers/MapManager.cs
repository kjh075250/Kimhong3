using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    //�� ���� Ȯ��
    public int mapCount;
    //���� �տ��ִ� ���� �ε���
    int firstMap;
    //���� ��ֹ� ��ȯ�� ����� �ε���
    int index;
    //�⺻ �� ������Ʈ
    public GameObject defaultMap;
    //���� ���� �ڿ��ִ� �� ������Ʈ
    public GameObject lastObject;
    //�ʵ��� ����ִ� �迭
    GameObject[] maps;
    //���� ������ ���� �� �̵��� ��ġ
    Vector3 nextVec;

    private void Awake()
    {
        //��ġ 0���� �ʱ�ȭ
        nextVec = Vector3.zero;
        //���� �տ��ִ� ���� 0(0��°�� �ִ� ��)
        firstMap = 0;
        //�� �迭 �ʱ�ȭ
        maps = new GameObject[mapCount];
    }

    private void Start()
    {
        //�� �迭�� 0��° �ε����� �⺻ �� ������Ʈ�� ��ȯ�� 
        maps[0] = Instantiate(defaultMap);

        //�� ���� ��ŭ ��ȯ
        for (int i = 1; i < mapCount; i++)
        {
            //������ ���� ��ȯ �� ��ġ�� ���� ���� �ڿ��ִ� �� ��ġ�� + 150(���� ũ��)
            nextVec = new Vector3(maps[i-1].transform.position.x, maps[i-1].transform.position.y, maps[i-1].transform.position.z + 150f);
            maps[i] = Instantiate(defaultMap, nextVec, Quaternion.identity);
        }
        //lastObject�� �迭 ������ �ε������ִ� ������Ʈ�� �־���
        lastObject = maps[mapCount - 1];
        //�� ���� �Ϸ� �� lastObject������ �־��� �� ���ӸŴ��� ������ �Լ� ����(lastObject�� ������ ������ ���ܼ�)
        GameManager.Instance.StartCoroutine(GameManager.Instance.SetThunderGage());
    }

    public void MoveMaps()
    {
        //���� �տ��ִ� ���� �ε����� 6�� ������ ��ֹ��� ��ȯ��
        if (firstMap % 6 == 0)
        {
            //���� ��ֹ�, �ٴ� ��ֹ� ���߿� �ϳ��� ��ȯ�ϱ� ���� �ε����� �������� ������
            index = Random.Range(0, 2);
        }
        //�ƴ϶�� ��ȯ���� ����(index�� -1)
        else
        {   
            index = -1;
        }
        //���� �տ��ִ� ��(������ �̵��� ��)�� ���� ���� �ڿ��ִ� �ʿ� +150(���� ũ��) ��ŭ�� ��ġ�� �̵���
        maps[firstMap].transform.position = new Vector3(lastObject.transform.position.x, lastObject.transform.position.y, lastObject.transform.position.z + 150f);
        //��� �̵���Ų ���� lastObject�� �־���
        lastObject = maps[firstMap];

        //��� �̵���Ų ���� ��ֹ� ��ȯ �Լ��� ����
        maps[firstMap].GetComponent<MapControl>().SpawnObstacle(index);
        //���� �տ��ִ� �� �ε��� �� ++(���� �ε�����)
        firstMap++;

        //���� �ε������� ���� �ִ밹�� - 1�� �������� 0���� �ʱ�ȭ
        if (firstMap == mapCount - 1)
        {
            firstMap = 0;
        }
    }

    //���� ���� ������ ���� ��ֹ��� �ٲٴ� �Լ�
    public void SwitchMap()
    {
        //���� �����ϴ� �ʵ��� SwitchObs(��ֹ� ��ġ�� �ٲٴ� �Լ�)�� �����Ŵ
        for(int i = 0; i < mapCount - 1; i++)
        {
            maps[i].GetComponent<MapControl>().SwitchObs();
        }
    }

    //���� ���� �տ��ִ� ���� ��ġ�� -90���� �۴ٸ�(���� �����ߴٸ�)
    private void FixedUpdate()
    {
        if (maps[firstMap].transform.position.z < -90)
        {
            //�� �̵���Ű�� �Լ� ����
            MoveMaps();
        }
    }
}
