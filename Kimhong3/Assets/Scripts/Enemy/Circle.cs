using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    private Transform parentPos;
    private void Start()
    {
        StartCoroutine(MovingSinCos());
    }
    public IEnumerator MovingSinCos()
    {
        while (true)
        {
            for (int th = 0; th < 360; th++)
            {
                var rad = Mathf.Deg2Rad * th;
                var x = 3 * Mathf.Sin(rad);
                var y = 3 * Mathf.Cos(rad);
                this.transform.position = new Vector2(x, y);
                yield return new WaitForSeconds(0.0278f);
            }
        }
    }
}
