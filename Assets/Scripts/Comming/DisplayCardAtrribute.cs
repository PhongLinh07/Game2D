using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Playables;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.InputManagerEntry;

public class DisplayCardAtrribute : MonoBehaviour
{
    [SerializeField] private LogicCharacter mOwner;

    public TextMeshProUGUI textInformation;
    public UICard cardGeneral;
    public UICard cardCombat;
    public UICard cardMartialArts;
    public UICard cardSpiritualRoot;

    void OnEnable()
    {
        mOwner = LogicCharacter.Instance;
        LogicCharacter.Instance.OnStatsChanged += RefreshUI;
        RefreshUI();
    }

    void OnDisable()
    {
        LogicCharacter.Instance.OnStatsChanged -= RefreshUI;
    }

    void RefreshUI()
    {
        // textInformation.text = mOwner.infomation.ToString();
        cardGeneral.SetContent
           (
               $"Lifespan: {mOwner.Data.attributes[EAttribute.Lifespan].currValue}/{mOwner.Data.attributes[EAttribute.Lifespan].value}\n" +
               $"Vitality: {mOwner.Data.attributes[EAttribute.Hp].currValue}/{mOwner.Data.attributes[EAttribute.Hp].value}\n" +
               $"Energy  : {mOwner.Data.attributes[EAttribute.Mana].currValue}/{mOwner.Data.attributes[EAttribute.Mana].value}\n"
            );

        cardCombat.SetContent
            (
                $"Attack : {mOwner.Data.attributes[EAttribute.Attack].value}\n" +
                $"Defense: {mOwner.Data.attributes[EAttribute.Defense].value}\n" +
                $"Agility: {mOwner.Data.attributes[EAttribute.Speed].value}\n"
            );
        //cardMartialArts.SetContent();
        cardSpiritualRoot.SetContent
            (
                $"Wind     : {mOwner.Data.attributes[EAttribute.WindCore].value}\n" +
                $"Fire     : {mOwner.Data.attributes[EAttribute.FireCore].value}\n" +
                $"Water    : {mOwner.Data.attributes[EAttribute.WaterCore].value}\n" +
                $"Lightning: {mOwner.Data.attributes[EAttribute.LightningCore].value}\n" +
                $"Earth    : {mOwner.Data.attributes[EAttribute.EarthCore].value}\n" +
                $"Wood     : {mOwner.Data.attributes[EAttribute.WindCore].value}\n" +
                $"Metal    : {mOwner.Data.attributes[EAttribute.MetalCore].value}\n"
            );
    }
}
