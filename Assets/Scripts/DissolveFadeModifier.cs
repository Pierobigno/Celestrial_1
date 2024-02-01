using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DissolveFadeModifier : MonoBehaviour
{   
    private bool dissolveFadeOn;
    private Material dissolveMaterial;

    [Header("Si le material est sur ce GameObject")]
    public bool materialIsOnThisGO;

    [Header("Si le material est sur un autre GameObject")]
    public bool materialIsOnAnotherGO;
    public Image wichImage;
    
    private void Start()
    {
        if(materialIsOnThisGO)
        {
            dissolveMaterial = GetComponent<Image>().material;
        }
        else if (materialIsOnAnotherGO)
        {
            dissolveMaterial = wichImage.material;
        }

        else
        {
            dissolveMaterial = transform.GetChild(0).GetComponent<Image>().material;
        }
    }

    public void DissolveFadeOn()
    {
        dissolveFadeOn = true;
    }
    public void DissolveFadeOff()
    {
        dissolveFadeOn = false;
    }

    private void Update()
    {
        if(dissolveFadeOn && dissolveMaterial != null)
        {
            if(dissolveMaterial.GetFloat("_Fade") < 1)
            {
                dissolveMaterial.SetFloat("_Fade", dissolveMaterial.GetFloat("_Fade") + Time.unscaledDeltaTime * 2);
            }
        }
        else if(!dissolveFadeOn && dissolveMaterial != null)
        {
            if(dissolveMaterial.GetFloat("_Fade") > 0)
            {
                dissolveMaterial.SetFloat("_Fade", dissolveMaterial.GetFloat("_Fade") - Time.unscaledDeltaTime * 2);
            }
        }
    }
}
