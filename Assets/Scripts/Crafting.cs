using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Crafting : MonoBehaviour
{

    #region SingleTon
    public static Crafting instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
        }
        instance = this;
    }
    #endregion
    public static bool craftingTableOn = false;
    public GameObject craftingUI;
    public CraftingRecipe[] craftingRecipes;
    public CraftingSlot[] craftingSlots;
    public CraftingSlot productSlot;
    public AudioSource source;
    public AudioClip craftSound;
    public Animator crafting;

    Interactable result;

    void Start()
    {
        craftingTableOn = false;
    }

    void Update()
    {
        if (craftingTableOn)
        {
            crafting.SetBool("open", true);
        }
        else
        {
            crafting.SetBool("open", false);
        }
    }

    public void onConfirmButton()
    {
        Craft();
    }

    public void onExitButton()
    {
        craftingTableOn = false;
    }

    void Craft()
    {
        if (craftingSlots[0].slotInteractable != null && craftingSlots[1].slotInteractable != null)
        {

            for (int i = 0; i < craftingRecipes.Length; i++)
            {
                Item a = craftingRecipes[i].Materials[0].item;
                int aa = craftingRecipes[i].Materials[0].Amount;
                Item b = craftingRecipes[i].Materials[1].item;
                int ba = craftingRecipes[i].Materials[1].Amount;

                if (craftingSlots[0].slotInteractable.item == a && craftingSlots[0].slotItemAmount.Equals(aa))
                {
                    if(craftingSlots[1].slotInteractable.item == b && craftingSlots[1].slotItemAmount.Equals(ba))
                    {
                        foreach (CraftingSlot x in craftingSlots)
                        {
                            x.RemoveItem();
                        }
                        result = craftingRecipes[i].Result.GetComponent<Interactable>();
                        productSlot.AddItem(result, 1);
                        NoticeManager.instance.setText("成功合成" + result.item.name);
                        NoticeManager.instance.noticeOnOff();
                        source.PlayOneShot(craftSound);
                    }
                }else if (craftingSlots[0].slotInteractable.item == b && craftingSlots[0].slotItemAmount.Equals(ba))
                {
                    if (craftingSlots[1].slotInteractable.item == a && craftingSlots[1].slotItemAmount.Equals(aa))
                    {
                        foreach (CraftingSlot x in craftingSlots)
                        {
                            x.RemoveItem();
                        }
                        result = craftingRecipes[i].Result.GetComponent<Interactable>();
                        productSlot.AddItem(result, 1);
                        NoticeManager.instance.setText("成功合成"+ result.item.name);
                        NoticeManager.instance.noticeOnOff();
                        source.PlayOneShot(craftSound);
                    }
                }

            }
        }
        else
        {
            NoticeManager.instance.setText("擺啦仲唔擺?");
            NoticeManager.instance.noticeOnOff();
        }
    }
}
