using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BattleGrid : Singleton<BattleGrid>{
    [SerializeField] public float upBorder = 5f;
    [SerializeField] public float downBorder = -4.5f;
    [SerializeField] public float leftBorder = -2.5f;
    [SerializeField] public float rightBorder = -2.5f;
    [SerializeField] public float cellSize = 0.5f;
}
