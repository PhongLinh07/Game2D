using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform owner;        // Player hoặc đối tượng muốn theo
    private float smoothSpeed = 3.5f;
    public Vector3 offset = new Vector3(0.0f, 0.0f, -10);
    void FixedUpdate()
    {
        if (owner == null) return;

        Vector3 desiredPosition = owner.position + offset;
        Vector3 smoothed = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Làm tròn x và y tới 2 chữ số thập phân
        float roundedX = Mathf.Round(smoothed.x * 100f) / 100f;
        float roundedY = Mathf.Round(smoothed.y * 100f) / 100f;

        transform.position = new Vector3(roundedX, roundedY, transform.position.z); // giữ z

        // transform.position = new Vector3(owner.transform.position.x, owner.transform.transform.position.y, -10);
    }
}
