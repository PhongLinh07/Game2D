using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick_uGUI : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public static Joystick_uGUI Instance { get; private set; }

    public RectTransform background;  // Nền Joystick_uGUI
    public RectTransform handle;      // Cần Joystick_uGUI
    public Vector2 Input { get; private set; }             // Giá trị di chuyển -1..1

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
        Input = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(background, eventData.position, null, out pos);

        pos.x = (pos.x / background.sizeDelta.x) * 2;
        pos.y = (pos.y / background.sizeDelta.y) * 2;

        Input = new Vector2(pos.x, pos.y);
        Input = (Input.magnitude > 1.0f) ? Input.normalized : Input;

        handle.anchoredPosition = new Vector2(Input.x * (background.sizeDelta.x / 2), Input.y * (background.sizeDelta.y / 2));
    }
}
