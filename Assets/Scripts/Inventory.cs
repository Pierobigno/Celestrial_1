using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class Inventory : MonoBehaviour
{
    [SerializeField] public List<AvailableItem> availableItems = new List<AvailableItem>();
    [SerializeField] public List<EquippedItem> equippedItems = new List<EquippedItem>();
    [SerializeField] public List<GameObject> availableItemSlots = new List<GameObject>();
    [SerializeField] public List<GameObject> equippedItemSlots = new List<GameObject>();
    public bool isOpen;
    public GameObject inventory;
    private Vector2 direction;
    private PlayerInput input;
    private Gamepad gamepad;
    public GameObject availableItemSlotPrefab;
    public GameObject equippedItemSlotPrefab;
    public RectTransform availableItemsGrid;
    public RectTransform equippedItemsGrid;
    public GameObject cursor;
    public int targetButton;
    private float timer;
    private bool timerOn;
    private float buttonTimer;
    private bool buttonTimerOn;
    public bool onAvailableObjects = true;
    public bool onEquippedObjects;


    private void Awake()
    {
        gamepad = Gamepad.current;
    }

    void Start()
    {
        input = new PlayerInput();
        input.Lucian.Enable();
        input.Lucian.Inventory.canceled += x => Inventory_canceled();
        input.Lucian.AvailableObjects.canceled += x => AvailableObjects_canceled();
        input.Lucian.EquippedObjects.canceled += x => EquippedObjects_canceled();
        input.Lucian.Action.canceled += x => Action_canceled();
    }

    void Update()
    {
        if(gamepad != null && isOpen)
        {
            direction = gamepad.leftStick.ReadValue() * -1;

            if(onAvailableObjects)
            {
                if(availableItemSlots.Count > 0)
                {
                    cursor.transform.position = availableItemSlots[targetButton].transform.position;
                }

                if(targetButton < (availableItemSlots.Count - 1) && direction.y > 0 && !timerOn)
                {
                    targetButton += 1;
                    timerOn = true;
                }

                if(targetButton > 0 && direction.y < 0 && !timerOn)
                {
                    targetButton -= 1;
                    timerOn = true;
                }
            }

            else if(onEquippedObjects)
            {
                if(equippedItemSlots.Count > 0)
                {
                    cursor.transform.position = equippedItemSlots[targetButton].transform.position;
                }

                if(targetButton < (equippedItemSlots.Count - 1) && direction.y > 0 && !timerOn)
                {
                    targetButton += 1;
                    timerOn = true;
                }

                if(targetButton > 0 && direction.y < 0 && !timerOn)
                {
                    targetButton -= 1;
                    timerOn = true;
                }
            }          
        }

        if(timerOn)
        {
            timer += Time.unscaledDeltaTime;
            if(timer > 0.2f)
            {
                timerOn = false;
                timer = 0;
            }
        }
        
        if(buttonTimerOn)
        {
            buttonTimer += Time.unscaledDeltaTime;
            if(buttonTimer > 0.5f)
            {
                buttonTimer = 0;
                buttonTimerOn = false;
            }
        }
    }

    public void AddAvailableItem(string itemName, Sprite itemSprite, string itemDescription)
    {
        AddAvailableItemInClass(itemName, itemSprite, itemDescription);
        AddAvailableItemInInventory();
    }

    void AddAvailableItemInClass(string itemName, Sprite itemSprite, string itemDescription)
    {
        foreach (AvailableItem availableItem in availableItems)
        {
            if (availableItem.name == itemName)
            {
                Debug.Log(availableItem.name + " est déjà dans les objets disponibles");
                return;
            }
        }
        availableItems.Add(new AvailableItem(itemName, itemSprite, itemDescription));
    }

    void AddAvailableItemInInventory()
    {
        foreach (AvailableItem availableItem in availableItems)
        {
            if(!availableItem.isAddedToAList)
            {
                GameObject availableItemSlot = Instantiate(availableItemSlotPrefab, availableItemsGrid.position, availableItemsGrid.rotation);
                availableItemSlot.transform.SetParent(availableItemsGrid, false); //Le deuxième argument false garanti que la position locale et la rotation de l'enfant ne sont pas modifiées.
                availableItemSlot.transform.GetChild(2).GetComponent<Image>().sprite = availableItem.itemSprite;
                availableItemSlot.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = availableItem.name;
                availableItemSlot.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = availableItem.itemDescription;
                Debug.Log("Slot d'item disponible créé dans sa Grid");
                availableItemSlots.Add(availableItemSlot); // Ajoute à la liste pour naviguer avec le curseur
                availableItem.isAddedToAList = true;
            }
        }
    }

    public void RemoveAvailableItem(string itemName)
    {
        RemoveAvailableItemFromInventory(itemName);
        RemoveAvailableItemFromClass(itemName);    
    }

    void RemoveAvailableItemFromClass(string itemName)
    {
        Debug.Log("Supprime " + itemName + " de la classe des items disponibles");
        AvailableItem itemToRemove = availableItems.Find(item => item.name == itemName);
        if (itemToRemove != null)
        {
            availableItems.Remove(itemToRemove);
        }
    }

    void RemoveAvailableItemFromInventory(string itemName)
    {
        Debug.Log("Supprime " + itemName + " des items disponibles de l'inventaire");
        int childCount = availableItemsGrid.transform.childCount;
        for (int i = childCount - 1; i >= 0; i--)
        {
            GameObject child = availableItemsGrid.transform.GetChild(i).gameObject;
            if(child.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text == itemName)
            Destroy(child);
        }
    }

    public void AddEquippedItem(string itemName, Sprite itemSprite, string itemDescription)
    {
        AddEquippedItemInClass(itemName, itemSprite, itemDescription);
        AddEquippedItemInInventory();
    }

    public void AddEquippedItemInClass(string itemName, Sprite itemSprite, string itemDescription)
    {
        foreach (EquippedItem equippedItem in equippedItems)
        {
            if (equippedItem.name == itemName)
            {
                Debug.Log(equippedItem.name + " est déjà dans les objets équipés");
                return;
            }
        }
        equippedItems.Add(new EquippedItem(itemName, itemSprite, itemDescription));
    }

    void AddEquippedItemInInventory()
    {
        foreach (EquippedItem equippedItem in equippedItems)
        {
            if(!equippedItem.isAddedToAList)
            {
                GameObject equippedItemSlot = Instantiate(equippedItemSlotPrefab, equippedItemsGrid.position, equippedItemsGrid.rotation);
                equippedItemSlot.transform.SetParent(equippedItemsGrid, false); //Le deuxième argument false garanti que la position locale et la rotation de l'enfant ne sont pas modifiées.
                equippedItemSlot.transform.GetChild(2).GetComponent<Image>().sprite = equippedItem.itemSprite;
                equippedItemSlot.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = equippedItem.name;
                equippedItemSlot.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = equippedItem.itemDescription;
                Debug.Log("Slot d'item disponible créé dans sa Grid");
                equippedItemSlots.Add(equippedItemSlot); // Ajoute à la liste pour naviguer avec le curseur
                equippedItem.isAddedToAList = true;
            }
        }
    }


    public void RemoveEquippedItem(string itemName)
    {
        RemoveEquippedItemFromInventory(itemName);
        RemoveEquippedItemFromClass(itemName);
    }

    void RemoveEquippedItemFromClass(string itemName)
    {
        Debug.Log("Supprime " + itemName + " de la classe des items équipés");
        EquippedItem itemToRemove = equippedItems.Find(item => item.name == itemName);
        if (itemToRemove != null)
        {
            equippedItems.Remove(itemToRemove);
        }
    }

    void RemoveEquippedItemFromInventory(string itemName)
    {
        Debug.Log("Supprime " + itemName + " des items équipés de l'inventaire");
        GameObject itemToRemove = equippedItemSlots.Find(slot => slot.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text == itemName);
        if (itemToRemove != null)
        {
            equippedItemSlots.Remove(itemToRemove);
            Destroy(itemToRemove);
        }
    }

    public void CleanLists()
    {
        availableItems.RemoveAll(item => item == null);
        equippedItems.RemoveAll(item => item == null);
        availableItemSlots.RemoveAll(item => item == null);
        equippedItemSlots.RemoveAll(item => item == null);
        InitializeCursorPosition();
    }

    public void Inventory_canceled()
    {
        OpenInventory();
    }

    public void AvailableObjects_canceled()
    {
        SwitchCursorToAvailable();
    }

    void SwitchCursorToAvailable()
    {
        CleanLists();
        onAvailableObjects = true;
        onEquippedObjects = false;
    }
    
    public void EquippedObjects_canceled()
    {
        SwitchCursorToEquipped();
        InitializeCursorPosition();
    }

    void SwitchCursorToEquipped()
    {
        CleanLists();
        onAvailableObjects = false;
        onEquippedObjects = true;
    }

    public void Action_canceled()
    {
        if(isOpen)
        {
            SwitchObjectPlace();
        }
    }

    public void OpenInventory()
    {
        if(isOpen)
        {
            inventory.SetActive(false);
            isOpen = false;
        }
        else
        {
            inventory.SetActive(true);
            isOpen = true;
            InitializeCursorPosition();
        }
    }

    void SwitchObjectPlace()
    {
        StartCoroutine(SwitchObjectPlaceCoroutine());            
    }

    IEnumerator SwitchObjectPlaceCoroutine()
    {
        if(onAvailableObjects)
        {
            string itemName = availableItemSlots[targetButton].transform.GetChild(3).GetComponent<TextMeshProUGUI>().text; //Récupère le nom de l'object (depuis le slot)
            string itemDescription = availableItemSlots[targetButton].transform.GetChild(4).GetComponent<TextMeshProUGUI>().text; //Récupère la description de l'object (depuis le slot)
            Sprite itemSprite = availableItemSlots[targetButton].transform.GetChild(2).GetComponent<Image>().sprite; //Récupère le sprite de l'object (depuis le slot)
            RemoveAvailableItem(itemName);
            CleanLists();
            yield return new WaitForSeconds(0.01f);
            AddEquippedItem(itemName, itemSprite, itemDescription);
            SwitchCursorToEquipped();
        }

        else
        {
            string itemName = equippedItemSlots[targetButton].transform.GetChild(3).GetComponent<TextMeshProUGUI>().text; //Récupère le nom de l'object (depuis le slot)
            string itemDescription = equippedItemSlots[targetButton].transform.GetChild(4).GetComponent<TextMeshProUGUI>().text; //Récupère la description de l'object (depuis le slot)
            Sprite itemSprite = equippedItemSlots[targetButton].transform.GetChild(2).GetComponent<Image>().sprite; //Récupère le sprite de l'object (depuis le slot)
            RemoveEquippedItem(itemName);
            CleanLists();
            yield return new WaitForSeconds(0.01f);
            AddAvailableItem(itemName, itemSprite, itemDescription);
            SwitchCursorToAvailable();
        }
    }

    public void InitializeCursorPosition()
    {
        targetButton = 0;
    }
}
