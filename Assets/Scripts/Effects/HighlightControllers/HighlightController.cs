using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightController : MonoBehaviour
{
    private GameObject mOwner;
    private Vector2 mSizeOfOwner = new Vector2(0.0f, 0.0f);

    public SpriteRenderer mSr;
    private Vector2 mSizeOfThis = new Vector2(0.0f, 0.0f);


    private void Awake()
    {
        if(mSr) mSizeOfThis = mSr.bounds.size;
        else
        {
            Debug.Log("SpriteRenderer mSr is Null!");
        }

        gameObject.SetActive(false);
    }
    void Update()
    {
        
        if (gameObject.activeSelf && mOwner)
        {
            Vector3 posHl = mOwner.transform.position;
            posHl.y += (mSizeOfOwner.y + mSizeOfThis.y)/2;

            transform.position = posHl;
        }
    }

    public void SetHighlightOwner(GameObject owner)
    {
    
        if (!owner) return;

        mOwner = owner;
        mSizeOfOwner = mOwner.GetComponent<SpriteRenderer>().bounds.size;
        
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
