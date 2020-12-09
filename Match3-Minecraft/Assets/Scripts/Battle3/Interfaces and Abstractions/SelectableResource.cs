using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class SelectableResource : MonoBehaviour{
    [SerializeField] private List<SelectableResource> neighbours = new List<SelectableResource>();
    public event Action<SelectableResource> OnSelect;
    [SerializeField] private InventoryElement info; //Сам элемент, который хранит выбранный ресурс
    private Color originColor; 
    private bool isSelected;
    public Vector2 gridPosition{ //В проекте есть свой класс сетки, этот метод вызывает этот класс и возвращает позицию  
        get{ 
            Vector2 p = Vector2.zero;
            p.x = transform.position.x - BattleGrid.instance.leftBorder;  
            p.y = transform.position.y - BattleGrid.instance.upBorder; 
            return p / BattleGrid.instance.cellSize;
        }
    }
    private void Awake() => originColor = GetComponent<SpriteRenderer>().color;
    private void OnMouseDrag() => TrySelect();
    private void OnMouseDown() => TrySelect();
    private void OnMouseEnter() => TrySelect();
    private void TrySelect(){ //Дабы код функций не дублировался
        if (isSelected) return; 
        bool canSelect = neighbours.Any(n => n.isSelected && n.info.id == info.id) || Selector.instance.firstSelected == null;
        if (canSelect) OnSelect?.Invoke(this);
    }

//  @temp!
    public void Select(){ 
        GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 1f);
        isSelected = true;
    }

    public void Deselect(){ 
        GetComponent<SpriteRenderer>().color = originColor;
        isSelected = false;
    }

    private void OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.TryGetComponent<SelectableResource>(out var other)){
            if (neighbours.Contains(other)) return;
            neighbours.Add(other);
        }
    }
    private void OnTriggerExit2D(Collider2D col){
        if (col.gameObject.TryGetComponent<SelectableResource>(out var other)){
            neighbours.Remove(other);
        }
    }
}
