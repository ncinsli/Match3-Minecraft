using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Difficulty : ScriptableSingleton<Difficulty>
{
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _levelName;
    [SerializeField] private float _level;
    public float Level   
    { 
        get => _level; 
        set
        {
            if (value > 0 && value < maxDifficulty) _level = value; 
        }
    } 
    public Sprite Icon => _icon;
    public string LevelName => _levelName;
    public int maxDifficulty = 10;
}
