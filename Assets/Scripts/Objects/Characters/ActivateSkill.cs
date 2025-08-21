using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Activateitem : MonoBehaviour
{

    private float cooldown = 0.1f;
    private float lastAttack;

    public Transform transOfOwner;
    private UnitStats unitStats;

    private Vector2Int rangedMatrix = new Vector2Int(5, 4);
    // Start is called before the first frame update
    void Start()
    {
        unitStats = GetComponent<UnitStats>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) itemFallingSword();
    }

    void itemFallingSword()
    {
        if (Time.time < lastAttack + cooldown) return;
        
        lastAttack = Time.time;

         StartCoroutine(SpawnFallingSwords());

    }

    private IEnumerator SpawnFallingSwords()
    {
        for (int i = 0; i < 20; i++)
        {
            SpawnOneSword();
            yield return new WaitForSeconds(0.1f); // Delay 0.1s mỗi lần rơi
        }
    }

    void SpawnOneSword()
    {
        int x = UnityEngine.Random.Range((int)(transOfOwner.position.x - 3.0f), (int)(transOfOwner.position.x + 3.0f));
        int y = UnityEngine.Random.Range((int)(transOfOwner.position.y - 2.0f), (int)(transOfOwner.position.y + 2.0f));

        GameObject go = ObjectPoolManager.Instance.Spawn(EObjectPool.FallingSword);
        go.GetComponent<FallingSword>().Init(new Vector2(x, y), unitStats.combat.atk);
    }

}
