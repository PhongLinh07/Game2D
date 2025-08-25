//========== SkillData (Dữ liệu động runtime của 1 skill trên player/enemy) ==========
//
// Phân biệt tĩnh - động:
// - SkillCfgItem/SkillConfig: DỮ LIỆU TĨNH (không đổi) – từ file config.
// - SkillData: DỮ LIỆU ĐỘNG (thay đổi theo runtime), ví dụ: level hiện tại của skill, baseId tham chiếu, ....
//

public class SkillData
{
    public int mSkillId;      // ID cá thể (nếu anh có hệ thống unique id cho skill instance), không bắt buộc
    public int mBaseId;       // Tham chiếu tới SkillCfgItem.id (dùng để tra config tĩnh)
    public int mLv;           // Level hiện tại của skill trong runtime
    public SkillCfgItem mCfg; // Cache con trỏ tới config tĩnh (tiện truy cập, tránh tra dict nhiều lần)
}
