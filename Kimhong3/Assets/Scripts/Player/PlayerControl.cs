using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class PlayerControl : MonoBehaviour
{
    //�ڷ�ƾ�� ����� waitforseconds ���� ����(new ��� ���Ϸ���)
    WaitForSeconds wait;
    //�÷��̾� ����Ʈ�� ���� ��ũ��Ʈ
    PlayerEffect playerEffect;
    //������ ���� ������ ���� rigidbody
    Rigidbody rb;

    //�뽬 ������ üũ�ϴ� bool
    protected bool isDashing = false;
    //������ �����Ÿ� �̳��� �ִ��� üũ�ϴ� bool
    protected bool canBossAttack = false;

    private void Start()
    {
        //wait �ð� �����ְ�, ĳ��
        wait = new WaitForSeconds(2f);
        rb = GetComponent<Rigidbody>();
        playerEffect = GetComponentInChildren<PlayerEffect>();
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.playerState != GameManager.PlayerState.bossAttack)
        {
            Move();
        }
        //���� �÷��̾� ���°� ���� ����̰� ����Ʈ�� �����̽��� ������ �뽬
        if (GameManager.Instance.playerState == GameManager.PlayerState.overdrive)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.Space))
            {
                Dash();
            }
        }

    }
    void Move()
    {
        //���� ������ �����̱����� ��ǲ���� �޾ƿ�
        Vector3 horVec = new Vector3(Input.GetAxis("Horizontal") * 15f, 0, 0);

        //���� ������ �����϶� ���� �������� ������ ���� 
        Vector3 rotateVec = new Vector3(0, 0, Input.GetAxisRaw("Horizontal") * 30f);
        Tweener rotTween = transform.DORotate(-rotateVec, 1f);
        rotTween.Play();

        //�ڵ����� ���̸� �����ϱ� ���� �ٴڿ� ray�� ��� ���̸� ������
        RaycastHit hit;
        //ray�� �ٴڿ� �¾Ҵٸ�
        if (Physics.Raycast(transform.position, -transform.up, out hit, 100f, LayerMask.GetMask("Ground")))
        {
            //�ƹ��� �ൿ�� ���� �ʰų� ���ָ�� �� �� 2.5f
            float PlayerY = hit.point.y + 2.5f;
            //���� �븻��� �� ��
            if (GameManager.Instance.playerState == GameManager.PlayerState.normal)
            {
                //����Ʈ�� ������ �����̵�
                if (Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.Space))
                {
                    PlayerY = hit.point.y + 0.4f;
                    playerEffect.SlideEffect(true);
                }
                else playerEffect.SlideEffect(false);

                //�����̽��ٸ� ������ ���
                if (Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.LeftShift))
                {
                    PlayerY = hit.point.y + 5f;
                    playerEffect.HoverEffect(true);
                }
                else playerEffect.HoverEffect(false);
            }
            transform.DOMoveY(PlayerY, 0.2f);
        }
        //���Ϳ� ��� translate�� ��������
        Vector3 playerVec = (horVec * Time.deltaTime * 2.5f);
        transform.Translate(playerVec);
    }


    void Dash()
    {
        //���� �뽬���̶�� ����
        if (isDashing)
        {
            return;
        }
        StartCoroutine(Dashing());
    }

    IEnumerator Dashing()
    {
        isDashing = true;
        //���� ������ �����Ÿ� �̳��� �ִٸ� �������� �̺�Ʈ ȣ��
        if (canBossAttack)
        {
            Debug.Log("BossAttack");
            GameManager.Instance.bossAttack.Invoke();
        }
        //������ �Ŀ� wait��ŭ ��ٷ���
        transform.DOMoveZ(20f, 0.2f).OnComplete(() => transform.DOMoveZ(0f, 1.5f));
        yield return wait;
        isDashing = false;
    }

    public void StartBossAttackDashing()
    {
        StartCoroutine(BossAttackDashing());
    }

    IEnumerator BossAttackDashing()
    {
        yield return wait;
        transform.DOMoveZ(40f, 0.2f).OnComplete(() =>
        {
            GameManager.Instance.GameClear.Invoke();
            transform.DOMove((Vector3.back + Vector3.up) * 50f, 3f);
        });

    }
    private void OnTriggerStay(Collider col)
    {
        //���� ���� Ʈ���� �̳��� ���� �ִٸ�
        if (col.gameObject.CompareTag("Boss"))
        {
            canBossAttack = true;
        }
    }
    void OnTriggerExit(Collider col)
    {
        //���� Ʈ���� �ȿ��� �����ų� �������� �ʴٸ�
        if (col.gameObject.CompareTag("Boss"))
        {
            canBossAttack = false;
        }
    }
}

