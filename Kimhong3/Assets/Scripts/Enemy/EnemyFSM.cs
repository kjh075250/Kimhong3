using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private Transform parentPos;
    private float distance;

    private void Start()
    {
        parentPos = GetComponentInParent<Transform>();
        distance = GetDistance(player.transform.position.x, player.transform.position.y,
            this.transform.position.x, this.transform.position.y);
        StartCoroutine(MovingSinCos());
    }

    void Update()
    {
    }

    public IEnumerator MovingSinCos()
    {
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

}
