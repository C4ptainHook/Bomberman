using Assets.Scripts.Domain.Interfaces;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Application.Services
{
    public class TilemapService : ITilemapService
    {
        private readonly Tilemap _tilemap;

        public TilemapService(Tilemap tilemap)
        {
            _tilemap = tilemap;
        }

        public void SetTile(Vector3Int position, TileBase tile) => _tilemap.SetTile(position, tile);

        public void ClearTile(Vector3Int position) => _tilemap.SetTile(position, null);

        public void ClearAllTiles() => _tilemap.ClearAllTiles();

        public bool HasTile(Vector3Int position) => _tilemap.HasTile(position);

        public Vector3Int WorldToCell(Vector3 position) => _tilemap.WorldToCell(position);
    }
}
