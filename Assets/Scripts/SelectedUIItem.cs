using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedUIItem : MonoBehaviour
{
    public Sprite SelectedSprite;
    private Image SpriteImage;

    public void Awake()
    {
        SpriteImage = GetComponent<Image>();
        ClearSelection();
    }


    public void SetSelectedSprite(Sprite selected)
    {
        SpriteImage.sprite = selected;
    }

    public void ClearSelection()
    {
        SpriteImage.sprite = SelectedSprite;
    }

}
