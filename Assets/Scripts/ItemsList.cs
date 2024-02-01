using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemsList : MonoBehaviour
{
    public Items selectedItem;

    public string itemName;
    public string itemDescription;

    public bool spriteShown;
    private EventStates eventStates;

    private TextMeshProUGUI bumpWindowObjet;
    private TextMeshProUGUI bumpWindowDescription;
    private Image bumpWindowImage;

    public Sprite itemSprite;
    public Sprite flûteSprite;
    public Sprite castleKeySprite;
    public Sprite bookOfDissonanceSprite;
    public Sprite greatWhiteDiamondSprite;
    public Sprite greatRedDiamondSprite;
    public Sprite greatGreenDiamondSprite;

    void Start()
    {
        eventStates = gameObject.GetComponent<EventStates>();
        bumpWindowObjet = GameObject.Find("BumpWindow").transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        bumpWindowDescription = GameObject.Find("BumpWindow").transform.GetChild(4).GetComponent<TextMeshProUGUI>();
        bumpWindowImage = GameObject.Find("BumpWindow").transform.GetChild(5).GetComponent<Image>();
    }

    public enum Items
    {
        None,
        
        // QuestItem
        Flûte,
        CastleKey,
        BookOfDissonance,
        GreatWhiteDiamond,
        GreatRedDiamond,
        GreatGreenDiamond
    }

    private void OnValidate()
    {
        switch (selectedItem)
        {
            case Items.None:
                itemName = "";
                itemSprite = null;
                itemDescription = "";
                break;
            case Items.Flûte:
                itemName = "Flûte divine";
                itemSprite = flûteSprite;
                itemDescription = "Une flûte en bois, sans aucune imperfection.";
                break;
            case Items.CastleKey:
                itemName = "Clé du château";
                itemSprite = castleKeySprite;
                itemDescription = "Une clé entourée d'une lueur étrange.";
                break;
            case Items.BookOfDissonance:
                itemName = "Livre de la Dissonnance";
                itemSprite = bookOfDissonanceSprite;
                itemDescription = "Un livre ancien mais curieusement bien conservé. Les écritures à l'intérieur sont lisibles mais incompréhensibles.";
                break;
            case Items.GreatWhiteDiamond:
                itemName = "Grand diamant blanc";
                itemSprite = greatWhiteDiamondSprite;
                itemDescription = "Un diamant blanc étincelant. Un pouvoir divin semble en dégager.";
                break;
            case Items.GreatRedDiamond:
                itemName = "Grand diamant rouge";
                itemSprite = greatRedDiamondSprite;
                itemDescription = "Un diamant rouge étincelant. A quoi peut-il bien servir?";
                break;
            case Items.GreatGreenDiamond:
                itemName = "Grand diamant vert";
                itemSprite = greatGreenDiamondSprite;
                itemDescription = "Un diamant vert étincelant. Un sentiment de bien être vous envahi en le touchant.";
                break;
        }
    }

    void Update()
    {
        // Gestion du Sprite à montrer lorsque le coffre s'ouvre
        if(eventStates != null) // Si eventStates = null cela veut dire que le script ItemsList n'est pas attaché à un coffre mais directement à un objet dropable directement dans le jeu
        {
            if(eventStates.isTriggered && !spriteShown)
            {
                spriteShown = true;
                bumpWindowImage.sprite = itemSprite;
                bumpWindowObjet.text = itemName;
                bumpWindowDescription.text = itemDescription;
            }
        }
    }
}
