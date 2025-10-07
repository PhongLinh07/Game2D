using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCorpse : MonoBehaviour
{
    private float timeDie = 5.0f;

    // Update is called once per frame
    void Update()
    {
        timeDie -= Time.deltaTime;
        if(timeDie <= 0) Destroy(gameObject);
    }
}
