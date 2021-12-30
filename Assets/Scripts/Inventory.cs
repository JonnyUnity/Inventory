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
        var rand = new Random();

        for (int i = 0; i < 5; i++)
        {
            Item item = ItemDatabase.GetRandomItem();

            int inventoryIndex = 0;
            do
            {
                inventoryIndex = rand.Next(1, CharacterItems.Length);
                Debug.Log(i + " - " + inventoryIndex);

            } while (CharacterItems[inventoryIndex] != null);
        
            CharacterItems[inventoryIndex] = item;

        }
        
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
