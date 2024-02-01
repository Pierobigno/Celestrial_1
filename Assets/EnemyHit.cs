using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    [Header("Gestion des dommages infligés au joueur")]
    public LayerMask playerStuff;
    public float damage;
    public GameObject hitEffectPrefab;
    public bool destroyInCaseOfContact;
    
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if(((1 << hitInfo.gameObject.layer) & playerStuff) != 0)
        {
            if(!hitInfo.isTrigger)
            {
                hitInfo.gameObject.GetComponent<CelestePieceHealth>().TakeDamage(damage);

                if(hitEffectPrefab != null)
                {
                    Instantiate(hitEffectPrefab, transform.position, transform.rotation);
                }

                if(destroyInCaseOfContact)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
