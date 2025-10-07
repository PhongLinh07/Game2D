using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class CharacterInteractController : MonoBehaviour
{
    const float AoE = 2.0f;
    private Vector2 SIZE_INTTERACT_AREA = new Vector2(0.25f, 0.25f);
    private Vector2 mSizeOfChar = new Vector2(0.0f, 0.0f);
    private HighlightController mHlCtr;
   
    private void Start()
    {
        mHlCtr = GameManager.Instance.HighlightController.GetComponent<HighlightController>();
        if (!mHlCtr) Debug.Log("HighlightController mHlCtr is Null!");
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Interact();
        }
    }
    
    private void Interact()
    {
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float dis = Vector2.Distance(transform.position, worldMousePos); // tự chuẩn hóa Vector2
       // Debug.DrawLine(transform.position, worldMousePos, Color.red);

       // Debug.Log("Distance interact: " + dis);

        if (dis > AoE)
        {
            if (mHlCtr) mHlCtr.Hide();
            return;
        }

        Collider2D collider = Physics2D.OverlapBox(worldMousePos, SIZE_INTTERACT_AREA, 0.0f, LayerMask.GetMask("Interactable"));


        if (collider == null)
        {
            if (mHlCtr) mHlCtr.Hide();
            return;
        }

        IInteractable go = collider.GetComponent<IInteractable>();
        if (go != null)
        {
            if (mHlCtr) mHlCtr.SetHighlightOwner(collider.gameObject);
            go.Interact();
        }
        else
        {
            if (mHlCtr) mHlCtr.Hide();
        }
    }
}
