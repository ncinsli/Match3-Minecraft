using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
public class InventoryGUI : Singleton<InventoryGUI>
{
    [SerializeField] private float spaceBetweenCells = 100f;
    [SerializeField] private Vector3 startPosition; 
    [SerializeField] private GameObject inventoryCell;
    [SerializeField] private int count;
    private RectTransform cellTransform; 
    public List<InventoryCell> cells;
    private void Start()
    {
        for (int i = 0; i < count; i++)
        {
            var cell = Instantiate(inventoryCell, transform);
            cellTransform = cell.GetComponent<RectTransform>();
            
            cellTransform.position = startPosition;
            startPosition += Vector3.right * spaceBetweenCells;
            cells.Add(cell.GetComponent<InventoryCell>());
        }
    }
    public void AddElement(InventoryElement element)
    {
        if (cells.Any(cell => cell.currentElement == element))
        {
            cells.Where(cell => cell.currentElement == element).First().AddItemCount(element);
        }
        
        else cells.Where(cell => cell.currentElement == null).First().SetItem(element);
    }
}
