using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class InventoryController : MonoBehaviour
{
    // public GridLayoutGroup gridLayout;
    // public GameObject[] itemSlots;
    // public GameObject cursor;
    // private Gamepad gamepad;
    // private float timer;
    // private bool timerOn;
    // public int targetSlot;
    // private Vector2 direction;
    // private Inventory inventory;
    // [HideInInspector] public string selectedItemName;
    // PlayerInput input;

    // private TextMeshProUGUI itemName;
    // public TextMeshProUGUI itemDescription;
    // private ConfirmationWindow confirmationWindow;
    // public Image image;
    // private Animator animator;

    // public GameObject ItemSlot;

    // private void Start()
    // {
    //     cursor = GameObject.Find("InventoryCursor");
    //     gamepad = Gamepad.current;
    //     inventory = FindObjectOfType<Inventory>();
    //     animator = GetComponent<Animator>();

    //     input = new PlayerInput();
    //     input.Player.Enable();
    //     input.Player.Action.canceled += x => Action_canceled();

    //     confirmationWindow = FindObjectOfType<ConfirmationWindow>();
    // }

    // void Update()
    // {
    //     if(itemSlots.Length > 0)
    //     {
    //         for (int i = 0; i < itemSlots.Length; i++)
    //         {
    //             itemSlots[i] = ItemSlot.transform.GetChild(i).gameObject;
    //         }
    //     }

    //     if(gamepad != null && inventory.inventoryIsOpen /*&& !confirmationWindow.isOpenForItem*/)
    //     {
    //         direction = gamepad.leftStick.ReadValue();

    //         cursor.transform.position = itemSlots[targetSlot].transform.position;

    //         if(targetSlot < (inventory.items.Count - 1) && direction.x > 0 && !timerOn)
    //         {
    //             targetSlot += 1;
    //             timerOn = true;
    //         }

    //         if(targetSlot > 0 && direction.x < 0 && !timerOn)
    //         {
    //             targetSlot -= 1;
    //             timerOn = true;
    //         }

    //         if(!itemSlots[targetSlot].gameObject.activeSelf)
    //         {
    //             InitializeCursorPosition();
    //         }
    //     }

    //     if(timerOn)
    //     {
    //         timer += Time.unscaledDeltaTime;
    //         if(timer > 0.2f)
    //         {
    //             timerOn = false;
    //             timer = 0;
    //         }
    //     }
    // }

    // public void InitializeCursorPosition()
    // {
    //     cursor.GetComponent<Animator>().SetBool("isTriggered", true);
    //     targetSlot = 0;
    // }

    // public void ResetCursorPosition()
    // {
    //     cursor.GetComponent<Animator>().SetBool("isTriggered", false);
    //     targetSlot = 0;
    // }

    // void Action_canceled()
    // {
    //     confirmationWindow = FindObjectOfType<ConfirmationWindow>();
    //     if(inventory.inventoryIsOpen /*&& !confirmationWindow.isOpenForItem*/)
    //     {
    //         Debug.Log("Item selectionné, la boite de confirmation doit s'ouvrir");
    //         SelectItem();
    //     }
    // }

    // void SelectItem()
    // {
    //     //Ouvre une boite de dialogue demandant au joueur s'il confirme l'utilisation
    //     selectedItemName = itemSlots[targetSlot].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text;
    //     /*confirmationWindow.OpenConfirmationWindowForItem(selectedItemName);*/
    // }

    // public void UseItem()
    // {
    //     if(itemSlots[targetSlot].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text == "Grand diamant blanc")
    //     {
    //         if(FindObjectOfType<PowerSlotsManager>().powerSlots.Length < FindObjectOfType<PowerSlotsManager>().maxNumberOfSlots)
    //         {
    //             Debug.Log("Le joueur utilise un diamant blanc depuis l'inventaire");
    //             UseGreatWhiteDiamond();
    //             inventory.RemoveItem("Grand diamant blanc");
    //         }
    //         else
    //         {
    //             Debug.Log("Le nombre maximum de slots de pouvoir est atteint");
    //         }
    //     }

    //     if(itemSlots[targetSlot].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text == "Livre de la Dissonance")
    //     {
    //         Debug.Log("Rien ne se passe");
    //     }

    //     //Add
    // }

    // void UseGreatWhiteDiamond()
    // {
    //     PowerSlotsManager powerSlotsManager = FindObjectOfType<PowerSlotsManager>();
    //     powerSlotsManager.currentSlotCount += 1;

    //     // Met à jour le nombre de slots de départ pour les prochaines scenes
    //     powerSlotsManager.startingSlots = powerSlotsManager.currentSlotCount;
    //     powerSlotsManager.SetPowerSlots();
    // }
}

