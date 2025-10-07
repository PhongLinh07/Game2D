using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MarkerManager : MonoBehaviour
{
    public Tilemap OwnerTilemap;
    public TileBase tileBase;
    public Vector3Int markedCellPosition;
    
    private Vector3Int oldCellPosition;
    private bool isShow;

    private void Update()
    {
        if(!isShow) return;

        OwnerTilemap.SetTile(oldCellPosition, null);
        OwnerTilemap.SetTile(markedCellPosition, tileBase);
        oldCellPosition = markedCellPosition;
    }

    internal void Show(bool selectable)
    {
        isShow = selectable;
        OwnerTilemap.gameObject.SetActive(isShow);
    }
}
