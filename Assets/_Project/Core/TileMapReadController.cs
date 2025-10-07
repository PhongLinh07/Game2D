using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapReadController : MonoBehaviour
{
    public Tilemap tileMap; //GlobalTilemap
    public List<TileData> tileDatas; //  Plow/NotPlow
    private Dictionary<TileBase, TileData> dataFormTiles;

    private void Start()
    {
        dataFormTiles = new Dictionary<TileBase, TileData>();

        foreach(TileData tileData in tileDatas )
        {
            foreach(TileBase tileBase in tileData.tiles )
            {
                dataFormTiles.Add(tileBase, tileData);
            }
        }
    }



    public Vector3Int GetGridPosition(Vector2 position, bool mosePosition)
    {
        Vector3 worldPosition;
        if(mosePosition)
        {
            worldPosition = Camera.main.ScreenToWorldPoint(position);
        }
        else
        {
            worldPosition = position;
        }

        Vector3Int gridPosition = tileMap.WorldToCell(worldPosition);

        return gridPosition;
    }


    public TileBase GetTileBase(Vector3Int gridPosition)
    {  
        TileBase tileBase = tileMap.GetTile(gridPosition);
        return tileBase;
    }

    public TileData GetTileData(TileBase tileBase)
    {
        return dataFormTiles[tileBase];
    }
}
