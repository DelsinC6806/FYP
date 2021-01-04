using UnityEngine;

public class CraftTable : Interactable
{
    public override void OnCollisionStay(Collision col)
    {
        base.OnCollisionStay(col);
        if (col.gameObject.tag.Equals("Player"))
        {
            text.text += System.Environment.NewLine + "按 G 合成";
            if (Input.GetKeyDown(KeyCode.G))
            {
                Crafting.craftingTableOn = true;
                text.enabled = false;
            }
        }
    }

    public override void use()
    {
        if (PlayerController.drop)
        {
            PlayerController.instance.GetComponentInChildren<Animator>().Play("Drop");
            Instantiate(this.item.Model, PlayerController.instance.transform.position + PlayerController.instance.transform.forward * 2, Quaternion.identity);
            Inventory.instance.RemoveItem(this);
        }
        else
        {
            NoticeManager.instance.setText("前面有障礙物。");
            NoticeManager.instance.noticeOnOff();
        }
    }
}
