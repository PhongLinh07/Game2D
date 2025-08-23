using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class BaseAttack : ASkillLogic
{
    private SkillCfgSkill data;
    private Vector2 posCast;
    private Vector2 dirCast;

    public void Init(SkillCfgSkill skill)
    {
        data = skill;
    }

    public override IEnumerator Cast(params object[] args)
    {
        posCast = (Vector2)args[0]; // ép kiểu thủ công
        dirCast = (Vector2)args[1];

        Attack();
        yield return null;
    }


    // Update is called once per frame
    void Attack()
    {
        
        float angle = Mathf.Atan2(dirCast.y, dirCast.x) * Mathf.Rad2Deg;

        GameObject go = ObjectPoolManager.Instance.Spawn(EObjectPool.FlyingSword);
        go.GetComponent<Bullet>().Init(posCast, angle + 90.0f, dirCast, data.atk, 30);
    }

}
