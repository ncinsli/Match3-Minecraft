using UnityEngine;

[CreateAssetMenu(fileName = "", menuName = "Game logic/Inventory Element", order = 15)]
public class InventoryElement : ScriptableObject
{
    [SerializeField] private int _id;
    [SerializeField] private Sprite _sprite;
    public int ID => _id;
    public Sprite Sprite => _sprite;
    public InventoryElement(int id) => _id = id;
}