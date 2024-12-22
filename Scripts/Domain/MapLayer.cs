using Assets.Scripts.Domain.Enums;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Domain
{
    [CreateAssetMenu(fileName = "BuildingLayer", menuName = "Scriptable Objects/BuildingLayer")]
    public class MapLayer : ScriptableObject
    {
        [SerializeField]
        private int _sortingOrder;
        private Tilemap _tilemap;
        public int SortingOrder => _sortingOrder;
        public Tilemap Tilemap { get; set; }
    }
}
