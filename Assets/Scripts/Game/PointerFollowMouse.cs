using Unity.VisualScripting;
using UnityEngine;

public class PointerFollowMouse : MonoBehaviour
{
    public Transform playerTransform;
    [SerializeField] private const float distanceWithOwner= 1.33f;
    [SerializeField] private const float angleOffset = -28.99f;
    void Update()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;

        Vector3 dir = (mouseWorldPos - playerTransform.position).normalized;

        // Gán vị trí mũi tên cách player 1 đoạn theo hướng chuột
        transform.position = playerTransform.position + dir * distanceWithOwner; // distance = bán kính xoay

        // Quay mũi tên để hướng về chuột
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + angleOffset);

    }
}
