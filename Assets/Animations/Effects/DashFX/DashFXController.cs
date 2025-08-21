using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class DashFXController : MonoBehaviour
{
    private SpriteRenderer sr;
    private Animator animator;


    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        sr.enabled = false;
    }

    public void Dashing(Vector2 position, Vector2 direction)
    {
        transform.position = position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        sr.enabled = true;
        animator.SetTrigger("DashFX"); // Phát lại từ đầu mỗi lần dash
    }

    private void End()
    {
        sr.enabled = false;
    }

}
