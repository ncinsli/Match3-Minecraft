using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR 
using UnityEditor;
#endif
public class Craft : Singleton<Craft>
{
    [SerializeField] InventoryCell[] cells;
    [HideInInspector] public int[] craftTable = 
    {0, 0, 0, 
     0, 0, 0, 
     0, 0, 0}; 
    

}
#if UNITY_EDITOR
[CustomEditor(typeof(Craft))]
public class CraftEditor : Editor
{
    public override void OnInspectorGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Space(180f);
        GUILayout.Label("Crafting table");
        GUILayout.EndHorizontal();

        GUILayout.Space(10f);
        var craftTable = ((Craft)target).craftTable;
        for(int i = 0; i < 3; i++)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(200f);
            for(int j = 0; j < 3; j++) GUILayout.Label(craftTable[i + j].ToString()); 
            GUILayout.Space(600f);
            GUILayout.EndHorizontal();
        }
        GUILayout.Space(20f);
        base.OnInspectorGUI();
    }
}
#endif