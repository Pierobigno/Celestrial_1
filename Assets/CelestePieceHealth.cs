using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EZCameraShake;

public class CelestePieceHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public Image celestePieceUI;

    private bool visualDamageOn = false;
    private bool visualHealingOn = false;
    public Color uiOriginalColor;
    public Color originalColor;
    public Color damageColor;
    public Color healingColor;

    public float invulnerabilityTime = 1f;

    public bool isBroken;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if(isBroken)
        {
            gameObject.SetActive(false);
        }
    }

    public void TakeDamage(float damage)
    {
        if(!transform.root.GetComponent<CharacterStates>().isInvulnerable)
        {
            if(currentHealth <= damage)
            {
                Breaks();
            }
            else
            {
                currentHealth -= damage;

                if(!visualDamageOn)
                {
                    StartCoroutine(GameDamageEffect());
                    StartCoroutine(UIDamageEffect());
                    StartCoroutine(Invulnerability());
                    CameraShaker.Instance.ShakeOnce(10, 10, 0.1f, 1);
                }
            }
        }
        
    }

    public void TakeHeal(float healing)
    {
        currentHealth += healing;
        if(!visualDamageOn)
        {
            StartCoroutine(GameHealingEffect());
            StartCoroutine(UIHealingEffect());
        }     
    }    

    void Breaks()
    {
        if(name == "Cockpit")
        {
            Debug.Log("GAME OVER");
            Destroy(transform.root);
        }
        StartCoroutine(DialogueCoroutine());
        isBroken = true;
    }

    IEnumerator DialogueCoroutine()
    {
        GetComponent<DialogueTrigger>().StartDialogue();
        yield return new WaitForSeconds (1);
        FindObjectOfType<DialogueManager>().EndMessage();
    }

    IEnumerator UIDamageEffect()
    {
        visualDamageOn = true;
        uiOriginalColor = celestePieceUI.color;

        celestePieceUI.color = damageColor;
        yield return new WaitForSeconds(0.1f);
        celestePieceUI.color = uiOriginalColor;
        yield return new WaitForSeconds(0.1f);
        celestePieceUI.color = damageColor;
        yield return new WaitForSeconds(0.1f);
        celestePieceUI.color = uiOriginalColor;
        yield return new WaitForSeconds(0.1f);
        celestePieceUI.color = damageColor;
        yield return new WaitForSeconds(0.1f);
        celestePieceUI.color = uiOriginalColor;
        yield return new WaitForSeconds(0.1f);

        visualDamageOn = false;
    }

    IEnumerator UIHealingEffect()
    {
        visualDamageOn = true;
        uiOriginalColor = celestePieceUI.color;

        celestePieceUI.color = healingColor;
        yield return new WaitForSeconds(0.1f);
        celestePieceUI.color = uiOriginalColor;
        yield return new WaitForSeconds(0.1f);
        celestePieceUI.color = healingColor;
        yield return new WaitForSeconds(0.1f);
        celestePieceUI.color = uiOriginalColor;
        yield return new WaitForSeconds(0.1f);
        celestePieceUI.color = healingColor;
        yield return new WaitForSeconds(0.1f);
        celestePieceUI.color = uiOriginalColor;
        yield return new WaitForSeconds(0.1f);

        visualDamageOn = false;
    }

    IEnumerator GameDamageEffect()
    {
        visualDamageOn = true;

        SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

        originalColor = spriteRenderers[0].color;
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            spriteRenderer.color = damageColor;
        }
        yield return new WaitForSeconds(0.1f);
       foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            spriteRenderer.color = originalColor;
        }
        yield return new WaitForSeconds(0.1f);
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            spriteRenderer.color = damageColor;
        }
        yield return new WaitForSeconds(0.1f);
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            spriteRenderer.color = originalColor;
        }
        yield return new WaitForSeconds(0.1f);
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            spriteRenderer.color = damageColor;
        }
        yield return new WaitForSeconds(0.1f);
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            spriteRenderer.color = originalColor;
        }

        visualDamageOn = false;
    }

    IEnumerator GameHealingEffect()
    {
        visualHealingOn = true;

        SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

        originalColor = spriteRenderers[0].color;
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            spriteRenderer.color = healingColor;
        }
        yield return new WaitForSeconds(0.1f);
       foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            spriteRenderer.color = originalColor;
        }
        yield return new WaitForSeconds(0.1f);
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            spriteRenderer.color = healingColor;
        }
        yield return new WaitForSeconds(0.1f);
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            spriteRenderer.color = originalColor;
        }
        yield return new WaitForSeconds(0.1f);
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            spriteRenderer.color = healingColor;
        }
        yield return new WaitForSeconds(0.1f);
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            spriteRenderer.color = originalColor;
        }

        visualHealingOn = false;
    }

    public IEnumerator Invulnerability()
	{
		transform.root.GetComponent<CharacterStates>().isInvulnerable = true;
		yield return new WaitForSeconds(invulnerabilityTime);
		transform.root.GetComponent<CharacterStates>().isInvulnerable = false;
	}
}
