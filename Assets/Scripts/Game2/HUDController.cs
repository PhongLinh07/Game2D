using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public static HUDController Instance { get; private set; }

    [SerializeField]
    private LogicCharacter _logicCharacter;

    public StatusBar hpBar;
    public StatusBar mpBar;
    public Button avatar;

    private void Awake()
    {
        Instance = this;
        Bootstrapper.Instance.eventWhenCloneCharacter += Init;
    }

    public void Init(LogicCharacter logicCharacter)
    {
        Bootstrapper.Instance.eventWhenCloneCharacter -= Init;

        _logicCharacter = LogicCharacter.Instance;

        if (logicCharacter == null) Debug.Log("Null");
        hpBar.Init(_logicCharacter.Data.general.vitality, _logicCharacter.Data.general.currVitality);
        mpBar.Init(_logicCharacter.Data.general.energy, _logicCharacter.Data.general.currEnergy);
        logicCharacter.OnStatsChanged += TakeDamage;
        logicCharacter.OnStatsChanged += UseSKill;
    }
    private void TakeDamage()
    {
        hpBar.SetValue(_logicCharacter.Data.general.currVitality);
    }

    private void UseSKill()
    {
        mpBar.SetValue(_logicCharacter.Data.general.currEnergy);
    }
}
