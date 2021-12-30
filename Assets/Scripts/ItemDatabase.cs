using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class ItemDatabase : MonoBehaviour
{
    public List<Item> Items = new List<Item>();

    public void Awake()
    {
        BuildDatabase();
    }

    public void BuildDatabase()
    {
        Items = new List<Item>()
        {
              new Item(1, "Sword"),
              new Item(2, "Bow"),
              new Item(3, "Staff"),
              new Item(4, "Shovel"),
              new Item(5, "Good Sword"),
              new Item(6, "Axe"),
              new Item(7, "Pickaxe"),
              new Item(8, "Golden Dagger"),
              new Item(9, "Arrow"),
              new Item(10, "Poison Arrow"),
              new Item(11, "Nice Coat"),
              new Item(12, "Bronze Armour"),
              new Item(13, "Silver Armour"),
              new Item(14, "Wooden Buckler"),
              new Item(15, "Boots of Walking"),
              new Item(16, "Helmet"),
              new Item(17, "Sickle"),
              new Item(18, "Magic Staff"),
              new Item(19, "Shiny Ring"),
              new Item(20, "Amulet"),
              new Item(21, "Red Potion"),
              new Item(22, "Large Red Potion"),
              new Item(23, "Blue Potion"),
              new Item(24, "Green Potion"),
              new Item(25, "Coins"),
              new Item(26, "Ruby"),
              new Item(27, "Chest"),
              new Item(28, "Key"),
              new Item(29, "Meat"),
              new Item(30, "Apple"),
              new Item(31, "Carrot"),
              new Item(32, "Sunflower"),
              new Item(33, "Fish"),
              new Item(34, "Floppy Disk"),
              new Item(35, "Cog"),
              new Item(36, "Speech Bubble"),
              new Item(37, "Heart"),
              new Item(38, "Wave"),
              new Item(39, "Harvested Tooth"),
              new Item(40, "Fire Bolt"),
              new Item(41, "Wind Sword"),
              new Item(42, "Fire Sword"),
              new Item(43, "Fire Axe"),
              new Item(44, "Shield"),
              new Item(45, "Angry Dwarf"),
              new Item(46, "Eye of Staring"),
              new Item(47, "First Aid"),
              new Item(48, "Blood Dagger"),
              new Item(49, "Lightning Buckler"),
              new Item(50, "Mushroom"),
              new Item(51, "Eye of Newt"),
              new Item(52, "Slime"),
              new Item(53, "Square"),
              new Item(54, "Square 2"),
              new Item(55, "Square 3")
        };
          
            
    }

    public Item GetItem(int id)
    {
        return Items.Find(i => i.ID == id);
    }

    public Item GetRandomItem()
    {
        Random rand = new Random();
        return Items[rand.Next(1, Items.Count)];
    }


}
