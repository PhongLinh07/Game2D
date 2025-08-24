using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnManager : MonoBehaviour
{
    public static ItemSpawnManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public GameObject ItemPrefab;
    public void SpawnItem(Vector3 position, EnhanceCfgItem playerItem)
    {
        GameObject go = Instantiate(ItemPrefab, position, Quaternion.identity);
        go.GetComponent<PickUpItem>().SetItemPickUp(playerItem);
    }
}
