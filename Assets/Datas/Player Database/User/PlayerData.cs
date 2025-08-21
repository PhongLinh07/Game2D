using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Data/Player")]

public class PlayerData : ScriptableObject
{
    // Thông tin cơ bản
    public int ID;
    public string playerName;
    public List<int> skills; //id
    public List<int> skillEquipped = new();
    public List<PlayerItem> playerItems; // Id(Only), quantity

}

