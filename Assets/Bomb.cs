using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class Bomb : MonoBehaviour
{
    public float bombSpeed;
    public float timeBeforeExplode;
    public GameObject bombExplosionPrefab;

    [Header("Gestion de la vibration de caméra")]
    public float cameraShakeMagn = 1f;
    public float cameraShakeRough = 1f;
    public float cameraShakeFadeIn = 0.1f;
    public float cameraShakeFadeOut = 1f;

    void Start()
    {
        StartCoroutine(Explode());
    }

    void Update()
    {
        transform.position += transform.right * bombSpeed * Time.deltaTime;
    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(timeBeforeExplode);
        Instantiate(bombExplosionPrefab, transform.position, transform.rotation);
        CameraShaker.Instance.ShakeOnce(cameraShakeMagn, cameraShakeRough, cameraShakeFadeIn, cameraShakeFadeOut);
        Destroy(gameObject);
    }
}
