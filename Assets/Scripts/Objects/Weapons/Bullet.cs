using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : PoolableObject
{

    private int atkNum = 1;

    public Vector3 beginPos;
    private Vector3 directionMove;

    public int radius;
    public int moveSpeed;
    public string tagOfTarget;

    private Vector2 aoeSize = Vector2.zero;
    private Vector2 pivotCenter = Vector2.zero;
    private BoxCollider2D box2D;

    private float lastUpdateTime = -1;
    private float deltaTime = 0.1f;


    public override void OnSpawn() { }

    public override void OnReturn() { }

    // Start is called before the first frame update
    private void Start()
    {
        transform.parent = null;
        box2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameObject.activeSelf) return;

        if ((transform.position - beginPos).magnitude > radius)
        {
            ReturnToPool(gameObject);
            return;
        }

        transform.position = transform.position + directionMove * moveSpeed * Time.deltaTime;
        if(Time.time >= lastUpdateTime + deltaTime) DealDamage();
    }

    public void Init(Vector2 beginPos, float angle, Vector2 direction, int attackValue)
    {
        transform.position = beginPos;
        this.beginPos = beginPos;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        directionMove = direction.normalized;
        atkNum = attackValue;
    }


    private void DealDamage()
    {
        lastUpdateTime = Time.time;
        InitOverlapBox();
        Collider2D hit = Physics2D.OverlapBox(pivotCenter, aoeSize, transform.eulerAngles.z, LayerMask.GetMask(tagOfTarget));

        if (hit)
        {
            hit.gameObject.GetComponent<HPController>().TakeDamage(atkNum);
            ReturnToPool(gameObject);
        }

    }

    private void InitOverlapBox()
    {
        pivotCenter = transform.TransformPoint(box2D.offset);
        aoeSize = Vector2.Scale(box2D.size, transform.lossyScale);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Matrix4x4 rot = Matrix4x4.TRS(pivotCenter, Quaternion.Euler(0, 0, transform.eulerAngles.z), Vector3.one);
        Gizmos.matrix = rot;
        Gizmos.DrawWireCube(Vector3.zero, aoeSize);
        Gizmos.matrix = Matrix4x4.identity;
    }

}
