using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public enum EAttribute // number
{
    HpMax = 0,          // Máu tối đa của nhân vật
    Attack = 1,         // Lực tấn công vật lý
    Speed = 2,          // Tốc độ di chuyển hoặc hành động
    Crit = 3,           // Tỷ lệ chí mạng (%)
    CritHarm = 4,       // Sát thương chí mạng (multiplier hoặc %)
    Defense = 5,        // Giảm sát thương nhận vào
    Recovery = 6,       // Lượng hồi máu hoặc hồi sinh máu theo thời gian
    Rebirth = 7,        // Khả năng hồi sinh / tái sinh sau khi chết
    Cd = 8,             // Thời gian cooldown kỹ năng (giảm thời gian chờ)
    Collect = 9,        // Khả năng thu thập item, tiền hoặc điểm
    ExpUp = 10,         // Tăng kinh nghiệm nhận được
    Dash = 11,          // Khoảng cách hoặc khả năng dash/teleport
    Invince = 12,       // Thời gian bất tử / miễn sát thương
    ExtraManaAir = 13,  // Lượng mana/phép bổ sung hoặc năng lượng nguyên tố
    Qi = 14,            // Năng lượng nội lực (Qi) của nhân vật
    QiSpeed = 15,       // Tốc độ hồi Qi
    QiTime = 16,        // Thời gian duy trì trạng thái Qi
    Acupoint = 17,      // Điểm huyệt / skill point hoặc điểm bấm huyệt
    ExtraHerb = 18,     // Số lượng thảo dược phụ trợ / item hỗ trợ
    Reroll = 19,        // Số lượt quay lại / refresh skill/item
    SkillCount = 20,    // Số lượng skill có thể dùng / trang bị
    HighMissionItem = 21,// Item đặc biệt trong nhiệm vụ cấp cao
    FitSkillWeight = 22,// Trọng số chọn skill phù hợp hoặc ưu tiên skill
    Shield = 23,        // Lượng lá chắn hấp thụ sát thương
    RoleExpUp = 24,     // Tăng kinh nghiệm nhận từ vai trò / class
    FreeReroll = 25,    // Số lượt quay miễn phí / refresh miễn phí
    MaxLv = 26,         // Level tối đa nhân vật có thể đạt
    CritResist = 27,    // Kháng sát thương chí mạng (%)
    ExtraHerbProb = 28, // Tỷ lệ nhận thảo dược phụ trợ
    QiConsumption = 29  // Năng lượng tiêu hao

}

public enum EEquipType
{
    None = 0,      // Không trang bị (item không thể equip)
    Head = 1,      // Trang bị cho đầu (mũ, mấn, băng đô...)
    Cloth = 2,     // Trang bị cho thân (áo giáp, áo khoác, váy...)
    Shoes = 3,     // Trang bị cho chân (giày, ủng, dép chiến)
    Necklace = 4,  // Vòng cổ, tăng stat hoặc buff
    Pendant = 5,   // Mặt dây, amulet, vật phẩm phụ trợ
    Weapon = 6     // Vũ khí (kiếm, cung, quyền trượng, búa...)
}



[System.Serializable]
public class EquipType
{
    public EEquipType equipType;
    public int idItem;
}


[System.Serializable]
public class CharacterCfgItem : ConfigItem
{
    public Information infomation = new();
    public General general = new();
    public Combat combat = new();
    public MartialArts martialArts = new();
    public SpiritualRoot spiritualRoot = new();

    public List<int> SkillsLearned = new();
    public List<int> SkillsEquipped = new();   // danh sách buff

    [SerializeField] public List<ItemUseCfgItem> items = new();
    public Dictionary<int, ItemUseCfgItem> itemDict = new Dictionary<int, ItemUseCfgItem>();


    [SerializeField] public List<EquipType> equipTypes = new();
    [JsonIgnore] public Dictionary<EEquipType, int> quipDict = new Dictionary<EEquipType, int>();
    //[JsonProperty] public Dictionary<EEquipType, int> quipDict = new Dictionary<EEquipType, int>();


    public List<Attribute> attributes = new();
    [JsonIgnore] public Dictionary<EAttribute, float> attrDict = new Dictionary<EAttribute, float>();


    public float positionX = 0.0f;
    public float positionY = 0.0f;

    public EAsset idPrefab;

    [JsonIgnore] // tránh serialize thẳng Vector2
    public Vector2 position
    {
        get
        {
            return new Vector2(positionX, positionY);
        }
        set
        {
            positionX = value.x;
            positionY = value.y;
        }
    }

    public void Init()
    {

        foreach (var item in items)
        {
            item.Init();
            itemDict[item.id] = item;
        }

        foreach (var att in attributes)
        {
            attrDict[att.attribute] = att.value;
            Debug.LogWarning($"{att.attribute.ToString()} --- {att.value}");
        }

        foreach (var item in equipTypes)
        {
            quipDict[item.equipType] = item.idItem;
            Debug.LogWarning($"{item.equipType.ToString()} --- {item.idItem}");
        }

    }

    public override void ApplyFromRow(IDictionary<string, object> row) { }
    
}