using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSound : MonoBehaviour
{
    public void PlaySFX(int soundIndex)
    {
        FindObjectOfType<AudioManager>().PlaySfx(soundIndex);
    }
}
