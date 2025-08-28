using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;


public class GeneratorJSON : MonoBehaviour
{
    [SerializeField] private SkillConfigSO skillConfigSO;
    [SerializeField] private ItemConfigSO itemConfigSO;
    [SerializeField] private SpriteConfigSO spriteConfigSO;
    [SerializeField] private CharacterConfigSO chacterCfgItemSO;


    // Start is called before the first frame update
    void Awake()
    {
        spriteConfigSO.SaveJsonToFile(typeof(SpriteCfgItem).Name);

        ConfigMgr<SkillCfgItem>.GetInstance.ExportToJson(skillConfigSO.datas, typeof(SkillCfgItem).Name);
        ConfigMgr<ItemCfgItem>.GetInstance.ExportToJson(itemConfigSO.datas, typeof(ItemCfgItem).Name);
        ConfigMgr<ChacterCfgItem>.GetInstance.ExportToJson(chacterCfgItemSO.datas, typeof(ChacterCfgItem).Name);
        

    }

}
