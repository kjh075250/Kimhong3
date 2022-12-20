using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    PlayerEffect pe;
    void Start()
    {
        pe = GetComponentInChildren<PlayerEffect>();
    }
    private void OnTriggerEnter(Collider other)
    {
        float tg = GameManager.Instance.thunderGage;
        if (other.gameObject.CompareTag("Bullet") && GameManager.Instance.playerState == GameManager.PlayerState.normal)
        {
            tg = Mathf.Clamp(tg, 0f, 100f);
            tg -= 30f;
            GameManager.Instance.thunderGage = tg;
        }

        if (other.gameObject.CompareTag("Obstacle"))
        {
            if (GameManager.Instance.playerState == GameManager.PlayerState.normal)
            {
                GameManager.Instance.playerShake.Invoke();
                StartCoroutine(DieEffect());
            }
            else
            {
                pe.BreakEffect();
                GameManager.Instance.cameraShake.Invoke();
                StartCoroutine(BreakingEffect(other.gameObject));
            }
        }
    }
    IEnumerator BreakingEffect(GameObject obj)
    {
        obj.gameObject.GetComponent<ParticleSystem>().Play();
        Time.timeScale = 0.5f;
        yield return new WaitForSecondsRealtime(0.2f);
        obj.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
    IEnumerator DieEffect()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(0.2f);
        Time.timeScale = 0.3f;
        GameManager.Instance.GameOver();
    }
    
}