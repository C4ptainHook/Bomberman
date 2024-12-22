using Assets.Scripts.Domain.Enums;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Domain
{
    [CreateAssetMenu(fileName = "BuildingBlock", menuName = "Scriptable Objects/BuildingBlock")]
    public class MapMaterial : ScriptableObject
    {
        [SerializeField]
        private MapLayer blockLayer;

        [SerializeField]
        private TileBase blockTile;

        public TileBase BlockTile => blockTile;
        public MapLayer BlockLayer => blockLayer;
    }
}
