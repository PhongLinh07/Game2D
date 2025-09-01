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
    ExtraHerbProb = 28  // Tỷ lệ nhận thảo dược phụ trợ
}


[System.Serializable]
public class Attribute
{
    public EAttribute attribute;
    public int value;
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

    public List<EnhanceCfgItem> items = new();


    public List<Attribute> attributes = new();

    [JsonIgnore] // tránh serialize thẳng Vector2
    public Dictionary<EAttribute, int> attrDict = new Dictionary<EAttribute, int>();


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
        foreach(var att in attributes)
        {
            attrDict[att.attribute] = att.value;
            Debug.LogWarning($"{att.attribute.ToString()} --- {att.value}");
        }
    }

    public override void ApplyFromRow(IDictionary<string, object> row) { }
    
}