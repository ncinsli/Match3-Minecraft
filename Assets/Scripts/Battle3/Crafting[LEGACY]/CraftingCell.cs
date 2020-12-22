using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class CraftingCell : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image _itemRenderer; 
    public event Action<InventoryElement> OnElementPushed;
    public InventoryElement ItemAttached;
    public int cellNumber;

    public void PushToCell(InventoryElement item)
    {
        _itemRenderer = GetComponentsInChildren<Image>()[1];
        if (_itemRenderer != null) _itemRenderer.sprite = item.Sprite;
        else Debug.Log($"<color=yellow>Warning! Crafting cell {cellNumber} has null item renderer. INSPECTOR ASSIGN REQUIRED</color>)");
        ItemAttached = item;
    }
    
    public void OnPointerClick(PointerEventData data) => ItemSelector.instance.OnInventoryElementSelected(this);
    //Когда мы водим предметом 
}
