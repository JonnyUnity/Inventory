using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    public UIItem[] Items;
    public SelectedUIItem SelectedItem;
    public Sprite SelectedSprite;

    public int SelectedSlot = 0;
    public TMPro.TextMeshProUGUI ItemText;


    public void Awake()
    {
        Items = GetComponentsInChildren<UIItem>();
        UpdateItemText(0);
        
    }


    public void SetSelected(int slot, bool isPickedUp)
    {

        SelectedSlot = slot;
        SelectedItem.transform.position = Items[SelectedSlot].transform.position;
        if (!isPickedUp) UpdateItemText(slot);
    }


    public void PickSlot(int slot)
    {
        SelectedSlot = slot;
        SelectedItem.SetSelectedSprite(Items[SelectedSlot].Item.Icon);

        Items[SelectedSlot].MarkAsSelected();
    }


    public void UnpickSlot()
    {
        SelectedItem.ClearSelection();
    }


    public void UpdateItemText(int slot)
    {
        UIItem selectedItem = Items[slot];
        if (selectedItem.Item == null)
        {
            ItemText.text = "";
        }
        else
        {
            ItemText.text = selectedItem.Item.Title;
        }
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
        SelectedItem.ClearSelection();
        UpdateItemText(slot);
    }


    public void SwapItems(int fromSlot, int toSlot, Item fromItem, Item toItem)
    {
        var temp = toItem;

        UpdateSlot(toSlot, fromItem);
        UpdateSlot(fromSlot, temp);
        UnpickSlot();
        UpdateItemText(toSlot);
    }


    public void ClearInventory()
    {
        foreach (UIItem item in Items)
        {
            item.UpdateItem(null);
        }
    }


}
