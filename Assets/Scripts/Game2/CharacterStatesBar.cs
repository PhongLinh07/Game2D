using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatesBar : MonoBehaviour
{
    [SerializeField] CharacterUnit owner;
    public StatusBar hpBar;
    public StatusBar mpBar;

    private void Start()
    {
        hpBar.Init(owner.general.vitality, owner.general.vitality);
        mpBar.Init(owner.general.energy, owner.general.energy);
        owner.mlogic.OnStatsChanged += TakeDamage;
    }

    public void TakeDamage()
    {
        hpBar.SetValue(owner.general.currVitality);
    }
}
