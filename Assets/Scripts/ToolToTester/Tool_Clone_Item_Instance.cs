using UnityEngine;


public class Tool_Clone_Item_Instance : SingletonBase<Tool_Clone_Item_Instance>
{

    // Update is called once per frame
    public ItemUserCfgItem Clone()
    {
        ItemRarity rarity = ItemRarity.Mortal;
        int level = 0;
        
        int id_Template = Random.Range(0, UUIDConfig.GetInstance.GetUUID(EUUIDType.ItemTemplate));
        int quantity = Random.Range(1, 20);

        if (70 > Random.Range(0, 200))
        {
            rarity = ItemRarity.Celestial;
        }
        else if (50 > Random.Range(0, 200))
        {
            rarity = ItemRarity.Genuine;
        }
        else if (30 > Random.Range(0, 200))
        {
            rarity = ItemRarity.Superior;
        }
        else if (10 > Random.Range(0, 200))
        {
            rarity = ItemRarity.Divine;
        }
        
        level = Random.Range(0, 10);
        

        ItemUserCfgItem instance = new ItemUserCfgItem();
        instance.id_Item = id_Template;
        instance.Level = level;
        instance.Quantity = quantity;
        instance.Rarity = rarity;
        
        return instance;
    }
}
