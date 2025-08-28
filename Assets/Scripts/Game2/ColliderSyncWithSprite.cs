using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(BoxCollider2D))]
public class ColliderSyncWithSprite : MonoBehaviour
{
    private SpriteRenderer sr;
    private BoxCollider2D bc2d;
    private Sprite lastSprite;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        bc2d = GetComponent<BoxCollider2D>();
    }

    void LateUpdate() // dùng LateUpdate để đảm bảo đã render xong frame
    {
        if (sr.sprite != lastSprite)
        {
            UpdateColliderToMatchSprite();
        }
    }

    void UpdateColliderToMatchSprite()
    {
        lastSprite = sr.sprite;

        if (lastSprite == null)
            return;

        Vector2 size = lastSprite.bounds.size;
        Vector2 offset = lastSprite.bounds.center;

        bc2d.size = size;
        bc2d.offset = offset;
    }
}
