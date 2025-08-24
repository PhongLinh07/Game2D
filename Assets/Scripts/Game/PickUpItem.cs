using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Rendering;

// gan cho ItemObjects
public class PickUpItem : MonoBehaviour
{

    private Transform mTransOfChar;
    private const float SPEED = 5.0f;
    private const float PICK_UP_DISTANCE = 1.5f;
    private float mAliveTime = 10.0f;

    private EnhanceCfgItem mPlayerItem;
   

    // Start is called before the first frame update
    void Start()
    {
        mTransOfChar = GameManager.Instance.Character.transform;
    }


    public void SetItemPickUp(EnhanceCfgItem playerItem)
    {

        mPlayerItem = new EnhanceCfgItem();
        mPlayerItem.CopyFrom(playerItem); // ✨ Copy thay vì giữ tham chiếu
       
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.sprite = mPlayerItem.Data.Icon;
    }


    // Update is called once per frame
    void Update()
    {
  
        mAliveTime -= Time.deltaTime;
        if(mAliveTime < 0) Destroy(gameObject);

        float dis = Vector3.Distance(mTransOfChar.position, transform.position);

        if( dis > PICK_UP_DISTANCE) return;

        transform.position = Vector3.MoveTowards(transform.position, mTransOfChar.position, SPEED * Time.deltaTime);

        if (dis < 0.1f)
        {
            //*TODO* Should be moved into apecified controller rather than being checked here.
            if (GameManager.Instance)
            {
              //  GameManager.Instance.InventoryContiner.Add(mPlayerItem.item, mPlayerItem.quantity);
            }
            else
            {
                Debug.Log("No InventoryContiner attached to the GameManager!");
            }
            Destroy(gameObject);
        }
    }
}
