using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAroundEffect : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private float sortingWithOwner;

    public Transform PrivotWithOwner;       // vị trí trung tâm, thường là Player
    public float radiusX = 2.0f;   // bán kính ngang (elip theo trục X)
    public float radiusY = 0.7f;   // bán kính dọc (elip theo trục Y)
    public float offsetAngleDeg = 0f; // để chỉnh lệch góc nếu cần

    
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if(Input.touchCount <= 0) return;

        // Lấy hướng chuột so với player
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        mouseWorld.z = 0;
        Vector3 dir = (mouseWorld - PrivotWithOwner.position).normalized;

        // Lấy góc từ hướng chuột
        float angle = Mathf.Atan2(dir.y, dir.x); // tính bằng radian
        angle += offsetAngleDeg * Mathf.Deg2Rad; // cộng thêm góc nếu cần

        // Tính vị trí mới theo hình elip
        float x = -Mathf.Cos(angle) * radiusX;
        float y = Mathf.Sin(angle) * radiusY;

        transform.position = Vector3.Lerp(transform.position, PrivotWithOwner.position + new Vector3(x, y, 0), 4.0f * Time.deltaTime);

        if(mouseWorld.y <= PrivotWithOwner.position.y)
        {
            sortingWithOwner = -(mouseWorld.y - 50.0f) * 100.0f ;
        }
        else
        {
            sortingWithOwner = -mouseWorld.y * 100.0f;
        }
        spriteRenderer.sortingOrder = Mathf.RoundToInt(sortingWithOwner);
    }
}
 