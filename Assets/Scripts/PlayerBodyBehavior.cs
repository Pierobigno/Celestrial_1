using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBodyBehavior : MonoBehaviour
{
    private LucianMovement lucianMovement;

    public float kinematicTime;

    public Renderer rightLegBio;
    public Renderer rightLegMeca;
    public Renderer leftLegBio;
    public Renderer leftLegMeca;
    public Renderer rightArmBio;
    public Renderer rightArmMeca;
    public Renderer leftArmBio;
    public Renderer leftArmMeca;

    void Awake()
    {
        lucianMovement = GetComponent<LucianMovement>();
    }

    void Start()
    {
        rightLegBio = transform.Find("PlayerInGame").GetChild(8).GetComponent<Renderer>();
        rightLegMeca = transform.Find("PlayerInGame").GetChild(9).GetComponent<Renderer>();
        leftLegBio = transform.Find("PlayerInGame").GetChild(10).GetComponent<Renderer>();
        leftLegMeca = transform.Find("PlayerInGame").GetChild(12).GetComponent<Renderer>();
        rightArmBio = transform.Find("PlayerInGame").GetChild(1).GetComponent<Renderer>();
        rightArmMeca = transform.Find("PlayerInGame").GetChild(0).GetComponent<Renderer>();
        leftArmBio = transform.Find("PlayerInGame").GetChild(14).GetComponent<Renderer>();
        leftArmMeca = transform.Find("PlayerInGame").GetChild(15).GetComponent<Renderer>();
    }

    void Update()
    {
        if(lucianMovement.isFacingRight)
        {
            rightLegBio.enabled = true;
            leftLegMeca.enabled = true;
            rightArmBio.enabled = false;
            leftArmMeca.enabled = false;

            rightLegMeca.enabled = false;
            leftLegBio.enabled = false;
            rightArmMeca.enabled = true;
            leftArmBio.enabled = true;
        }

        if(!lucianMovement.isFacingRight)
        {
            rightLegBio.enabled = false;
            leftLegMeca.enabled = false;
            rightArmBio.enabled = true;
            leftArmMeca.enabled = true;

            rightLegMeca.enabled = true;
            leftLegBio.enabled = true;
            rightArmMeca.enabled = false;
            leftArmBio.enabled = false;
        }
    }

    public void LaunchTimer()
    {
        StartCoroutine(KinematicToDynamic());
    }

    IEnumerator KinematicToDynamic()
    {
        yield return new WaitForSeconds(kinematicTime);
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }
}
