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
        textInformation.text = mOwner.data.infomation.ToString();
        cardGeneral.SetContent(mOwner.data.general.ToString());
        cardCombat.SetContent(mOwner.data.combat.ToString());
        cardMartialArts.SetContent(mOwner.data.martialArts.ToString());
        cardSpiritualRoot.SetContent(mOwner.data.spiritualRoot.ToString());
    }
}
