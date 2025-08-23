using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectState : MonoBehaviour
{
    public Transform bottomTrans;
    public Transform centerTrans;

    public Vector2 lookDirection = Vector2.zero;
    public Vector2 moveDirection = Vector2.zero;
    public bool isAttacking = false;
}
