using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMouseHandler : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Vector2 direction = Vector2.zero;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = (mouseWorldPos - transform.position).normalized;
        direction.y = 0.0f;

        if (direction.x != 0.0f)
        {
            spriteRenderer.flipX = direction.x < -0.01f;
        }
    }
}
