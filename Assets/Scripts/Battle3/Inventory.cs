using UnityEngine;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
public class Inventory : ScriptableSingleton<Inventory>
{
    public Dictionary<int, int> InventoryMap = new Dictionary<int, int>()
    {
        {0, 3}, {1, 3}, {2, 3},
    };
    [SerializeField] private string JsonName = "inventory.json";
    private string _path; 
    public void Push(InventoryElement element, int count)
    {
        if (InventoryMap.TryGetValue(element.ID, out int c)) InventoryMap[element.ID] += count;      
        else InventoryMap.Add(element.ID, count); 
        for(int i = 0; i < count; i++) InventoryGUI.instance.AddElement(element); 
    }
    private void Serialize() //Запись
    {
        var stringyJson = JsonConvert.SerializeObject(InventoryMap);
        PlayerPrefs.SetString(name, stringyJson);
    }
    private void Deserialize() //Считка
    {
        if (PlayerPrefs.HasKey(name))
        {
            var jsonPlayerPrefs = PlayerPrefs.GetString(name);
            InventoryMap = JsonConvert.DeserializeObject<Dictionary<int, int>>(jsonPlayerPrefs);
        }
    }
    private void OnEnable() => Deserialize();
    private void OnDisable() => Serialize();
}