using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteDiamond : MonoBehaviour
{   
    public float healing;
    private PickupObject pickupObject;
    private bool objectUsed;
    public CelestePieceHealth[] celestePieces;

    void Start()
    {
        pickupObject = GetComponent<PickupObject>();
    }

    void Update()
    {
        if(pickupObject.isPickup && !objectUsed)
        {
            objectUsed = true;
            CelesteCockpitHealing();
        }
    }

    void CelesteCockpitHealing()
    {
        GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<CelestePieceHealth>().TakeHeal(healing);

        // celestePieces = FindObjectsOfType<CelestePieceHealth>();
        // foreach (CelestePieceHealth celestePiece in celestePieces)
        // {
        //     if(gameObject.name == "CelesteCockpit")
        //     {
        //         celestePiece.TakeHeal(healing);
        //     }
        // }
        
    }
}
