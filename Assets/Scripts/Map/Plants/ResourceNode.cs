using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Là một Attribute trong Unity C#, nó yêu cầu GameObject phải có component BoxCollider2D khi gắn script này.
[RequireComponent(typeof(BoxCollider2D))]
public class ResourceNode : MonoBehaviour, IDamageable
{
    public EnhanceCfgItem thisPlayerItem;
    public int quantityPlayerItem = 4;
    public SpriteRenderer mSr;
    public ResourceNodeType resourceNodeType;
    private Animator animator;// của Character

    private float OFFSET = 1.0f;
    private Vector2 mSize; //kích thước this
    private float mHp = 100.0f;

  

    private void Awake()
    {
        if (mSr) mSize = mSr.bounds.size;
        else
        {
            Debug.Log("SpriteRenderer mSr is null!");
        }

    }

    public void TakeDamage(int amount)
    {
        if (mHp > 0.0f)
        {
            mHp -= amount;
        }
        else
        {
            for(int i = 0; i < quantityPlayerItem; i++) 
            {
                Vector3 pos = transform.position;
                pos.x += OFFSET * UnityEngine.Random.value; //UnityEngine.Random.value: [0, 1)
                pos.y += OFFSET * UnityEngine.Random.value;

                ItemSpawnManager.Instance.SpawnItem(pos, thisPlayerItem);
            }
            Destroy(gameObject);
        }
    }

    public bool CanTakeDamage(List<ResourceNodeType> canTakeDamage)
    {
        return canTakeDamage.Contains(resourceNodeType);
    }
}
