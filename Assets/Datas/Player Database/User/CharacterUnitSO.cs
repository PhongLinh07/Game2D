using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;


public class CharacterUnitSO 
{
    public Information infomation;
    public General general;
    public Combat combat;

    public MartialArts martialArts;
    public SpiritualRoot spiritualRoot;

    public List<int> SkillsLearned = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7 }; // danh sách skill
    public List<int> SkillsEquipped = new();   // danh sách buff
    public List<EnhanceCfgItem> items = new List<EnhanceCfgItem>
    {
        new EnhanceCfgItem
        {
            id = 0,
            idItem = 0,
            Rarity = (ItemRarity)0,
        },
        new EnhanceCfgItem
        {
            id = 2,
            idItem = 2,
            Rarity = (ItemRarity)2
        },
        new EnhanceCfgItem
        {
            id = 3,
            idItem = 5,
            Rarity = (ItemRarity)4
        }
    };

    public CharacterUnit ToCharacterUnit(CharacterUnit unit)
    {
        unit.infomation = infomation;
        unit.general = general;
        unit.combat = combat;

        unit.martialArts = martialArts;
        unit.spiritualRoot = spiritualRoot;

        unit.SkillsLearned = SkillsLearned;
        unit.SkillsEquipped = SkillsEquipped;
        unit.items = items;

        return unit;
    }
}
