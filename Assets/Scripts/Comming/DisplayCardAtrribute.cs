using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayCardAtrribute : MonoBehaviour
{
    [SerializeField] private CharacterUnit mOwner;

    public TextMeshProUGUI textInformation;
    public UICard cardGeneral;
    public UICard cardCombat;
    public UICard cardMartialArts;
    public UICard cardSpiritualRoot;

    void OnEnable()
    {
        mOwner.mlogic.OnStatsChanged += RefreshUI;
        RefreshUI();
    }

    void OnDisable()
    {
        mOwner.mlogic.OnStatsChanged -= RefreshUI;
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
