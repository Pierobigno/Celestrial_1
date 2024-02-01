using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamepadButtons : MonoBehaviour
{
    public Sprite[] sprites;
    public string[] spriteNames;

    public string currentSpriteName;

    private Image image;

    void Awake()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        if (image == null)
        {
            image = GetComponent<Image>();
        }

        for (int i = 0; i < spriteNames.Length; i++)
        {
            if (spriteNames[i] == currentSpriteName)
            {
                image.sprite = sprites[i];
                break;
            }
        }
    }
}