using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Transform player;
    Vector2 mouseVec;
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void LateUpdate()
    {
        SetRotation();
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
}
