using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public enum ResourceNodeType
{
    None,   // không phải resource node
    Tree,   // cây (gather bằng rìu)
    Ore     // quặng chưa tinh chế, vẫn còn nằm trong đất đá.
}

[CreateAssetMenu(menuName = "Data/Tool action/Gather Resource Node")]
public class GatherResourceNode : ToolAction
{
    private Vector2 sizeAreaEffect = new Vector2(2, 1);

    public List<ResourceNodeType> canTakeDamageOfType; // List các kiểu ResourceNodeType có thể nhận damage

    public override bool OnApply(Vector2 worldPoint)
    {
        Collider2D collider = Physics2D.OverlapBox(worldPoint, sizeAreaEffect, 0.0f, LayerMask.GetMask("Damageable"));


        if (collider == null) return false;

        IDamageable go = collider.GetComponent<IDamageable>(); //go ám chỉ DamageableObjects
        if (go != null)
        {
            if(go.CanTakeDamage(canTakeDamageOfType))
            {
                go.TakeDamage(25);
                return true;
            }            
        }



        return false;
    }
}
