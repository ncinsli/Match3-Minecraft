using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BattleGrid : Singleton<BattleGrid>{
    public Vector3[,] cells;
    [SerializeField] public float cellSize = 1f;
    [SerializeField] private Bounds bounds;
    public float RightBorder = 3f;
    public float LeftBorder = -2.934f; 
    public float DownBorder = -5.22f; 
    public float UpBorder => 0f;
    private void OnEnable(){
        var currentPosition = new Vector2(LeftBorder, DownBorder);
        cells = new Vector3[7, 7];
        for (int x = 0; x < 7; x++){
            for (int y = 0; y < 7; y++){
                cells[x, y] = currentPosition;
                currentPosition.y += cellSize;
                Debug.Log(cells[x,y]);
            }
            currentPosition.x += cellSize;
            currentPosition.y = DownBorder;
        }
    }
    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(bounds.center, bounds.size);
        if (cells == null) return;
        for (int x = 0; x < 7; x++){
            for(int y = 0; y < 7; y++){
                Vector2 current = cells[x, y];
                Gizmos.DrawWireCube(current, Vector2.one * cellSize); 
            }
        } 
    }
    public void ForEachCell(Action<int, int, Vector2> del){
        for (int x = 0; x < 7; x++){
            for (int y = 0; y < 7; y++) del(x, y, cells[x, y]);
        }
    }

}
