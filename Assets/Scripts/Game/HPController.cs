using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPController : MonoBehaviour
{
    private UnitStats unitStats;

    public bool isAlive = true;
    public StatusBar hpBar;

    public StatusBar hpBarTry;


    private void Start()
    {
        unitStats = GetComponent<UnitStats>();
        
        hpBar.maxValue = unitStats.general.vitality;
        hpBar.currValue = unitStats.general.currVitality;

        if (!hpBarTry) return;
        hpBarTry.maxValue = unitStats.general.vitality;
        hpBarTry.currValue = unitStats.general.currVitality;


    }
    public void TakeDamage(int amount)
    {
        unitStats.TakeDamage(amount);
        hpBarTry?.Add(-amount);
       // unitStats.UpdateStats();
        if (!hpBar.Add(-amount))
        {
            
            isAlive = false;
            //Destroy(gameObject);
        }
    }
}
