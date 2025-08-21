using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingSword : PoolableObject
{
    public Vector2 destPos; //destination position
    private int atkNum = 1;
 
    public string tagOfTarget;
    public Animator animator;

    private Vector2 aoeSize = Vector2.zero;
    private Vector2 pivotCenter = Vector2.zero;
    private BoxCollider2D box2D;

    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
        box2D = GetComponent<BoxCollider2D>();
    }


    public void Init(Vector2 destination, int attackValue)
    {
        transform.position = destination;
        destPos = destination;
        atkNum = attackValue;
        animator.Play("Anim_SwordRain");
    }

    private void DealDamage()
    {
        InitOverlapBox();
        Collider2D[] hits = Physics2D.OverlapBoxAll(pivotCenter, aoeSize, 0, LayerMask.GetMask(tagOfTarget));

        foreach(Collider2D hit in hits)
        {
            if (hit)
            {
                hit.gameObject.GetComponent<HPController>().TakeDamage(atkNum * 2);
            }
        }
    }

    private void EndAttack()
    {
        ReturnToPool(gameObject);
    }


    private void InitOverlapBox()
    {
        pivotCenter = transform.TransformPoint(box2D.offset);
        aoeSize = Vector2.Scale(box2D.size, transform.lossyScale);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(pivotCenter, aoeSize);
    }
}
