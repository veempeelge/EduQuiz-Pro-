using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomInteractable : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private ImageState[] images;
    [SerializeField] private TextState[] texts;
    public Animator animator;

    bool correctButton;

    private void Start()
    {
    
    }
    public void OnDeselect(BaseEventData eventData)
    {
        foreach (var item in images)
        {
            //item.image.sprite = item.deselectSprite;
            
        }

        foreach (var item in texts)
        {
           // item.text.color = item.deselect;    
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
        foreach (var item in images)
        {
            animator.SetTrigger("Enter");
            

        }

        foreach (var item in texts)
        {
          //  item.text.color = item.hover;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        foreach (var item in images)
        {
            // item.image.sprite = item.normalSprite;
            animator.SetTrigger("Exit");

        }

        foreach (var item in texts)
        {
            //  item.text.color = item.hover;
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        foreach (var item in images)
        {
            //item.image.sprite = item.selectSprite;
            if (correctButton)
            {
                item.image.sprite = item.trueSprite;
            }
            else
            {
                item.image.sprite = item.falseSprite;
            }
        }

        foreach (var item in texts)
        {
           item.text.color = Color.clear;
        }
    }

    public void SetCorrect(bool value)
    {
        correctButton = value;
    }

}

[System.Serializable]
public class ImageState
{
   
    public Image image;
    public GameObject buttonGameObj;
    //  public Color select, deselect, hover;
    public Sprite trueSprite, falseSprite;

}

[System.Serializable]
public class TextState
{
   public TMPro.TMP_Text text;
  // public Color select, deselect, hover;
}
