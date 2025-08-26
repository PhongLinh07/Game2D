using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class SpriteCfgItem : ConfigItem
{
    public Sprite sprite;

    public override void ApplyFromRow(IDictionary<string, object> row) { }

    public override string ToString()
    {
        return $"ID: {id}, Sprite: {sprite}";
    }
}

