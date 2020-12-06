using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
public class SelectableResource : MonoBehaviour{
    [SerializeField] public InventoryElement info; //Сам элемент, который хранит выбранный ресурс
    public Vector2 gridPosition{ //В проекте есть свой класс сетки, этот метод вызывает этот класс и возвращает позицию  
        get{ 
            Vector2 p = Vector2.zero;
            p.x = transform.position.x - BattleGrid.instance.leftBorder;  
            p.y = transform.position.y - BattleGrid.instance.upBorder; 
            return p / BattleGrid.instance.cellSize;
        }
    }
//  @temp!
    public void Select() => GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 1f);
    public void Deselect() => GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 1f, 1f);
}
