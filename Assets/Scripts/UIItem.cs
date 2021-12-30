using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIItem : MonoBehaviour
{
    public Item Item;
    private Image SpriteImage;

    public void Awake()
    {
        SpriteImage = GetComponent<Image>();
        UpdateItem(null);
    }


    public void UpdateItem(Item item)
    {
        Item = item;
        if (Item == null)
        {
            SpriteImage.color = Color.clear;
        }
        else
        {
            SpriteImage.color = Color.white;
            SpriteImage.sprite = item.Icon;
        }

    }



}
