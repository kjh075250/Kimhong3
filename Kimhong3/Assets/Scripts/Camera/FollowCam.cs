using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Transform player;
    bool isBossAttack;

    [SerializeField]
    Camera subcam;

    Vector2 mouseVec;
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        subcam.gameObject.SetActive(false);
    }
    void LateUpdate()
    {
        if(!isBossAttack) SetRotation();
    }
    void SetRotation()
    {
        mouseVec.x = Input.GetAxis("Mouse X");
        mouseVec.y = -Input.GetAxis("Mouse Y");

        if(mouseVec.magnitude != 0)
        {
            Quaternion q = player.rotation;
            q.eulerAngles = new Vector3(q.eulerAngles.x + mouseVec.y * 3f, q.eulerAngles.y + mouseVec.x * 3f, q.eulerAngles.z);
            player.rotation = q;
        }
    }

    public void StartBossAttack()
    {
        StartCoroutine(bossAttack());
    }

    IEnumerator bossAttack()
    {
        isBossAttack = true;
        ParticleSystem pa = GetComponentInChildren<ParticleSystem>();
        pa.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.1f);

        subcam.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.8f);

        subcam.gameObject.SetActive(false); ;
        pa.gameObject.SetActive(true);
        isBossAttack = false;   
    }
}
