using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSelector : Singleton<ItemSelector>
{
    public InventoryElement currentlySelected;
    private void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void OnInventoryElementSelected(CraftingCell cellSelected)
    {
        if (ItemSelector.instance.currentlySelected == null) return;
        cellSelected.PushToCell(ItemSelector.instance.currentlySelected);
        InventoryGUI.instance.SubstractElement(cellSelected.ItemAttached);
        ItemSelector.instance.Clear();
    }

    public void Clear() => currentlySelected = null;
}
