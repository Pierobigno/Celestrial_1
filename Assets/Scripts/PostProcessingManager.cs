using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessingManager : MonoBehaviour
{

    public Volume volume;

    public bool vignetteIsActive;

    public bool bloomDirtIsActive;

    public float dirtIntensity;

    [HideInInspector]
    public Bloom bloom;
    public Vignette vignette;

    void Start()
    {
        vignetteIsActive = false;
        volume.profile.TryGet<Bloom>(out bloom);
        volume.profile.TryGet<Vignette>(out vignette);
    }

    void Update()
    {
        if(vignetteIsActive)
        {
            vignette.intensity.value += (0.5f * Time.unscaledDeltaTime);

            if(vignette.intensity.value == 1)
            {
                vignette.intensity.value = 1;
            }
        }

        if(!vignetteIsActive)
        {
            vignette.intensity.value -= (0.5f * Time.unscaledDeltaTime);

            if(vignette.intensity.value == 0)
            {
                vignette.intensity.value = 0;
            }            
        }


        if(bloomDirtIsActive)
        {
            bloom.dirtIntensity.value += (10 * Time.unscaledDeltaTime);

            if(bloom.dirtIntensity.value > dirtIntensity)
            {
                bloom.dirtIntensity.value = dirtIntensity;
            }
        }

        if(bloomDirtIsActive == false)
        {
            bloom.dirtIntensity.value -= (10 * Time.unscaledDeltaTime);

            if(bloom.dirtIntensity.value <= 0)
            {
                bloom.dirtIntensity.value = 0;
            }  
        }
    }
}
