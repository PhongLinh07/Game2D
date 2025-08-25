using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpriteCfgItem : ConfigItem
{
    public int id;
    public string atlas;
    public string sprite;

    public override string ToString()
    {
        return $"ID: {id}, Atlas: {atlas}, Sprite: {sprite}";
    }
}