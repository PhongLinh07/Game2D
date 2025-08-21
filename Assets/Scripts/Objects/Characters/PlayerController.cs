using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private AnimatorController animatorController;
    private ObjectState objState;

    public GameObject dashFX_Prefab;
    private DashFXController dashFX_Controller;

    private void Start()
    {
        animatorController = GetComponent<AnimatorController>();
        objState = GetComponent<ObjectState>();
        dashFX_Controller = dashFX_Prefab.GetComponent<DashFXController>();
    }

    private void Update()
    {
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        objState.lookDirection.x = worldMousePos.x - transform.position.x;
      //  animatorController.direction = objState.lookDirection.normalized;

        // Gọi Dash nếu đủ điều kiện
        if (Input.GetKeyDown(KeyCode.Space))
        {
           // dashFX_Controller.Dashing(animatorController.direction);
        }

        //dashFX_Controller.IsEnd();

    }
}
