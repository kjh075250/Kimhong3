using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DigitalRuby.LightningBolt;
using UnityEngine.UI;

public class PlayerAimEvent : MonoBehaviour
{
    //���� ���� ���� �� �� ��� �� ���� ��ũ��Ʈ
    LightningBoltScript lbScript;

    //���� ���� ���� �� �� ��� �� ���� ������ �ҷ�����
    LineRenderer lineRenderer;

    //waitforseconds ���� ����ϴ� �� �̸� ����
    WaitForSeconds wait = new WaitForSeconds(0.2f);

    //���� �����
    [SerializeField]
    AudioClip shootAudios;
    void Start()
    {
        //ĳ��
        lbScript = GetComponent<LightningBoltScript>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }
    void Update()
    {
        //���� ���콺 ��ġ(ȭ�� �߾�)�� Ray�� ��
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            //���� Ray�� ���� collider�� TargetAim Tag���
            if(hit.collider.CompareTag("TargetAim"))
            {
                //�ݶ��̴��� ChangeEnemyTargetAim �̶�� ��ũ��Ʈ�� �ִٸ� 
                //Ÿ�� �̹����� �ٲ��ִ� �Լ� ����(�������� �̹����� ��Ҵٸ� ��Ҵٴ� ������ �ֱ� ����)
                hit.collider?.GetComponent<ChangeEnemyTargetAim>().ChangeAimImage();

                //���� ���� ������ ���� ���� �������� ��ǥ ������ TargetAIm tag�� ���� ������Ʈ�� ��ġ�� ����
                lbScript.EndObject = hit.collider.gameObject;

                //���� ���콺 ��Ŭ���� ������ �������� 5 �̻��̶��
                if (Input.GetMouseButtonDown(0) && GameManager.Instance.ThunderGage >= 5f)
                {
                    //���ݿ� �����ߴٴ� ���̴�
                    //������ ��� �Լ��� ����(-10)�� �̿��� �������� 10 ��½�Ŵ
                    GameManager.Instance.DecreaseThunderGage(-10f);

                    //ȭ�� ��鸲 ���� �̺�Ʈ ȣ��
                    GameManager.Instance.cameraShake.Invoke();

                    //���� ����
                    SoundManager.instance.SFXPlay("shoot", shootAudios);

                    //���� ����� �� �״� ȿ���� ���� �ڷ�ƾ ȣ��
                    //collider�� ���� �̹����� �پ��־ �θ� ������Ʈ�� �ִ� EnemyFSM�� ã�� �� ������Ʈ�� �Ѱ���
                    StartCoroutine(ShootingEffect(hit.collider.gameObject.GetComponentInParent<Canvas>().
                        GetComponentInParent<EnemyFSM>().gameObject));

                }
            }
        }
    }
    IEnumerator ShootingEffect(GameObject obj)
    {
        //���� ������ Ȱ��ȭ �ϰ�
        lineRenderer.enabled = true;
        //��ǥ ������Ʈ�� ��ƼŬ �ý����� �ִٸ� ����
        obj?.GetComponent<ParticleSystem>().Play();
        yield return wait;
        //Ǯ�Ŵ����� �� ����ϰ� �ϴ� �Լ� ����
        ObjectPoolManager.ReturnEnemy(obj);
        //���� ������ ��Ȱ��ȭ ���ش�. (���� ���� ���� ����)
        lineRenderer.enabled = false;
    }
}
