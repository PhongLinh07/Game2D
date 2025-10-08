using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IndicatorManager : MonoBehaviour
{
    public static IndicatorManager Instance;

    [Header("Indicator Prefabs")]
    public GameObject targetPrefab;
    public GameObject rotatePrefab;

    // Giữ các instance của Indicator đang dùng (key là kiểu input)
    private readonly Dictionary<SkillInputType, IndicatorBase> indicators = new();

    private void Awake()
    {
        Instance = this;

        // ✅ Khởi tạo sẵn từng prefab
        if (targetPrefab != null)
        {
            indicators[SkillInputType.Drag] = targetPrefab.GetComponent<IndicatorBase>();
            indicators[SkillInputType.Drag].gameObject.SetActive(false);
        }

        if (rotatePrefab != null)
        {
            indicators[SkillInputType.Rotate] = rotatePrefab.GetComponent<IndicatorBase>();
            indicators[SkillInputType.Rotate].gameObject.SetActive(false);
        }
    }

    // 🌀 Hiển thị indicator xoay quanh pivot (VD: nhân vật)
    public GameObject Show(SkillInputType type, Vector3 pivot, Quaternion rot)
    {
        if (!indicators.TryGetValue(type, out var ind))
        {
            Debug.LogWarning($"IndicatorManager: Không tìm thấy indicator cho {type}");
            return null;
        }

        // ✅ Tính hướng di chuyển dựa trên rotation
        Vector3 dir = rot * Vector3.right;

        // ✅ Đặt vị trí xoay quanh pivot
        Vector3 pos = pivot + dir * ind.distanceWithPivot;

        // ✅ Giữ góc giống UI (UI xoay theo screen-space)
        Quaternion fixedRot = Quaternion.Euler(0f, 0f, rot.eulerAngles.z + ind.angleOffset);

        // ✅ Cập nhật vị trí & góc xoay
        ind.transform.SetPositionAndRotation(pos, fixedRot);
        ind.gameObject.SetActive(true);
        return ind.gameObject;
    }

    // 🧭 Cập nhật indicator đang hoạt động
    public void UpdateIndicator(SkillInputType type, Vector3 pivot, Quaternion rot)
    {
        if (!indicators.TryGetValue(type, out var ind) || !ind.gameObject.activeSelf)
            return;

        // ✅ Tính hướng di chuyển dựa trên rotation
        Vector3 dir = rot * Vector3.right;

        // ✅ Đặt vị trí xoay quanh pivot
        Vector3 pos = pivot + dir * ind.distanceWithPivot;

        // ✅ Giữ góc giống UI (UI xoay theo screen-space)
        Quaternion fixedRot = Quaternion.Euler(0f, 0f, rot.eulerAngles.z + ind.angleOffset);

        // ✅ Cập nhật vị trí & góc xoay
        ind.transform.SetPositionAndRotation(pos, fixedRot);
    }

    // 🚫 Ẩn indicator
    public void Hide(SkillInputType type)
    {
        if (indicators.TryGetValue(type, out var ind)) ind.gameObject.SetActive(false);
    }
}
