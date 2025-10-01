using UnityEngine;
using UnityEngine.Rendering.Universal; // PixelPerfectCamera

public class CameraFollowPixelPerfect : MonoBehaviour
{
    public static CameraFollowPixelPerfect Instance;

    [Header("Camera settings")]
    public Vector3 offset = new Vector3(0f, 0f, -10f);
    public float smoothSpeed = 5f;

    [Header("Dynamic offset (look-ahead/cutscene)")]
    public Vector3 farViewOffset = Vector3.zero;

    [Header("Optional PixelPerfect")]
    public bool usePixelPerfect = true;

    private Transform owner;
    private PixelPerfectCamera ppc;

    private void Awake()
    {
        Instance = this;

        // Lấy PixelPerfectCamera nếu có
        if (usePixelPerfect)
            ppc = GetComponent<PixelPerfectCamera>();

        // Đăng ký event clone player
        Bootstrapper.Instance.eventWhenCloneCharacter += Init;
    }

    private void OnDestroy()
    {
        if (Bootstrapper.Instance != null)
            Bootstrapper.Instance.eventWhenCloneCharacter -= Init;
    }

    // Init khi player clone xong
    private void Init(LogicCharacter logicCharacter)
    {
        owner = logicCharacter.transCenter;
        transform.position = owner.position + offset;
    }

    private void LateUpdate()
    {
        if (owner == null) return;

        // Vị trí mong muốn
        Vector3 desiredPos = owner.position + offset + farViewOffset;

        // Smooth follow
        Vector3 smoothed = Vector3.Lerp(transform.position, desiredPos, smoothSpeed * Time.deltaTime);

        // Round pixel nếu PixelPerfectCamera
        if (usePixelPerfect && ppc != null)
        {
            smoothed.x = Mathf.Round(smoothed.x * ppc.assetsPPU) / ppc.assetsPPU;
            smoothed.y = Mathf.Round(smoothed.y * ppc.assetsPPU) / ppc.assetsPPU;
        }
        else
        {
            // Round 2 chữ số thập phân nếu không dùng PixelPerfect
            smoothed.x = Mathf.Round(smoothed.x * 100f) / 100f;
            smoothed.y = Mathf.Round(smoothed.y * 100f) / 100f;
        }

        transform.position = new Vector3(smoothed.x, smoothed.y, offset.z);
    }

    // Snap camera ngay lập tức về player
    public void SnapToPlayer()
    {
        if (owner != null)
            transform.position = owner.position + offset;
    }

    // Cập nhật dynamic offset
    public void SetFarViewOffset(Vector3 off)
    {
        farViewOffset = off;
    }

    // Thay đổi resolution runtime cho PixelPerfectCamera
    public void SetPixelPerfectResolution(int x, int y)
    {
        if (ppc != null)
        {
            ppc.refResolutionX = x;
            ppc.refResolutionY = y;
        }
    }
}
