using UnityEngine;
using UnityEngine.UIElements;

public class Joystick : MonoBehaviour
{
    public static Joystick Instance { get; private set; }

    [Header("Joystick Settings")]
    public float maxRadius = 64.0f;
    public float deadZone = 0.1f;

    [Header("UI Toolkit References")]
    public UIDocument uiDocument;

    private VisualElement root;
    private VisualElement handle;

    private Vector2 input;
    private bool isDragging;

    public Vector2 Input => input;

    private void Awake()
    {
        Instance = this;
        uiDocument = GetComponent<UIDocument>();

        root = uiDocument.rootVisualElement.Q<VisualElement>("root");
        handle = root.Q<VisualElement>("Handle");

        root.RegisterCallback<PointerDownEvent>(OnPointerDown);
        root.RegisterCallback<PointerMoveEvent>(OnPointerMove);
        root.RegisterCallback<PointerUpEvent>(OnPointerUp);
    }

    private void OnPointerDown(PointerDownEvent evt)
    {
        isDragging = true;
        root.CaptureMouse();
        UpdateHandle(evt.position);
    }

    private void OnPointerMove(PointerMoveEvent evt)
    {
        if (!isDragging) return;
        UpdateHandle(evt.position);
    }

    private void OnPointerUp(PointerUpEvent evt)
    {
        isDragging = false;
        root.ReleaseMouse();

        input = Vector2.zero;

        // Reset handle về giữa
        Vector2 center = root.layout.size / 2f;
        handle.style.left = center.x - handle.layout.width / 2f;
        handle.style.top = center.y - handle.layout.height / 2f;
    }

    private void UpdateHandle(Vector2 screenPos)
    {
        // Đổi vị trí chuột sang local position của root
        Vector2 localPos = root.WorldToLocal(screenPos);

        // Tính offset từ tâm joystick
        Vector2 offset = localPos - root.layout.size / 2f;

        // 👉 ĐẢO TRỤC Y để khi kéo lên => input.y > 0
        offset.y *= -1f;

        offset = Vector2.ClampMagnitude(offset, maxRadius);

        // Tính input - giá trị normalized (-1..1)
        input = offset / maxRadius;
        if (input.magnitude < deadZone)
            input = Vector2.zero;

        // 👉 Khi set handle thì phải đảo lại trục Y để hiển thị đúng hướng kéo
        handle.style.left = root.layout.size.x / 2f + offset.x - handle.layout.width / 2f;
        handle.style.top = root.layout.size.y / 2f - offset.y - handle.layout.height / 2f;
    }
}
