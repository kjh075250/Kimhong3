using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    //카메라 위치를 조정 할 때 사용할 오브젝트의 transform을 가져옴
    public Transform Campos;
    //보스를 공격 중인지 bool
    bool isBossAttack;

    //보스를 공격 할 떄 사용 할 연출을 위한 카메라
    [SerializeField]
    Camera subcam;

    //마우스의 위치를 담을 벡터
    Vector2 mouseVec;

    void Awake()
    {
        //커서 위치 잠금
        Cursor.lockState = CursorLockMode.Locked;
        //연출을 위한 카메라 꺼두기
        subcam.gameObject.SetActive(false);
    }
    void LateUpdate()
    {
        //보스를 공격중이 아니라면 화면을 마우스로 움직일 수 있게
        if(!isBossAttack) SetRotation();
    }
    void SetRotation()
    {
        //GetAxis를 이용해 마우스 위치를 받아옴
        mouseVec.x = Input.GetAxis("Mouse X");
        mouseVec.y = -Input.GetAxis("Mouse Y");

        //mouseVec의 길이가 0이 아니라면
        if(mouseVec.magnitude != 0)
        {
            //쿼터니언에 현재 회전값 저장해두고
            Quaternion q = Campos.rotation;
            //각도에 현재 마우스 벡터값을 더해줌
            q.eulerAngles = new Vector3(q.eulerAngles.x + mouseVec.y * 3f, q.eulerAngles.y + mouseVec.x * 3f, q.eulerAngles.z);
            //이를 다시 campPos에 회전값으로 만들어줌
            Campos.rotation = q;
        }
    }

    //보스를 공격하는 연출을 위한 ㅎ마수
    public void StartBossAttack()
    {
        StartCoroutine(bossAttack());
    }

    IEnumerator bossAttack()
    {
        //보스 어택을 true로 만들어 화면 회전이 더 이상 되지않게
        isBossAttack = true;
        //원래 사용하던 파티클 연출(화면 집중선 같은 것) 끄기
        ParticleSystem pa = GetComponentInChildren<ParticleSystem>();
        pa.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.1f);

        //연출을 위한 카메라 오브젝트 켜줌
        subcam.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.8f);

        //연출 끝나면 카메라 오브젝트 꺼줌
        subcam.gameObject.SetActive(false); 
        pa.gameObject.SetActive(true);
        isBossAttack = false;   
    }
}
