using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
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
}

/// <summary>
/// Chỉ số võ học – quyết định khả năng dùng từng loại vũ khí.
/// </summary>
[System.Serializable]
public class MartialArts
{
    public int sword; // Kỹ năng dùng kiếm
    public int spear; // Kỹ năng dùng thương
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
}

/// <summary>
/// Kỹ năng thủ công – dùng trong luyện đan, chế tạo, và các hoạt động sống.
/// </summary>
[System.Serializable]
public class Artisanship
{
    public int alchemy;    // Kỹ năng luyện đan
    public int forge;      // Kỹ năng luyện khí (chế tạo pháp bảo)
    public int fengShui;   // Kỹ năng bố trí trận pháp, phong thủy
    public int talismans;  // Kỹ năng chế tạo bùa chú
    public int mining;     // Kỹ năng khai khoáng, thu thập tài nguyên
}
public class UnitStats : MonoBehaviour
{
    public Information infomation;
    public General general;
    public Combat combat;

    public virtual void UpdateStats() { }
    public virtual void TakeDamage(int amount) { }

}
