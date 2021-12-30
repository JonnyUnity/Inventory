using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Inventory : MonoBehaviour
{
    public Item[] CharacterItems = new Item[15];
    public ItemDatabase ItemDatabase;

    public void Start()
    {
        TestItem();
    }

    public void TestItem()
    {
        
        
        Item item = ItemDatabase.GetRandomItem();
        var rand = new Random();
        int inventoryIndex = rand.Next(1, CharacterItems.Length);

        CharacterItems[inventoryIndex] = item;

        DebugInventory();
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
