using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class YSort : MonoBehaviour
{
    // Tăng Multiplier lên 1000 để tạo khoảng cách lớn (1000 đơn vị) cho mỗi mét di chuyển.
    private const int SORTING_ORDER_MULTIPLIER = 100;

    // Giới hạn Tie-Breaker ở 99 (nhỏ hơn 1000 rất nhiều).
    private const int TIE_BREAKER_MAX = 10;

    private SpriteRenderer spriteRenderer;
    private int tieBreaker;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Tie-Breaker (0-99) sẽ là phần ngẫu nhiên, độc lập với Y-Sort Value.
        tieBreaker = gameObject.GetInstanceID() % TIE_BREAKER_MAX;
    }

    void LateUpdate()
    {
        // 1. Tính Y-Sort Value: Sử dụng FloorToInt(-Y * 1000)
        // Giá trị này sẽ có khoảng cách ít nhất 1000 đơn vị so với Y-Sort Value tiếp theo.
        int y_sort_value = Mathf.FloorToInt(-transform.position.y * SORTING_ORDER_MULTIPLIER);

        // 2. Cộng Tie-Breaker (0-99)
        // Vì Tie-Breaker (max 99) nhỏ hơn rất nhiều so với khoảng cách giữa các Y-Sort Value (min 1000), 
        // nó không bao giờ có thể gây ra hiện tượng đảo ngược.
        spriteRenderer.sortingOrder = y_sort_value + tieBreaker;
    }
}