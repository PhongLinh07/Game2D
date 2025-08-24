using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player
public enum EAnimParametor
{
    None = 0,          // Không có
    Speed = 1,         // Tốc độ di chuyển
    Attack = 2,        // Tấn công
    Skill = 3,         // Dùng kỹ năng
    LeftHand = 4,      // Hành động tay trái
    UseMove = 5,       // Sử dụng di chuyển đặc biệt
    Sleep = 6,         // Ngủ
    Sit = 7,           // Ngồi
    Idle = 8,          // Đứng yên
    PickUp = 9,        // Nhặt đồ
    ComboNum = 10,     // Số lần combo
    Building = 11,     // Đang xây dựng
    Builded = 12,      // Xây dựng xong
    Runfast = 13,      // Chạy nhanh
    Run = 14,          // Chạy
    Strength = 15,     // Dùng sức mạnh
    AtkSpecial = 16,   // Đòn tấn công đặc biệt
    Wtype = 17,        // Loại vũ khí
    Specialover = 18,  // Kết thúc chiêu đặc biệt
    UseItem = 19,      // Sử dụng vật phẩm
    HitTree = 20,      // Chặt cây
    Down = 21,         // Ngã xuống
    PlayRecoverGrid = 22,  // Bắt đầu hồi phục (trên ô lưới)
    EndRecoverGrid = 23,   // Kết thúc hồi phục
    Open = 24,         // Mở (rương, cửa…)
    State = 25,        // Trạng thái chung
    Die = 26,          // Chết
    IdleState = 27,    // Trạng thái đứng yên
    SkillValue = 28,   // Giá trị kỹ năng
    Hit = 29,          // Bị đánh trúng
    Top = 30,          // Vị trí trên (top view hoặc cao nhất)
    PlayBuildGrid = 31, // Bắt đầu xây dựng (trên ô lưới)
    EndBuildGrid = 32,  // Kết thúc xây dựng
    SkillOver = 33,    // Kết thúc kỹ năng
    RunAway = 34,      // Bỏ chạy
    ReSearch = 35,     // Tìm lại mục tiêu
    SkillInterrupt = 36,// Bị ngắt kỹ năng
    Hide = 37,         // Ẩn mình
    SpeedMult = 38,    // Hệ số tốc độ
    HaveWeapon = 39,   // Có vũ khí
    Warning = 40,      // Cảnh báo
    DirX = 41,         // Hướng X
    DirY = 42,         // Hướng Y
    Turn = 43,         // Quay người
    IdleState1 = 44,   // Trạng thái Idle phụ
    Born = 45,         // Sinh ra / xuất hiện
    SkillPlayer = 46,  // Người chơi dùng kỹ năng
    Show = 47          // Hiện ra (xuất hiện)
}

public enum EFsmAction
{
    SetCanMove = 1,         // Bật/tắt khả năng di chuyển
    Attack = 2,             // Tấn công thường
    AffectSkill = 3,        // Kích hoạt hiệu ứng kỹ năng
    BeginAffectSkill = 4,   // Bắt đầu hiệu ứng kỹ năng
    BeginAttack = 5,        // Chuẩn bị ra đòn tấn công
    Dash = 6,               // Lướt/Dash
    EnterSpecialState = 7,  // Vào trạng thái đặc biệt
    ExitSpecialState = 8,   // Thoát trạng thái đặc biệt
    SetFastMultSpeed = 9,   // Tăng tốc độ tạm thời (nhân hệ số tốc độ)
    BeginSpecialAttack = 10,// Chuẩn bị tấn công đặc biệt
    SpecialAttack = 11,     // Tấn công đặc biệt
    BowStartAttack = 12,    // Bắt đầu bắn cung
    BowSpecialAttack = 13,  // Đòn cung đặc biệt
    Die = 14,               // Chết
    Hit = 15,               // Bị đánh trúng
    ToolAttackStart = 16,   // Bắt đầu dùng công cụ tấn công (rìu, cuốc…)
    ToolAttack = 17,        // Dùng công cụ tấn công
    Boom = 18               // Nổ (bom, kỹ năng nổ…)
}
