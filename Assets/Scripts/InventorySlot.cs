using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image icon, DurabilityUI;
    public Button removeButton, itemButton;
    public Text quantity;

    GameObject player, itemDetail;
    Interactable interactable;
    HandSlot handslot;
    InputAmountUI inputAmountUI;



    void Start()
    {
        player = GameObject.FindWithTag("Player");
        handslot = FindObjectOfType<HandSlot>();
        itemDetail = GameObject.Find("itemDetail");

        inputAmountUI = FindObjectOfType<InputAmountUI>();
    }

    public void AddItem(Interactable newInteractable)
    {
        interactable = newInteractable;
        removeButton.interactable = true;
        icon.sprite = interactable.item.itemImage;
        icon.enabled = true;
        quantity.text = newInteractable.currentQuantity.ToString();
        quantity.enabled = true;
    }

    public void ClearSlot()
    {
        interactable = null;
        removeButton.interactable = false;
        icon.sprite = null;
        icon.enabled = false;
        quantity.enabled = false;
    }

    public void OnRemoveButton()
    {
        if (PlayerController.drop && PlayerController.instance._animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            if (interactable.item.Model == null) { return; }

            if (interactable.currentQuantity == 1)
            {
                player.GetComponentInChildren<Animator>().Play("Drop");
                GameObject newGen = Instantiate(interactable.item.Model, player.transform.position +player.transform.forward + player.transform.up, Quaternion.identity);
                Inventory.instance.RemoveItem(interactable);

            }
            else
            {
                player.GetComponentInChildren<Animator>().Play("Drop");
                interactable.currentQuantity -= 1;
                GameObject newGen = Instantiate(interactable.item.Model, player.transform.position + player.transform.forward + player.transform.up, Quaternion.identity);

            }
        }
        else if(!PlayerController.drop)
        {
            NoticeManager.instance.setText("前面有障礙物。");
            NoticeManager.instance.noticeOnOff();
        }
        else
        {
            NoticeManager.instance.setText("停下才可以掉下物品。");
            NoticeManager.instance.noticeOnOff();
        }
    }

    public void OnItemButton()
    {
        //Equip
        if (interactable != null)
        {
            if (interactable.item.Model == null) { return; }
            if (!Crafting.craftingTableOn)
            {
                if (interactable != null && interactable.item.EitemType == Item.itemType.Tool)
                {
                   handslot.equip(interactable);
                }
                else if(interactable.item.EitemType == Item.itemType.SpecialItem)
                {
                    interactable.use();
                }
            }
            else
            {
                inputAmountUI.enableOrDisable(true);
                inputAmountUI.recieveItem(interactable);
            }
        }//Craft
        controlItemDetail(false);
    }

    void Update()
    {
        if (interactable != null)
        {
            quantity.text = interactable.currentQuantity.ToString();
            if (interactable.currentQuantity == 0)
            {
                Inventory.instance.RemoveItem(interactable);
            }
        }

        
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (interactable != null && eventData.pointerCurrentRaycast.gameObject.name != "RemoveButton")
        {
            controlItemDetail(true);
            itemDetail.transform.position = eventData.position;
            itemDetail.GetComponentInChildren<Text>().text = "名稱:"+interactable.name+"\n"+"重量: "+interactable.weight*interactable.currentQuantity+"KG";
            
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (interactable != null)
        {
            controlItemDetail(false);
        }
    }

    public void controlItemDetail(bool a)
    {
        itemDetail.GetComponentInChildren<Image>().enabled = a;
        itemDetail.GetComponentInChildren<Text>().enabled = a;
    }
}
