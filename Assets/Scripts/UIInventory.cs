using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    public UIItem[] Items;
    public UIItem SelectedItem;
    public int SelectedSlot = 0;
    public TMPro.TextMeshProUGUI ItemText;

    private int currRow = 0;
    private int currCol = 0;
    private int totalRows = 3;
    private int totalCols = 6;


    public void Awake()
    {
        Items = GetComponentsInChildren<UIItem>();
        UpdateItemText(0);
    }


    public void Update()
    {
        int rowChange = 0;
        int colChange = 0;

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            colChange = -1;
            rowChange = 0;
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            colChange = 0;
            rowChange = 1;
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            colChange = 1;
            rowChange = 0;
        }
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            colChange = 0;
            rowChange = -1;
        }

        if (Mathf.Abs(rowChange) != 0 || Mathf.Abs(colChange) != 0)
        {
            ChangeSelected(rowChange, colChange);
        }        
     
    }

    public void ChangeSelected(int rowChange, int colChange)
    {

        if (rowChange < 0)
        {
            currRow = Mathf.Max(0, currRow + rowChange);
        }
        else if (rowChange > 0)
        {
            currRow = Mathf.Min(currRow + rowChange, totalRows - 1);
        }

        if (colChange < 0)
        {
            currCol = Mathf.Max(0, currCol + colChange);
        }
        else if (colChange > 0)
        {
            currCol = Mathf.Min(currCol + colChange, totalCols - 1);
        }

        SelectedSlot = (currRow * totalCols) + currCol;

        UIItem selectedItem = Items[SelectedSlot];
        SelectedItem.transform.position = selectedItem.transform.position;

        UpdateItemText(SelectedSlot);

    }

    private void UpdateItemText(int slot)
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
    }


}
