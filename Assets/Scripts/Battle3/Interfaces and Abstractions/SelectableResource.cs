using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class SelectableResource : MonoBehaviour
{
    public static event Action<SelectableResource> OnSelected;
    public static event Action<SelectableResource> OnDeselected;
    public static event Action<SelectableResource> OnTrySelect;

    /// <summary>
    /// Сам элемент, который хранит выбранный ресурс
    /// </summary>
    private InventoryElement _info;

    private List<SelectableResource> neighbours = new List<SelectableResource>();
    private SpriteRenderer _spriteRenderer;
    private bool isSelected;
    private Color _unselectedeColor = Color.white;
    private Color _selectedColor = Color.gray;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Init(InventoryElement inventoryElement)
    {
        _info = inventoryElement;
        UpdateView();
    }

    private void UpdateView()
    {
        _spriteRenderer.sprite = _info.Sprite;
        _spriteRenderer.color = isSelected ? _selectedColor : _unselectedeColor;
    }

    private void OnMouseDrag() => TrySelect();
    private void OnMouseDown() => TrySelect();
    private void OnMouseEnter() => TrySelect();

    private void TrySelect()
    {
        //Дабы код функций не дублировался
        if (isSelected) return;
        bool canSelect = neighbours.Any(n => n.isSelected && n._info.ID == _info.ID) ||
                         Selector.instance.firstSelected == null;
        
        if (canSelect) OnTrySelect?.Invoke(this);
    }

//  @temp!
    public void Select()
    {
        isSelected = true;
        UpdateView();
        OnSelected?.Invoke(this);
    }

    public void Deselect()
    {
        if (isSelected)
            isSelected = false;
        UpdateView();

        OnDeselected?.Invoke(this);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent<SelectableResource>(out var other))
        {
            if (neighbours.Contains(other)) return;
            neighbours.Add(other);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent<SelectableResource>(out var other))
        {
            neighbours.Remove(other);
        }
    }
}