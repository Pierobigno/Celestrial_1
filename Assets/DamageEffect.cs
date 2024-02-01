using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEffect : MonoBehaviour
{
    private SpriteRenderer[] spriteRenderers;
    private ParticleSystem[] particleSystems;
    public Color flashColor;
    private Color originalColor;
    private bool visualDamageOn;

    void Awake()
    {
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        particleSystems = GetComponentsInChildren<ParticleSystem>();
    }

    public void VisualDamageEffect()
    {
        if(!visualDamageOn)
        {
            if(spriteRenderers.Length > 0)
            {
                StartCoroutine(SpriteRendererDamageEffect());
            }
            else if(particleSystems.Length > 0)
            {
                StartCoroutine(ParticleSystemDamageEffect());
            }
        }     
    }

    IEnumerator SpriteRendererDamageEffect()
    {
        visualDamageOn = true;

        originalColor = spriteRenderers[0].color;
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            spriteRenderer.color = flashColor;
        }
        yield return new WaitForSeconds(0.1f);
       foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            spriteRenderer.color = originalColor;
        }
        yield return new WaitForSeconds(0.1f);
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            spriteRenderer.color = flashColor;
        }
        yield return new WaitForSeconds(0.1f);
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            spriteRenderer.color = originalColor;
        }
        yield return new WaitForSeconds(0.1f);
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            spriteRenderer.color = flashColor;
        }
        yield return new WaitForSeconds(0.1f);
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            spriteRenderer.color = originalColor;
        }

        visualDamageOn = false;
    }

    IEnumerator ParticleSystemDamageEffect()
    {
        visualDamageOn = true;

        originalColor = particleSystems[0].main.startColor.color;
        foreach (ParticleSystem particleSystem in particleSystems)
        {
            var mainModule = particleSystem.main;
            mainModule.startColor = flashColor;
        }
        yield return new WaitForSeconds(0.1f);
        foreach (ParticleSystem particleSystem in particleSystems)
        {
            var mainModule = particleSystem.main;
            mainModule.startColor = flashColor;
        }
        yield return new WaitForSeconds(0.1f);
        foreach (ParticleSystem particleSystem in particleSystems)
        {
            var mainModule = particleSystem.main;
            mainModule.startColor = flashColor;
        }
        yield return new WaitForSeconds(0.1f);
        foreach (ParticleSystem particleSystem in particleSystems)
        {
            var mainModule = particleSystem.main;
            mainModule.startColor = flashColor;
        }
        yield return new WaitForSeconds(0.1f);
        foreach (ParticleSystem particleSystem in particleSystems)
        {
            var mainModule = particleSystem.main;
            mainModule.startColor = flashColor;
        }
        yield return new WaitForSeconds(0.1f);
        foreach (ParticleSystem particleSystem in particleSystems)
        {
            var mainModule = particleSystem.main;
            mainModule.startColor = flashColor;
        }

        visualDamageOn = false;
    }
}
