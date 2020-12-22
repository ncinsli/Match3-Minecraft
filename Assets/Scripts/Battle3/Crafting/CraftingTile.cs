using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CraftingTile : MonoBehaviour
{
    [SerializeField] private Image IconGUI;
    [SerializeField] private Text NameShower;
    public Button buyButton;
    public CraftingItem ItemToCraft;
    public List<InventoryElement> materialsNeeded;
    private void Start()
    {
        if (ItemToCraft == null) Debug.LogWarning("SO ISN'T ASSIGNED");
        NameShower.text = ItemToCraft.Name;
        IconGUI.sprite = ItemToCraft.Icon; 
        buyButton.onClick.AddListener(Buy);
    }
    private void Buy()
    {
        materialsNeeded.ForEach( m => Inventory.instance.Remove(m, 1));
    }
}
