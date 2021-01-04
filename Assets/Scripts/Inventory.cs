using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;
    void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
        }
        instance = this;    
    }
    #endregion
    public delegate void OnItemChanged();
    public OnItemChanged OnItemChangedCallBack;
    public List<Interactable> interactables = new List<Interactable>();
    public GameObject UIinventory;
    public Image handslot;
    int money;
    int maxWeight = 100;
    float sumOfAllItem;

    public Text weight;
    public Text cashValue;

    private GameObject Player;
    int space = 20;
    bool inventoryOpen = false;



    void Start()
    {
        Player = GameObject.Find("Player");
        sumOfAllItem = 0;
        weight.text = "總重量: " + sumOfAllItem + "/" + maxWeight;
        money = 0;
    }

    // Update is called once per frame
    void Update()
    {
        cashValue.text = "$"+money.ToString();
        inventoryControl();
    }

    public bool Additem(Interactable g)
    {
        if (Stackitem(g) == false)
        {

            if (interactables.Count < space)
            {
                if (sumOfAllItem < maxWeight)
                {
                    PlayerController.instance._animator.SetTrigger("tr_pickup");
                    interactables.Add(g);

                    if (OnItemChangedCallBack != null)
                    {
                        OnItemChangedCallBack.Invoke();
                    }
                    calculateWeightTotal();
                    return true;
                }
                else
                {
                    NoticeManager.instance.setText("物品欄過重。");
                    NoticeManager.instance.noticeOnOff();
                }
            }
            else
            {
                NoticeManager.instance.setText("物品欄已滿。");
                NoticeManager.instance.noticeOnOff();
                return false;
            }
        }
        return true;
    }

    public void RemoveItem(Interactable g)
    {
        interactables.Remove(g);
        calculateWeightTotal();
        if (OnItemChangedCallBack != null)
        {
            OnItemChangedCallBack.Invoke();
        }
    }

    bool Stackitem(Interactable g)
    {
        foreach (Interactable i in interactables)
        {
            if (i != null)
            {
                if (i.name == g.name && g.item.EitemType != Item.itemType.Tool && g.item.EitemType != Item.itemType.Fish)
                {
                    if(i.currentQuantity <= i.item.maxcapacity)
                    i.currentQuantity += g.currentQuantity;
                    OnItemChangedCallBack();
                    PlayerController.instance._animator.SetTrigger("tr_pickup");
                    Destroy(g.gameObject);
                    calculateWeightTotal();
                    return true;
                }
            }
        }
        
        return false;
    }

    void inventoryControl()
    {
        if (inventoryOpen == false && Input.GetKeyDown(KeyCode.B))
        {
            UIinventory.GetComponent<Animator>().SetBool("open", true);
            inventoryOpen = true;
        }
        else if (inventoryOpen == true && Input.GetKeyDown(KeyCode.B))
        {
            inventoryDisable();
        }
    }

    public void inventoryDisable()
    {
        UIinventory.GetComponent<Animator>().SetBool("open", false);
        inventoryOpen = false;
    }

    void calculateWeightTotal()
    {
        if (interactables.Count != 0)
        {
            sumOfAllItem = 0;
            for (int x = 0; x < interactables.Count; x++)
            {
                sumOfAllItem += interactables[x].weight * interactables[x].currentQuantity;
            }
        }
        else
        {
            sumOfAllItem = 0;
        }
        weight.text = "負重: " + sumOfAllItem + "/" + maxWeight;
    }
}
