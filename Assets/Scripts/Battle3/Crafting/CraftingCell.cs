using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class CraftingCell : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Image itemRenderer; 
    public InventoryElement itemAttached;
    public int cellNumber;

    public void PushToCell(InventoryElement item)
    {
        if (itemRenderer != null) itemRenderer.sprite = item.Sprite;
        else Debug.Log($"<color=yellow>Warning! Crafting cell {cellNumber} has null item renderer. INSPECTOR ASSIGN REQUIRED</color>)");
        itemAttached = item;
    }
    
    public void OnPointerClick(PointerEventData data) => ItemSelector.instance.OnInventoryElementSelected(this);
    //Когда мы водим предметом 
}
