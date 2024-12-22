using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Domain.Interfaces
{
    public interface ITilemapService
    {
        void SetTile(Vector3Int position, TileBase tile);
        void ClearTile(Vector3Int position);
        void ClearAllTiles();
        bool HasTile(Vector3Int position);
        Vector3Int WorldToCell(Vector3 position);
    }
}
