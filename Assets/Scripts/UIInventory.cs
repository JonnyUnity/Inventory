using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    public UIItem[] Items;


    public void Awake()
    {
       Items = GetComponentsInChildren<UIItem>();
    }


    public void UpdateSlot(int slot, Item item)
    {
        Items[slot].UpdateItem(item);
    }


    public void AddNewItem(int slot, Item item)
    {
        UpdateSlot(slot, item);
    }



    public void RemoveItem(int slot)
    {
        UpdateSlot(slot, null);
    }


}
