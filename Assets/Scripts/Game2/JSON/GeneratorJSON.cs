using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;


public class GeneratorJSON : MonoBehaviour
{
    [SerializeField] private CharacterConfigSO chacterCfgItemSO;

    // Start is called before the first frame update
    void Awake()
    {
        CharacterConfig.GetInstance.ExportToJson(chacterCfgItemSO.datas, typeof(ChacterCfgItem).Name);
    }

}
