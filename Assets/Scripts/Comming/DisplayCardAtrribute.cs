using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayCardAtrribute : MonoBehaviour
{
    [SerializeField] private CharacterCfgItem mOwner;

    public TextMeshProUGUI textInformation;
    public UICard cardGeneral;
    public UICard cardCombat;
    public UICard cardMartialArts;
    public UICard cardSpiritualRoot;

    void OnEnable()
    {
        mOwner = LogicCharacter.Instance.Data;
        LogicCharacter.Instance.OnStatsChanged += RefreshUI;
        RefreshUI();
    }

    void OnDisable()
    {
        LogicCharacter.Instance.OnStatsChanged -= RefreshUI;
    }

    void RefreshUI()
    {
        textInformation.text = mOwner.infomation.ToString();
        cardGeneral.SetContent(mOwner.general.ToString());
        cardCombat.SetContent(mOwner.combat.ToString());
        cardMartialArts.SetContent(mOwner.martialArts.ToString());
        cardSpiritualRoot.SetContent(mOwner.spiritualRoot.ToString());
    }
}
