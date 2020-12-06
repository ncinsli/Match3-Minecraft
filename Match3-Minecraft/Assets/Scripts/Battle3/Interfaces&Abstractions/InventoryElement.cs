using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "", menuName = "Game logic/Inventory Element", order = 15)]
public class InventoryElement : ScriptableObject {
    [SerializeField] public int id;
    [SerializeField] public string name;
    [SerializeField] public Sprite texture;
}
