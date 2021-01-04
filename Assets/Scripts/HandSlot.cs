using UnityEngine.UI;
using UnityEngine;

public class HandSlot : MonoBehaviour
{
    public PlayerController player;
    public Image equipmentImage;
    public Button handSlotButton;
    public UIInventory uiinventory;
    GameObject gameModel;

    void Start()
    {
     
    }

    void Update()
    {
        checkIfToolIsBroken();
    }

    public void equip(Interactable i)
    {
        if (player.currentWeapon == null)
        {
            player.currentWeapon = i;
            equipmentImage.sprite = i.item.itemImage;
            equipmentImage.enabled = true;
            i.Equiped = true;

            gameModel = Instantiate(i.item.Model, this.transform.position,Quaternion.identity);
            gameModel.transform.parent = this.transform;
            gameModel.transform.localPosition = i.item.pickUpPosition;
            gameModel.transform.localRotation =Quaternion.Euler (i.item.pickUpRotation);
            gameModel.GetComponent<Rigidbody>().isKinematic = true;
            i.RemoveFromInventory();

        }
    }

    public void Unequip()
    {
        if (player.currentWeapon != null)
        {
            equipmentImage.enabled = false;
            player.currentWeapon.Equiped = false;
            Inventory.instance.Additem(player.currentWeapon);
            player.currentWeapon = null;
            Destroy(gameModel);
        }
    }

    void checkIfToolIsBroken()
    {
        if (player.currentWeapon != null)
        {
            if (player.currentWeapon.currentDurability <= 0)
            {
                equipmentImage.enabled = false;
                player.currentWeapon.Equiped = false;
                player.currentWeapon = null;
                Destroy(gameModel);
            }
        }
    }
}

