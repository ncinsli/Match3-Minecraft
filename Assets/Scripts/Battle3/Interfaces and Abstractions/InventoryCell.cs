using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(Image))]
public class InventoryCell : MonoBehaviour
{
    private RectTransform rectTransform;
    public InventoryElement currentElement;
    [SerializeField] private Image itemHolder;
    [SerializeField] private Text itemCounter;
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    public void SetItem(InventoryElement element)
    {
        itemHolder.sprite = element.Sprite;        
        currentElement = element;
    }
    public void AddItemCount(InventoryElement element)
    {
        Debug.Log(itemCounter.text);
        if (currentElement != null)
        {
            var txtInt = Convert.ToInt32(itemCounter.text) + 1;
            itemCounter.text = txtInt.ToString();
        }
        else SetItem(element);
        currentElement = element; 
    }
}
