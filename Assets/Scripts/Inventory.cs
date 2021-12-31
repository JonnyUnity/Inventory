using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Inventory : MonoBehaviour
{
    public Item[] CharacterItems = new Item[18];
    public Item SelectedItem;

    public ItemDatabase ItemDatabase;
    public UIInventory InventoryUI;

    public int SelectedSlot;
    public int PickedUpSlot;
    private bool IsPickedUp;

    private int currRow = 0;
    private int currCol = 0;
    private readonly int totalRows = 3;
    private readonly int totalCols = 6;


    public void Start()
    {
        PopulateInventory();
    }


    public void PopulateInventory()
    {
        CharacterItems = new Item[18];
        currRow = 0;
        currCol = 0;
        var rand = new Random();

        for (int i = 0; i < 5; i++)
        {
            Item item = ItemDatabase.GetRandomItem();

            int inventoryIndex;
            do
            {
                inventoryIndex = rand.Next(0, CharacterItems.Length);
                Debug.Log(i + " - " + inventoryIndex);

            } while (CharacterItems[inventoryIndex] != null);
        
            CharacterItems[inventoryIndex] = item;
            InventoryUI.AddNewItem(inventoryIndex, item);

        }

        InventoryUI.SetSelected(0);
        //DebugInventory();
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

        // Pick item
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (IsPickedUp)
            {
                // drop picked item
                Debug.Log("Drop picked item!");
                SwapItems(PickedUpSlot, SelectedSlot);
                //InventoryUI.UnpickSlot();
                IsPickedUp = false;
            }
            else if (CharacterItems[SelectedSlot] != null)
            {
                // pick item
                PickedUpSlot = SelectedSlot;
                IsPickedUp = true;
                InventoryUI.PickSlot(SelectedSlot);
            }
        }


        if (Input.GetKeyDown(KeyCode.R))
        {
            // Remove item from inventory
            if (IsPickedUp)
            {
                RemoveItem(PickedUpSlot);
                PickedUpSlot = -1;
                IsPickedUp = false;
            }
            else
            {
                InventoryUI.ClearInventory();
                PopulateInventory();
            }

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

        InventoryUI.SetSelected(SelectedSlot);

    }


    public void MoveItem(int oldSlot, int newSlot)
    {

    }

    public void RemoveItem(int index)
    {
        CharacterItems[index] = null;
        InventoryUI.RemoveItem(index);
    }



    public void SwapItems(int fromSlot, int toSlot)
    {
        InventoryUI.SwapItems(fromSlot, toSlot, CharacterItems[fromSlot], CharacterItems[toSlot]);

        var temp = CharacterItems[toSlot];
        CharacterItems[toSlot] = CharacterItems[fromSlot];
        CharacterItems[fromSlot] = temp;

    }


    public void DebugInventory()
    {
        string itemTitle;
        for (int i = 0; i < CharacterItems.Length; i++)
        {
            if (CharacterItems[i] == null)
            {
                itemTitle = "";
            }
            else
            {
                itemTitle = CharacterItems[i].Title;
            }
            Debug.Log("[" + i + "] = " + itemTitle);
        }


    }

}
