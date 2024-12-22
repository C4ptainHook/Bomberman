using Assets.Scripts.Domain;
using Assets.Scripts.Domain.Interfaces;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Application.Services
{
    public class MaterialMapper : IMaterialMapper
    {
        public TileBase GetTileForMaterial(MapMaterial material) => material?.BlockTile;
    }
}
