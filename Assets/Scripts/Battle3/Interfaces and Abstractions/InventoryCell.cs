using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(Image))]
public class InventoryCell : MonoBehaviour, IPointerClickHandler, IPointerExitHandler
{
    public int order;
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

    public void SubstractItemCount()
    {
        itemCounter.text = (Convert.ToInt32(itemCounter.text) - 1).ToString();
        if (itemCounter.text == "0"){ 
            currentElement = null; //Типа обнуляем
            itemHolder.sprite = null;
        }
    }

    public void OnPointerClick(PointerEventData data) 
    {
        //То есть, при выборе элемента селектором он будет запоминаться
        ItemSelector.instance.currentlySelected = currentElement; 
    }

    public void OnPointerExit(PointerEventData data)
    {

    }
}
