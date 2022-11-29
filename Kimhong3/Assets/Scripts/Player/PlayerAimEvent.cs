using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAimEvent : MonoBehaviour
{
    public UnityEvent onShooting;

    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.yellow);

        if (Physics.Raycast(ray, out hit))
        {
            if(hit.collider.CompareTag("TargetAim"))
            {
                hit.collider?.GetComponent<ChangeEnemyTargetAim>().ChangeAimImage();
                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log(hit.collider.transform.position);
                    LineRenderer lineRenderer;
                    onShooting.Invoke();
                }
            }

        }
        
    }

}
