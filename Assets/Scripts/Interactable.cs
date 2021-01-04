
using UnityEngine.UI;
using UnityEngine;

public class Interactable : MonoBehaviour {

    public Item item;
    public int currentQuantity, currentDurability;
    GameObject interactText;
    [SerializeField]
    public Text text;
    [SerializeField]
    public bool Equiped, pickedUp;
    public AudioSource audioSource;
    public float weight;


    public void Awake()
    {
        weight = item.weight;
        name = item.name;
        interactText = GameObject.Find("interactText");
        text = interactText.GetComponentInChildren<Text>();
        text.enabled = false;
        currentQuantity = 1;
        if (item.EitemType == Item.itemType.Tool)
        {
            Equiped = false;
            currentDurability = item.maxDurability;
        }

        if(item.EitemType == Item.itemType.Materials)
        {
            item.maxcapacity = 10;
        }

    }


    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            text.enabled = false; 
        }
    }

    public virtual void OnCollisionStay(Collision col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            text.enabled = true;
            text.text = "按 F 撿起" + this.name;
            if (Input.GetKeyDown(KeyCode.F))
            {
                pickUp();
            }

            void pickUp()
            {
                pickedUp = Inventory.instance.Additem(this);
                if (pickedUp)
                {
                    text.enabled = false;
                    this.gameObject.GetComponent<Collider>().isTrigger = true;
                    this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                    this.gameObject.GetComponent<MeshRenderer>().enabled = false;
                }
            }
        }
    }

    public void RemoveFromInventory()
    {
        Inventory.instance.RemoveItem(this);
    }

    public virtual void use()
    {
        
        if (!PlayerController.instance.isAttacking)
        {
            PlayerController.instance._animator.SetTrigger("Farm");
            PlayerController.instance.isAttacking = true;
            audioSource.PlayDelayed(0.5f);

        }
    }

    
}