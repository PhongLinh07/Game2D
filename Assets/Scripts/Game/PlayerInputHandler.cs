using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 moveInput { get; private set; }
    public bool dashPressed { get; private set; }

    void Update()
    {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }

    public void ClearOneTimeInputs()
    {
        // Dùng để reset các input chỉ nhấn 1 lần
        dashPressed = false;
    }
}
