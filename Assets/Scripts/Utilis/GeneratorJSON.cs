using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;


public class GeneratorJSON : MonoBehaviour
{
    [SerializeField] private SkillConfigSO skillConfigSO;
    [SerializeField] private ItemConfigSO itemConfigSO;
    [SerializeField] private SpriteConfigSO spriteConfigSO;

    // Start is called before the first frame update
    void Awake()
    {
        spriteConfigSO.SaveJsonToFile(typeof(SpriteCfgItem).Name);
        skillConfigSO.SaveJsonToFile(typeof(SkillCfgItem).Name);
        itemConfigSO.SaveJsonToFile(typeof(ItemCfgItem).Name);
    }

}
