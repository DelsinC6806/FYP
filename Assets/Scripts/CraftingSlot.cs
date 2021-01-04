using UnityEngine.UI;
using UnityEngine;

public class CraftingSlot : MonoBehaviour
{
    public Interactable slotInteractable;
    public int slotItemAmount;
    public Text quantity;
    public Image itemImg;


    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (slotItemAmount == 0)
        {
            Destroy(slotInteractable);
        }
    }

    public void AddItem(Interactable i, int itemAmount)
    {
            slotInteractable = Instantiate(i);
            slotItemAmount = itemAmount;
            Invoke("UpdateUI", 0.1f);
    }

    public void onCraftingSlotButton()
    {
        slotInteractable.currentQuantity = slotItemAmount;
        Inventory.instance.Additem(slotInteractable);
        Invoke("RemoveItem", 0f);
    }

    public void RemoveItem()
    {
        slotInteractable = null;
        itemImg.enabled = false;
        quantity.enabled = false;
    }

    void UpdateUI()
    {
        quantity.enabled = true;
        itemImg.enabled = true;
        quantity.text = slotItemAmount.ToString();
        itemImg.sprite = slotInteractable.item.itemImage;
    }
}
