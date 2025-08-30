using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyAttackType { Melee, Ranged }

public interface IEnemyCombat
{
    EnemyAttackType GetAttackType();
    void ResetCombat();
}

