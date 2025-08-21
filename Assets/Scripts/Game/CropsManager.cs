using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Crops
{
    
}
public class CropsManager : MonoBehaviour
{
    public TileBase plowedTile;
    public TileBase seededTile;
    public Tilemap ownerTilemap;

    Dictionary<Vector2Int, Crops> cropsDic;

    private void Start()
    {
        cropsDic = new Dictionary<Vector2Int, Crops>();
    }


    public bool TileIsPlowed(Vector3Int postion)
    {
        return cropsDic.ContainsKey((Vector2Int)postion);
    }

    public void Plow(Vector3Int position)
    {
        if(cropsDic.ContainsKey((Vector2Int)position))
        {
            return;
        }

        CreatePlowedTile(position);
    }

    public void Seed(Vector3Int postion)
    {
        ownerTilemap.SetTile(postion, seededTile);
    }
    private void CreatePlowedTile(Vector3Int position)
    {
        Crops crops = new Crops();
        cropsDic.Add((Vector2Int)position, crops);

        ownerTilemap.SetTile(position, plowedTile);
    }
}
