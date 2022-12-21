using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MapControl : MonoBehaviour
{
    //�ʿ� �پ��ִ� ��ֹ� ���� ���ϱ�
    int obsCount;
    //�����ӿ� ����� ������ �ٵ�
    Rigidbody rb;
    //�ʿ� �پ����� ��ֹ�
    GameObject[] obs;
    //���� Ȱ��ȭ �Ǿ��ִ� ��ֹ�
    GameObject nowObs;
    //waitForSeconds ���� ���α�
    WaitForSeconds wait;
    //��ֹ� �����͸� �޾ƿ��� ���� so
    public MapSO m_SO;

    private void Awake()
    {
        //ĳ�̰� ��ֹ� ���ڸ� ����
        obsCount = 2;
        rb = GetComponent<Rigidbody>();
        obs = new GameObject[obsCount];
        wait = new WaitForSeconds(1f);
    }
    void Start()
    {
        //���� ��ֹ��� �ϴ� obs[0]���� �ʱ�ȭ �س���
        nowObs = obs[0];
        //�� ������Ʈ(�ڽ�)�� ���߿� ��ȯ�� ��ֹ��� ����� obs[0]�� ����
        obs[0] = Instantiate(m_SO.map_JumpObstacle, transform);
        //��Ȱ��ȭ �ص�
        obs[0].SetActive(false);

        //�� ������Ʈ(�ڽ�)�� �ٴڿ� ��ȯ�� ��ֹ��� ����� obs[1]�� ����
        obs[1] = Instantiate(m_SO.map_SlideObstacle, transform);
        //��Ȱ��ȭ �ص�
        obs[1].SetActive(false);
    }

    //���� �� �ڷ� �̵��� �� ���� �� �Լ�
    //�Ű������� �޾ƿ� �ε����� �̿��� �ٴڰ� ���� �� �� �ϳ��� ��ֹ��� �������� ��ȯ
    public void SpawnObstacle(int index)
    {
        //��ֹ� ��ġ �ʱ�ȭ & ��Ȱ��ȭ���� �ٽ� ��
        obs[0].transform.DOMoveY(0, 0);
        obs[1].transform.DOMoveY(8, 0);
        for(int i = 0; i < obsCount; i++)
        {
            obs[i].SetActive(false);
        }

        //�Ű������� �޾ƿ� index�� �̿��� switch ����
        switch (index)
        {
            //index�� 0�̶�� obs[0](���� ��ֹ�)�� Ȱ��ȭ �ϰ� nowObs�� �̸� ����
            case 0:
                obs[index].SetActive(true);
                nowObs = obs[index];
                break;
            //index�� 1�̶�� obs[1](�ٴ� ��ֹ�)�� Ȱ��ȭ �ϰ� nowObs�� �̸� ����
            case 1:
                obs[index].SetActive(true);
                nowObs = obs[index];
                break;
            default:
                break;
        }

    }

    //���� ���� �� �ϳ�. ��ֹ����� ���� ��ġ�� �ٲ�
    public void SwitchObs()
    {
        //nowObs�� null�̸� ����
        if (nowObs == null) return;
        StartCoroutine(SwitchCoroutine());
    }

    IEnumerator SwitchCoroutine()
    {
        //y���� 8���� �۴ٸ�(�Ʒ��� �ִٸ�)
        if (nowObs.transform.position.y < 8)
        {
            //��鸮�� ���� �Ŀ�
            nowObs.transform.DOShakePosition(1f, 2f, 10, 90);
            yield return wait;
            //��ġ�� 8�� �̵�(���������̵�) �� ī�޶� ��鸲 ����
            nowObs.transform.DOMoveY(8, 0.07f);
            GameManager.Instance.cameraShake.Invoke();
        }
        //y���� 8���� ���� �ʴٸ�(���߿� �ִٸ�)
        else
        {
            //��鸮�� ���� �Ŀ�
            nowObs.transform.DOShakePosition(1f, 2f, 10, 90);
            yield return wait;
            //��ġ�� 8�� �̵�(�Ʒ����̵�) �� ī�޶� ��鸲 ����
            nowObs.transform.DOMoveY(0, 0.07f);
            //�׸��� ��ƼŬ �ý����� �ҷ��� ��ƼŬ ����
            nowObs.GetComponent<ParticleSystem>().Play();
            GameManager.Instance.cameraShake.Invoke();
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        //GetAxis�� vectical ���� �޾ƿ��� moveVec ���� ����
        Vector3 moveVec = new Vector3(0, 0, Input.GetAxis("Vertical") * 300f);
        //AddForce�� �̿��� ���� �������� ������(���� �÷��̾� ������ �����̴� ���̴� ���̳ʽ���)
        rb.AddForce(-moveVec, ForceMode.Acceleration);
        //�ִ� �ӵ�
        float minZ;
        //���� �븻���³� �������¶��
        if (GameManager.Instance.playerState == GameManager.PlayerState.normal || GameManager.Instance.playerState == GameManager.PlayerState.godMod)
        {
            //�ִ� �ӵ��� 500
            minZ = -500f;
        }
        //���ֻ����϶� 1000;
        else
        {
            minZ = -1000f;
        }

        //���� ������ �������̶�� �ƿ� �ٷ� ���߰�
        if (GameManager.Instance.playerState == GameManager.PlayerState.bossAttack)
        {
            rb.velocity = Vector3.zero;
        }
        //�ƴ϶�� clamp�� �̿��� rigidbody�� ����
        else
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, Mathf.Clamp(rb.velocity.z, minZ, 0));
        }
    }
}
