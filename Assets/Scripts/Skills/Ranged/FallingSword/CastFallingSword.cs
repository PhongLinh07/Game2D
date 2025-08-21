using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class CastFallingSword : ASkillLogic
{
    private SkillCfgSkill data;
    private Vector2 posCast;
    
    // Start is called before the first frame updateS
    public void Init(SkillCfgSkill skill)
    {
        data = skill;
    }

    public override IEnumerator Cast(params object[] args)
    {
        posCast = (Vector2)args[0]; // ép kiểu thủ công

        for (int i = 0; i < 20; i++)
        {
            SpawnOneSword();
            yield return new WaitForSeconds(0.1f);
        }
    }


    void SpawnOneSword()
    {
        int x = UnityEngine.Random.Range((int)(posCast.x - 3.0f), (int)(posCast.x + 3.0f));
        int y = UnityEngine.Random.Range((int)(posCast.y - 2.0f), (int)(posCast.y + 2.0f));

        GameObject go = ObjectPoolManager.Instance.Spawn(EObjectPool.FallingSword);
        go.GetComponent<FallingSword>().Init(new Vector2(x, y), data.atk);
    }
}
