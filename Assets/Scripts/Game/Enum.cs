public enum EAttribute
{
    // ===================== Tư chất =====================
    Hp = 0,          // Máu tối đa của nhân vật
    Mana = 1,        // Mana hoặc linh lực tối đa
    Speed = 2,          // Tốc độ di chuyển hoặc hành động
    Attack = 3,         // Lực tấn công vật lý
    Defense = 4,        // Phòng thủ vật lý, giảm sát thương nhận vào
    WindResist = 5,     // Kháng gió, giảm sát thương thuộc tính gió
    WaterResist = 6,    // Kháng thủy, giảm sát thương thuộc tính nước
    FireResist = 7,     // Kháng hỏa, giảm sát thương thuộc tính lửa
    LightningResist = 8,// Kháng lôi, giảm sát thương thuộc tính sấm sét
    WoodResist = 9,     // Kháng mộc, giảm sát thương thuộc tính gỗ
    EarthResist = 10,   // Kháng thổ, giảm sát thương thuộc tính đất
    HpRegen = 11,       // Hồi máu theo thời gian
    MpRegen = 12,       // Hồi mana/linh lực theo thời gian

    // ===================== Linh căn =====================
    WindCore = 13,       // Phong linh căn, tăng sát thương/kháng gió
    FireCore = 14,       // Hỏa linh căn, tăng sát thương/kháng hỏa
    WaterCore = 15,      // Thủy linh căn, tăng sát thương/kháng thủy
    WoodCore = 16,       // Mộc linh căn, tăng sát thương/kháng mộc
    EarthCore = 17,      // Thổ linh căn, tăng sát thương/kháng đất
    LightningCore = 18,  // Lôi linh căn, tăng sát thương/kháng lôi
    MetalCore = 19,      // Kim linh căn, tăng sát thương/kháng kim

    // ===================== Pháp khí =====================
    Hardness = 20,           // Độ cứng của pháp khí, giảm sát thương vật lý tác động vào
    Durability = 21,         // Độ bền, giảm hao mòn/phá hủy của pháp khí
    PhysicalPenetrate = 22,  // Xuyên giáp vật lý, bỏ qua một phần phòng thủ kẻ địch
    MagicPenetrate = 23,     // Xuyên giáp phép, bỏ qua một phần kháng phép kẻ địch
    CritRate = 24,           // Tỷ lệ chí mạng (%)
    CritDamage = 25,         // Sát thương chí mạng (multiplier)

    // ===================== Thời gian / tiêu hao =====================
    Duration = 26,           // Thời gian duy trì hiệu ứng, buff, pháp thuật
    Cooldown = 27,           // Thời gian hồi chiêu hoặc thời gian chờ
    EnergyCost = 28,         // Năng lượng/linh lực tiêu hao khi dùng skill
    HpCost = 29,              // Máu tiêu hao khi dùng skill đặc biệt
    Lifespan = 30


}

public enum EEquipmentType
{
    None = 0,
    Head = 1,       // Trang bị đầu (mũ, nón, v.v.)
    Outfit = 2,     // Trang phục (áo, quần, y phục)
    Shoes = 3,      // Giày, dép
    Ring = 4,       // Nhẫn
    Bracelet = 5,   // Vòng tay
    Necklace = 6,   // Vòng cổ
    Weapon = 7

}
