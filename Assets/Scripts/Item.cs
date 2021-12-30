using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public int ID;
    public string Title;
    public Sprite Icon;


    public Item(int id, string title)
    {
        ID = id;
        Title = title;
        Icon = Resources.Load<Sprite>("Sprites/Items/" + id);
    }

}
