using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject bullet;

    private float distance;
    private LineRenderer lr;
    private float curTime = 0f;
    private RaycastHit hit;

    private void Start()
    {
        StartCoroutine(MovingSinCos());

        lr = GetComponent<LineRenderer>();
        lr.startWidth = .05f;
        lr.endWidth = .05f;
        lr.enabled = false;

        StartCoroutine(Attack());
    }

    void Update()
    {
        curTime += Time.deltaTime;
    }

    public IEnumerator MovingSinCos()
    {
        int th = Random.Range(-1, 358);
        distance = GetDistance(player.transform.position.x, player.transform.position.y, 
            this.transform.position.x, this.transform.position.y);
        while (true)
        {
            for (th += 1; th < 360 + th; th++)
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
        if (Physics.Raycast(transform.position, GameManager.Instance.Player.transform.position, out hit, 30f))
        {
            //hit.transform.gameObject.CompareTag()
        }
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(3f, 6f));
            lr.enabled = true;
            while (true)
            {
                lr.SetPosition(0, transform.position);
                lr.SetPosition(1, GameManager.Instance.Player.transform.position);
                if (curTime >= 5f) break;
                yield return null;
            }
            lr.enabled = false;
            Instantiate(bullet, transform);
        }
    }
}
