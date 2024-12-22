using Assets.Scripts.Domain;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Domain.Interfaces
{
    public interface IMaterialMapper
    {
        TileBase GetTileForMaterial(MapMaterial material);
    }
}
