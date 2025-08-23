using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 moveInput { get; private set; }
    public bool dashPressed { get; private set; }
    public Joystick joystick;

    void Update()
    {
        moveInput = new Vector2(joystick.input.x, joystick.input.y).normalized;
    }

    public void ClearOneTimeInputs()
    {
        // Dùng để reset các input chỉ nhấn 1 lần
        dashPressed = false;
    }
}
