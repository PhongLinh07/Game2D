using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;




public class CharacterStats : UnitStats
{
    public MartialArts martialArts;
    public SpiritualRoot spiritualRoot;

    public TextMeshProUGUI textInformation;
    public UICard cardGeneral;
    public UICard cardCombat;
    public UICard cardMartialArts;
    public UICard cardSpiritualRoot;
   

    public override void TakeDamage(int amount)
    {
        general.currVitality -= amount;
        cardGeneral.SetContent
        (
            $"Lifespan: {general.currLifespan}/{general.lifespan}\n" +
            $"Vitality: {general.currVitality}/{general.vitality}\n" +
            $"Energy: {general.currEnergy}/{general.energy}"
        );

    }
    private void Init()
    {
        // 1. Thông tin cơ bản
        textInformation.text =
            $"Surname: {infomation.surName}\n" +
            $"Name: {infomation.name}\n" +
            $"Sect: {infomation.sect}\n" +
            $"Race: {infomation.race}\n" +
            $"Sex: {infomation.sex}";

        TakeDamage(0);

        //3. Chỉ số chiến đấu
        cardCombat.SetContent
        (
            $"Attack: {combat.atk}\n" +
            $"Defense: {combat.def}\n" +
            $"Agility: {combat.agility}"
        );

        // 4. Võ học
        cardMartialArts.SetContent
        (
            $"Sword: {martialArts.sword}\n" +
            $"Spear: {martialArts.spear}"
        );

        // 5. Linh căn
        cardSpiritualRoot.SetContent
        (
            $"Wind: {spiritualRoot.wind}\n" +
            $"Fire: {spiritualRoot.fire}\n" +
            $"Water: {spiritualRoot.water}\n" +
            $"Lightning: {spiritualRoot.lightning}\n" +
            $"Earth: {spiritualRoot.earth}\n" +
            $"Wood: {spiritualRoot.wood}"
        );

       
    }

    private void Start()
    {
        Init(); // Gọi khởi tạo khi game bắt đầu
        
    }

}
