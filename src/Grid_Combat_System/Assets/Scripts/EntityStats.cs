using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


[CreateAssetMenu(fileName = "EntityName_Stats", menuName = "EntityStats", order = 0)]
public class EntityStats : ScriptableObject
{
    [SerializeField] private SerializeDict _serializeDict;
    private Dictionary<string, float> _stats;
}


[Serializable]
public class SerializeDict
{
    [SerializeField] public SerializeDictObject[] _stats = {
        new SerializeDictObject("HP", 10),
        new SerializeDictObject("MP", 3),
        new SerializeDictObject("AP" , 6),
        new SerializeDictObject("Strength", 5),
        new SerializeDictObject("Range", 1)
    };

    public Dictionary<string, int> ToDictionary()
    {
        Dictionary<string, int> dict = new Dictionary<string, int>();
        foreach (SerializeDictObject obj in _stats)
        {
            dict.Add(obj._statName , obj._value);
        }
        return dict;
    }

    public void UpdateDictionary(Dictionary<string, int> dict)
    {
        int counter = 0;
        foreach (var stats in dict)
        {
            _stats[counter]._statName = stats.Key;
            _stats[counter]._value = stats.Value;
            counter++;
        }
    }
}

[Serializable]
public class SerializeDictObject
{
    public string _statName;
    public int _value;

    public SerializeDictObject(string statName, int value)
    {
        _statName = statName;
        _value = value;
    }
}
