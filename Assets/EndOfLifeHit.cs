using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfLifeHit : MonoBehaviour
{
    [Header("Gestion des dommages infligés au joueur")]
    public LayerMask player;
    public LayerMask others;
    public float damage;
    public GameObject hitEffectPrefab;
    
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if(((1 << hitInfo.gameObject.layer) & others) != 0)
        {
            if(!hitInfo.isTrigger)
            {
                Destroy(hitInfo.gameObject);
                Debug.Log(hitInfo.gameObject.name + " est pulvérisé par la Fin de La Vie");
                if(hitEffectPrefab != null)
                {
                    Instantiate(hitEffectPrefab, hitInfo.transform.position, transform.rotation);
                }
            }
        }
        else if(((1 << hitInfo.gameObject.layer) & player) != 0)
        {
            Destroy(hitInfo.transform.root.gameObject);
            Debug.Log("GAME OVER");
        }
    }
}
