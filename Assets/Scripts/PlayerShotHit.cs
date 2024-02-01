using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShotHit : MonoBehaviour
{
    public LayerMask enemyStuff;
    public float damage;
    public GameObject hitExplosionPrefab;
    public float playerShotSpeed;

    void Update()
    {
        transform.position += transform.right * playerShotSpeed * Time.deltaTime;
    }
    
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if(((1 << hitInfo.gameObject.layer) & enemyStuff) != 0)
        {
            if(!hitInfo.isTrigger)
            {
                hitInfo.gameObject.GetComponent<Health>().TakeDamage(damage);
                hitInfo.gameObject.GetComponent<DamageEffect>().VisualDamageEffect();
                Instantiate(hitExplosionPrefab, transform.position, transform.rotation);
                Destroy(gameObject, 0f);
            }
        }
    }
}
