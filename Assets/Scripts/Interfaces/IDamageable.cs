using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public void TakeDamage(int amount); // ko đc triển khai tại đây

    public bool CanTakeDamage(List<ResourceNodeType> canTakeDamage);
}
