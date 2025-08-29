using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatesBar : MonoBehaviour
{
    [SerializeField] private CharacterUnit mOwner;
    public StatusBar hpBar;
    public StatusBar mpBar;

    private void Start()
    {
        if (mOwner.data == null) return;

        hpBar.Init(mOwner.data.general.vitality, mOwner.data.general.vitality);
        mpBar.Init(mOwner.data.general.energy, mOwner.data.general.energy);
        mOwner.mlogic.OnStatsChanged += TakeDamage;
    }

    public void TakeDamage()
    {
        hpBar.SetValue(mOwner.data.general.currVitality);
    }
}
