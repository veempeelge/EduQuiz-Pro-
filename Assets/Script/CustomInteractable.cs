using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomInteractable : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerEnterHandler
{
    [SerializeField] private ImageState[] images;
    [SerializeField] private TextState[] texts;

    public void OnDeselect(BaseEventData eventData)
    {
        foreach (var item in images)
        {
            item.image.color = item.deselect;
            
        }

        foreach (var item in texts)
        {
            item.text.color = item.deselect;    
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        foreach (var item in images)
        {
            item.image.color = item.hover;

        }

        foreach (var item in texts)
        {
            item.text.color = item.hover;
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        foreach (var item in images)
        {
            item.image.color = item.select;

        }

        foreach (var item in texts)
        {
            item.text.color = item.select;
        }
    }
}

[System.Serializable]
public class ImageState
{
    public Image image;
    public Color select, deselect, hover;
}

[System.Serializable]
public class TextState
{
    public TMPro.TMP_Text text;
    public Color select, deselect, hover;
}
