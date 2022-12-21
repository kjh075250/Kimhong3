using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    //보스 오브젝트가 뒤로 계속 움직이게 하는 코드
    void Update()
    {
        //현재 위치가 30보다 클때(플레이어와 가까이 있지 않을때)
        if (transform.position.z > 30)
        {
            //플레이어 속도에 반비례해 뒤로 가는 속도가 빨라진다.
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + Time.deltaTime * (60f - GameManager.Instance.GetSpeed() * 0.1f));
        }
    }
}
