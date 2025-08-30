using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class MonsterUnit : UnitStats
{
    public Information infomation;
    public General general;
    public Combat combat;

    public override void SetPosition(Vector2 position) { }
    public override int TakeDamage(int damage)
    {
        return general.currVitality = Mathf.Clamp(general.currVitality - damage, 0, general.vitality);
    }

    public override int Heal(int amount)
    {
        return general.currVitality = Mathf.Clamp(general.currVitality + amount, 0, general.vitality);
    }
}
