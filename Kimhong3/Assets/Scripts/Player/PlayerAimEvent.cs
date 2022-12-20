using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DigitalRuby.LightningBolt;
using UnityEngine.UI;

public class PlayerAimEvent : MonoBehaviour
{
    public UnityEvent onShooting;
    LightningBoltScript lbScript;
    LineRenderer lineRenderer;
    WaitForSeconds wait = new WaitForSeconds(0.2f);
    void Start()
    {
        lbScript = GetComponent<LightningBoltScript>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }
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
                lbScript.EndObject = hit.collider.gameObject;
                if (Input.GetMouseButtonDown(0) && GameManager.Instance.ThunderGage >= 5f)
                {
                    GameManager.Instance.DecreaseThunderGage(5f);
                    GameManager.Instance.DecreaseThunderGage(-10f);
                    StartCoroutine(ShootingEffect(hit.collider.gameObject.GetComponentInParent<Canvas>().
                        GetComponentInParent<EnemyFSM>().gameObject));
                    onShooting.Invoke();
                }
            }
        }
    }
    IEnumerator ShootingEffect(GameObject obj)
    {
        lineRenderer.enabled = true;
        obj.GetComponent<ParticleSystem>().Play();
        yield return wait;
        ObjectPoolManager.ReturnEnemy(obj);
        lineRenderer.enabled = false;
    }
}
