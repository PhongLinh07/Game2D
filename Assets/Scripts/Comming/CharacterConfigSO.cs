using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;


[CreateAssetMenu(menuName = "DataBase/Character")]
public class CharacterConfigSO : ScriptableObject
{
    public List<ChacterCfgItem> datas;
}

