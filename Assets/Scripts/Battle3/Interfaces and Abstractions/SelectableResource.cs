using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class SelectableResource : MonoBehaviour
{
    public static event Action<SelectableResource> OnSelected;
    public static event Action<SelectableResource> OnDeselected;
    public static event Action<SelectableResource> OnTrySelect;
    private InventoryElement _info;
    public InventoryElement Info => _info;
    private List<SelectableResource> _neighbours = new List<SelectableResource>();
    private SpriteRenderer _spriteRenderer;
    private bool _isSelected;
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
        _spriteRenderer.color = _isSelected ? _selectedColor : _unselectedeColor;
    }

    private void OnMouseDrag() => TrySelect();
    private void OnMouseDown() => TrySelect();
    private void OnMouseEnter() => TrySelect();
    private void TrySelect()
    {
        //Дабы код функций не дублировался
        if (_isSelected) return;
        bool canSelect = _neighbours.Any(n => n._isSelected && n._info.ID == _info.ID && Selector.instance.LastSelected == n) ||
                         Selector.instance.firstSelected == null;
        
        if (canSelect) OnTrySelect?.Invoke(this);
    }

//  @temp!
    public void Select()
    {
        _isSelected = true;
        UpdateView();
        OnSelected?.Invoke(this);
    }

    public void Deselect()
    {
        if (_isSelected)
            _isSelected = false;
        UpdateView();
        InventoryGUI.instance.AddElement(Info);
        OnDeselected?.Invoke(this);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent<SelectableResource>(out var other))
        {
            if (_neighbours.Contains(other)) return;
            _neighbours.Add(other);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent<SelectableResource>(out var other))
        {
            _neighbours.Remove(other);
        }
    }
}