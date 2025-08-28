using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class SpreadShot : ASkillLogic
{
    private SkillCfgItem data;
    private Vector2 posCast;
    private Vector2 dirCast;
    
    // Start is called before the first frame updateS
    public void Init(SkillCfgItem skill)
    {
        data = skill;
    }

    public override IEnumerator Cast(params object[] args)
    {
        posCast = (Vector2)args[0]; // ép kiểu thủ công
        dirCast = (Vector2)args[1];

        ShootFan(dirCast, 5, 45);
        yield return null;
    }

    public void ShootFan(Vector3 direction, int bulletCount, float maxAngle)
    {
        // Chuẩn hóa hướng bắn
        direction.Normalize();

        // Góc quay trung tâm (hướng chính)
        float baseAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Góc lệch giữa mỗi viên
        float angleStep = (bulletCount > 1) ? maxAngle / (bulletCount - 1) : 0;

        // Góc bắt đầu (để tỏa đều 2 bên)
        float startAngle = baseAngle - maxAngle / 2;

        for (int i = 0; i < bulletCount; i++)
        {
            float currentAngle = startAngle + angleStep * i;
            Vector2 shootDir = new Vector2(Mathf.Cos(currentAngle * Mathf.Deg2Rad), Mathf.Sin(currentAngle * Mathf.Deg2Rad));

            // Tạo đạn
            GameObject go = ObjectPoolManager.Instance.Spawn(EObjectPool.FlyingSword);
            go.GetComponent<Bullet>().Init(posCast, currentAngle + 90.0f, shootDir, data.atk);
        }
    }

}
