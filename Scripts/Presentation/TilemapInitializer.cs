using System.Collections.Generic;
using Assets.Scripts.Domain;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Presentation
{
    public class TilemapInitializer : Singleton<TilemapInitializer>
    {
        [SerializeField]
        List<MapLayer> mapLayers;

        [SerializeField]
        Transform gridParent;

        private void Start()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            foreach (var mapLayer in mapLayers)
            {
                var tilemapObj = new GameObject(mapLayer.name);
                var map = tilemapObj.AddComponent<Tilemap>();
                var mapRenderer = tilemapObj.AddComponent<TilemapRenderer>();
                tilemapObj.AddComponent<TilemapCollider2D>();
                tilemapObj.transform.SetParent(gridParent);
                mapRenderer.sortingOrder = mapLayer.SortingOrder;
                mapLayer.Tilemap = map;
            }
        }
    }
}
