using UnityEngine;
using UnityEngine.UIElements;

public class Joystick_Toolkit : MonoBehaviour
{
    public static Joystick_Toolkit Instance { get; private set; }

    [Header("Joystick_Toolkit Settings")]
    public float maxRadius = 64.0f;
    public float deadZone = 0.1f;

    [Header("UI Toolkit References")]
    public UIDocument uiDocument;

    private VisualElement root;
    private VisualElement background;
    private VisualElement handle;

    private Vector2 input;
    private bool isDragging;

    public Vector2 Input => input;

    private void Awake()
    {
        Instance = this;
        uiDocument = GetComponent<UIDocument>();

        root = uiDocument.rootVisualElement;
        background = root.Q<VisualElement>("Background");
        handle = background.Q<VisualElement>("Handle");

        root.RegisterCallback<PointerDownEvent>(OnPointerDown);

    }

    private int? activePointerId = null;

    private void OnPointerDown(PointerDownEvent evt)
    {
        if (activePointerId == null)
        {
            activePointerId = evt.pointerId;
            isDragging = true;
            UpdateHandle(evt.position);

            // Lắng nghe toàn panel
            root.panel.visualTree.RegisterCallback<PointerMoveEvent>(OnPointerMoveGlobal);
            root.panel.visualTree.RegisterCallback<PointerUpEvent>(OnPointerUpGlobal);
        }
    }

    private void OnPointerMoveGlobal(PointerMoveEvent evt)
    {
        if (evt.pointerId != activePointerId) return;
        UpdateHandle(evt.position);
    }

    private void OnPointerUpGlobal(PointerUpEvent evt)
    {
        if (evt.pointerId != activePointerId) return;
        isDragging = false;
        activePointerId = null;

        input = Vector2.zero;

        // Reset handle về giữa
        Vector2 center = background.layout.size / 2f;
        handle.style.left = center.x - handle.layout.width / 2f;
        handle.style.top = center.y - handle.layout.height / 2f;

        // Hủy callback toàn panel
        root.panel.visualTree.UnregisterCallback<PointerMoveEvent>(OnPointerMoveGlobal);
        root.panel.visualTree.UnregisterCallback<PointerUpEvent>(OnPointerUpGlobal);
    }

    private void UpdateHandle(Vector2 screenPos)
    {
        // Đổi vị trí chuột sang local position của root
        Vector2 localPos = background.WorldToLocal(screenPos);

        // Tính offset từ tâm Joystick_Toolkit
        Vector2 offset = localPos - background.layout.size / 2f;

        // 👉 ĐẢO TRỤC Y để khi kéo lên => input.y > 0
        offset.y *= -1f;

        offset = Vector2.ClampMagnitude(offset, maxRadius);

        // Tính input - giá trị normalized (-1..1)
        input = offset / maxRadius;
        if (input.magnitude < deadZone)
            input = Vector2.zero;

        // 👉 Khi set handle thì phải đảo lại trục Y để hiển thị đúng hướng kéo
        handle.style.left = background.layout.size.x / 2f + offset.x - handle.layout.width / 2f;
        handle.style.top = background.layout.size.y / 2f - offset.y - handle.layout.height / 2f;
    }

}