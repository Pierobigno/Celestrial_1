using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBombExplosion : MonoBehaviour
{
    public LayerMask enemyStuff;
    public float damage;
    public float explosionRadius;

    void Start()
    {
        GetComponent<CircleCollider2D>().radius = explosionRadius;
    }
   
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if(((1 << hitInfo.gameObject.layer) & enemyStuff) != 0)
        {
            hitInfo.gameObject.GetComponent<Health>().TakeDamage(damage);
            hitInfo.gameObject.GetComponent<DamageEffect>().VisualDamageEffect();
        }
    }
}
