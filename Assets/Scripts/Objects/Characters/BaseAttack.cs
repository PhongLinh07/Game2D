using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class BaseAttack : MonoBehaviour
{
    private float cooldown = 0.3f;
    private float lastAttack;

    public Transform transOfOwner;
    private UnitStats unitStats;
    // Start is called before the first frame update
    void Start()
    {
        unitStats = GetComponent<UnitStats>();
        
    }

    // Update is called once per frame
    void Update()
    {
        // Kiểm tra cooldown
        if (Time.time < lastAttack + cooldown) return;

        // Chỉ xử lý khi người chơi click chuột trái(1 lần), không phải giữ
        if (!Input.GetMouseButton(0)) return;

        // Không bắn khi đang trỏ vào UI
        if (EventSystem.current.IsPointerOverGameObject()) return;
        lastAttack = Time.time;

        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 dir = (mouseWorld - transOfOwner.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        GameObject go = ObjectPoolManager.Instance.Spawn(EObjectPool.FlyingSword);
        go.GetComponent<Bullet>().Init(transOfOwner.position, angle + 90.0f, dir, unitStats.combat.atk);
    }

}
