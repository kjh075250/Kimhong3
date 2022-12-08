using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject bullet;
    private Transform parentPos;
    private float distance;
    private LineRenderer lr;
    private Vector3 cube1Pos, cube2Pos;
    private float curTime;

    private void Start()
    {
        parentPos = GetComponentInParent<Transform>();
        StartCoroutine(MovingSinCos());

        lr = GetComponent<LineRenderer>();
        lr.startWidth = 0.5f;
        lr.endWidth = 0.5f;
        lr.startColor = Color.red;
        lr.endColor = Color.red;

        cube1Pos = gameObject.GetComponent<Transform>().position;
        StartCoroutine(Attack());
    }

    // Update is called once per frame
    void Update()
    {
        curTime += Time.deltaTime;
    }

    public IEnumerator MovingSinCos()
    {
        distance = GetDistance(player.transform.position.x, player.transform.position.y, 
            this.transform.position.x, this.transform.position.y);
        while (true)
        {
            for (int th = 0; th < 360; th++)
            {
                var rad = Mathf.Deg2Rad * th;
                var x = distance * Mathf.Sin(rad);
                var y = distance * Mathf.Cos(rad);
                this.transform.position = new Vector3(x, y, transform.position.z);
                yield return new WaitForSeconds(0.0278f);
            }
        }
    }

    float GetDistance(float x1, float y1, float x2, float y2)
    {
        float width = x2 - x1;
        float height = y2 - y1;

        float distance = width * width + height * height;
        distance = Mathf.Sqrt(distance);

        return distance;
    }

    public IEnumerator Attack()
    {
        yield return new WaitForSeconds(4f);
        lr.enabled = true;
        while (true)
        {
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, GameManager.Instance.Player.transform.position);
            if (curTime >= 5f)
            {
                lr.enabled = false;
                Instantiate(bullet, transform);
                break;
            }   
        }
    }
}
