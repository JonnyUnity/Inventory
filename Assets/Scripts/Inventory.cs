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

    private int SelectedSlot;
    private int PickedUpSlot;
    private bool IsPickedUp;

    private int currRow = 0;
    private int currCol = 0;
    private readonly int totalRows = 3;
    private readonly int totalCols = 6;

    private readonly List<(int, int)> Resolutions = new() { (1280, 720), (1920, 1080), (3860, 2160) };
    private int ResolutionIndex = 1;
    
    private Controls Controls;
    private Vector2 Move;
    private bool SelectionMoving;


    public void Awake()
    {
        Controls = new Controls();

        Controls.Map.PickupPutdown.performed += ctx => OnPickup();

        Controls.Map.ResolutionUp.performed += ctx => OnResolutionUp();
        Controls.Map.ResolutionDown.performed += ctx => OnResolutionDown();

        Controls.Map.RemoveRestart.performed += ctx => OnRemoveRestart();

        Controls.Map.Move.performed += ctx => Move = ctx.ReadValue<Vector2>();
        Controls.Map.Move.canceled += ctx => Move = Vector2.zero;

        Controls.Enable();

    }


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
                //Debug.Log(i + " - " + inventoryIndex);

            } while (CharacterItems[inventoryIndex] != null);
        
            CharacterItems[inventoryIndex] = item;
            InventoryUI.AddNewItem(inventoryIndex, item);

        }

        SelectedSlot = 0;
        InventoryUI.SetSelected(SelectedSlot, IsPickedUp);
        IsPickedUp = false;
        //DebugInventory();
    }


    public void OnPickup()
    {
        if (IsPickedUp)
        {
            // drop picked item
            SwapItems(PickedUpSlot, SelectedSlot);
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


    public void OnRemoveRestart()
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


    public void Update()
    {

        int rowChange = 0;
        int colChange = 0;

        Vector2 m = new Vector2(Move.x, Move.y);

        if (!SelectionMoving)
        {
            if (m == Vector2.up)
            {
                rowChange = -1;
                colChange = 0;
            }
            else if (m == Vector2.down)
            {
                rowChange = 1;
                colChange = 0;
            }
            else if (m == Vector2.left)
            {
                rowChange = 0;
                colChange = -1;
            }
            else if (m == Vector2.right)
            {
                rowChange = 0;
                colChange = 1;
            }

            if (Mathf.Abs(rowChange) != 0 || Mathf.Abs(colChange) != 0)
            {
                SelectionMoving = true;
                ChangeSelected(rowChange, colChange);
            }
        }

        if (m == Vector2.zero)
        {
            SelectionMoving = false;
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

        InventoryUI.SetSelected(SelectedSlot, IsPickedUp);

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



    private void ChangeResolution()
    {
        var res = Resolutions[ResolutionIndex];
        Screen.SetResolution(res.Item1, res.Item2, FullScreenMode.MaximizedWindow);
    }

    private void OnResolutionDown()
    {
        ResolutionIndex = Mathf.Max(0, ResolutionIndex - 1);
        ChangeResolution();
    }


    private void OnResolutionUp()
    {
        ResolutionIndex = (ResolutionIndex + 1) % Resolutions.Count;
        ChangeResolution();
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
