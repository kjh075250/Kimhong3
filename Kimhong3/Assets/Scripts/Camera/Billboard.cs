using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public bool flagReverse;
    public enum WorldDirection
    {
        up, down, left, right, foward, back
    };
    public WorldDirection direction = WorldDirection.up;

    public Vector3 GetAxis(WorldDirection worldDirection)
    {
        switch (worldDirection)
        { 
            case WorldDirection.up:
                return Vector3.up;
                case WorldDirection.down:
                return Vector3.down;
                case WorldDirection.left:
                return Vector3.left;
                case WorldDirection.right:
                return Vector3.right;
                case WorldDirection.foward:
                    return Vector3.forward;
                case WorldDirection.back:
                return Vector3.back;
        }
        return Vector3.up;

    }
    private void LateUpdate()
    {
        Vector3 cameraRotation = Camera.main.transform.rotation * (flagReverse ? Vector3.forward : Vector3.back);
        Vector3 posTarget = transform.position + cameraRotation;
        Vector3 orientationTarget = Camera.main.transform.rotation * GetAxis(direction);
        transform.LookAt(posTarget, orientationTarget);
    }
}
