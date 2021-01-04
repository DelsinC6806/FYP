
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    Inventory inventory;

    public Transform itemsParent;
    public InventorySlot[] slots;
    Text[] quantity;


    void Start()
    {
        
        inventory = Inventory.instance;
        inventory.OnItemChangedCallBack += UpdateUI;
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    void Update()
    {

    }

    void UpdateUI()
    {
            for (int i = 0; i < slots.Length; i++)
            {
                if (i < inventory.interactables.Count)
                {
                    slots[i].AddItem(inventory.interactables[i]);
                }
                else
                {
                    slots[i].ClearSlot();
                }
            }
    }
}
