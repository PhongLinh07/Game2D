using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public static Joystick Instance { get; private set; }

    public RectTransform background;  // Nền joystick
    public RectTransform handle;      // Cần joystick
    public Vector2 input;             // Giá trị di chuyển -1..1

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        input = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(background, eventData.position, null, out pos);

        pos.x = (pos.x / background.sizeDelta.x) * 2;
        pos.y = (pos.y / background.sizeDelta.y) * 2;

        input = new Vector2(pos.x, pos.y);
        input = (input.magnitude > 1.0f) ? input.normalized : input;

        handle.anchoredPosition = new Vector2(input.x * (background.sizeDelta.x / 2), input.y * (background.sizeDelta.y / 2));
    }
}
