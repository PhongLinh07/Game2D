using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

using UnityEngine;


/// <summary>
/// Lưu trữ thông tin cá nhân cơ bản của nhân vật.
/// </summary>
[System.Serializable]
public class Information
{
    public string surName; // Họ của nhân vật (ví dụ: Vương)
    public string name;    // Tên của nhân vật (ví dụ: Thất Phong)
    public string sect;    // Tông môn (có thể là "Không" nếu là tán tu)
    public string race;    // Chủng tộc (ví dụ: Nhân tộc, Yêu tộc)
    public string sex;     // Giới tính (Nam / Nữ / Khác)

    public override string ToString()
    {
        return
            $"Surname: {surName}\n" +
            $"Name: {name}\n" +
            $"Sect: {sect}\n" +
            $"Race: {race}\n" +
            $"Sex: {sex}";
    }
}

/// <summary>
/// Chứa các chỉ số cơ bản liên quan đến sinh lực, năng lượng, và tuổi thọ.
/// </summary>
[System.Serializable]
public class General
{
    public int lifespan;  // Tuổi thọ hiện tại (số năm còn sống)
    public int currLifespan;  // Tuổi thọ hiện tại (số năm còn sống)
    public int vitality;  // Sinh lực (HP), nếu về 0 có thể tử vong
    public int currVitality;  // Sinh lực (HP), nếu về 0 có thể tử vong
    public int energy;    // Năng lượng (MP), dùng cho kỹ năng hoặc pháp thuật
    public int currEnergy;    // Năng lượng (MP), dùng cho kỹ năng hoặc pháp thuật

    public override string ToString()
    {
        return
            $"Lifespan: {currLifespan}/{lifespan}\n" +
            $"Vitality: {currVitality}/{vitality}\n" +
            $"Energy: {currEnergy}/{energy}";
        
    }
}

/// <summary>
/// Chứa các chỉ số chiến đấu cơ bản.
/// </summary>
[System.Serializable]
public class Combat
{
    public int atk;       // Sức tấn công cơ bản
    public int def;       // Phòng thủ, giảm sát thương nhận vào
    public int agility;   // Nhanh nhẹn, ảnh hưởng tốc độ hành động và né đòn

    public override string ToString()
    {
        return 
            $"Attack: {atk}\n" +
            $"Defense: {def}\n" +
            $"Agility: {agility}";
    }
}

/// <summary>
/// Chỉ số võ học – quyết định khả năng dùng từng loại vũ khí.
/// </summary>
[System.Serializable]
public class MartialArts
{
    public int sword; // Kỹ năng dùng kiếm
    public int spear; // Kỹ năng dùng thương
    public override string ToString()
    {
        return
            $"Sword: {sword}\n" +
            $"Spear: {spear}";
    }
}

/// <summary>
/// Căn cơ tu luyện – ảnh hưởng đến khả năng hấp thụ linh khí theo từng hệ.
/// </summary>
[System.Serializable]
public class SpiritualRoot
{
    public int wind;       // Linh căn hệ Phong
    public int fire;       // Linh căn hệ Hỏa
    public int water;      // Linh căn hệ Thủy
    public int lightning;  // Linh căn hệ Lôi
    public int earth;      // Linh căn hệ Thổ
    public int wood;       // Linh căn hệ Mộc

    public override string ToString()
    {
        return
            $"Wind: {wind}\n" +
            $"Fire: {fire}\n" +
            $"Water: {water}\n" +
            $"Lightning: {lightning}\n" +
            $"Earth: {earth}\n" +
            $"Wood: {wood}";
    }
}

public class UnitStats : MonoBehaviour
{
    public virtual void SetPosition(Vector2 position) { }
    public virtual int TakeDamage(int damage) { return 0; }
    public virtual int Heal(int amount) { return 0; }
    public virtual int UseSkill(int idSkill) { return 0; }
}
