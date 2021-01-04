using UnityEngine;
using UnityEngine.UI;

public class InputAmountUI : MonoBehaviour
{
    InputField inputField;
    Crafting craftingTable;
    Interactable interactable;

    void Start()
    {
        inputField = this.GetComponentInChildren<InputField>();
        craftingTable = FindObjectOfType<Crafting>();
        enableOrDisable(false);
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void onExitButton()
    {
        enableOrDisable(false);
    }

    public void onConfirmButton()
    {
        AddItemToCraftSlot(int.Parse(inputField.text));
        enableOrDisable(false);
    }

    public void AddItemToCraftSlot(int inputAmount)
    {
        for (int i = 0; i < craftingTable.craftingSlots.Length; i++)
        {
            if (craftingTable.craftingSlots[i].slotInteractable == null)
            {

                if (inputAmount <= interactable.currentQuantity)
                {
                    craftingTable.craftingSlots[i].AddItem(interactable, inputAmount);
                    interactable.currentQuantity -= inputAmount;
                    break;
                }
                else
                {
                   //warning for slots that do NOT have enough amount of item
                }

                if (interactable.currentQuantity == 0)
                {
                    interactable.RemoveFromInventory();
                    Inventory.instance.OnItemChangedCallBack();
                }

            }
        }
    }
    
    public void recieveItem(Interactable interact)
    {
        interactable = interact;
    }

    public void enableOrDisable(bool a)
    {
        var img = GetComponentsInChildren<Image>();
        var txt = GetComponentsInChildren<Text>();
        if (inputField.enabled != a)
        {
            inputField.enabled = a;
        }
            foreach (Image image in img)
            {
                image.enabled = a;
            }
            foreach (Text text in txt)
            {
                text.enabled = a;
            }

    }
}
