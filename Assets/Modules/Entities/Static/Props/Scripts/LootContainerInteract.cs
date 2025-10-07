using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootContainerInteract: MonoBehaviour, IInteractable
{
    public Sprite[] mSprite; // 0: Close; 1: Opened
    public SpriteRenderer mSr;
    private bool mIsOpened = false;

    private void Awake()
    {
        if (mSr == null) Debug.Log("SpriteRenderer mSr is Null!");
    }

    public void Interact()
    {
        mIsOpened = !mIsOpened;
        mSr.sprite = mIsOpened ? mSprite[1] : mSprite[0];
    }
}
