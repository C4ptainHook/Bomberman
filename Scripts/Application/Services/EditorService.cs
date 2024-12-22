using Assets.Scripts.Domain;
using Assets.Scripts.Domain.Enums;
using Assets.Scripts.Domain.Interfaces;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Application.Services
{
    public class EditorService
    {
        private readonly ITilemapService _previewTilemap;
        private readonly ITilemapService _defaultTilemap;
        private readonly ITilemapService _forbiddenTilemap;
        private readonly IInputService _inputService;
        private readonly IMaterialMapper _materialMapper;

        private Vector3Int _currentTilePosition;
        private Vector3Int _previousTilePosition;
        private MapMaterial _currentMaterial;
        private TileBase _currentTile;
        private bool _isHoldActive;
        private Vector3Int _holdStartPosition;
        private BoundsInt _fillBounds;

        private ITilemapService GetTilemap()
        {
            if (
                _currentMaterial != null
                && _currentMaterial.BlockLayer != null
                && _currentMaterial.BlockLayer.Tilemap != null
            )
            {
                return new TilemapService(_currentMaterial.BlockLayer.Tilemap);
            }
            return _defaultTilemap;
        }

        public EditorService(
            ITilemapService previewTilemap,
            ITilemapService defaultTilemap,
            ITilemapService forbiddenTilemap,
            IInputService inputService,
            IMaterialMapper materialMapper
        )
        {
            _previewTilemap = previewTilemap;
            _defaultTilemap = defaultTilemap;
            _forbiddenTilemap = forbiddenTilemap;
            _inputService = inputService;
            _materialMapper = materialMapper;

            _inputService.OnSelect += HandleSelect;
            _inputService.OnDeselect += HandleDeselect;
            _inputService.OnMouseMove += HandleMouseMove;
        }

        private bool IsForbidden(Vector3Int position) => _forbiddenTilemap.HasTile(position);

        public void UpdatePreview(Vector3 worldPosition)
        {
            RemoveLastTile();
            _previousTilePosition = _currentTilePosition;
            _currentTilePosition = _previewTilemap.WorldToCell(worldPosition);
            if (IsForbidden(_previewTilemap.WorldToCell(worldPosition)))
                return;

            if (_isHoldActive)
            {
                RenderFill();
            }
            else
            {
                RenderSingle();
            }
        }

        public void SelectMaterial(MapMaterial material)
        {
            _currentMaterial = material;
            _currentTile = _materialMapper.GetTileForMaterial(material);
        }

        private void HandleSelect(InputAction.CallbackContext ctx)
        {
            if (IsForbidden(_currentTilePosition))
                return;
            if (_currentMaterial == null)
                return;

            if (ctx.phase == InputActionPhase.Started)
            {
                if (ctx.interaction is SlowTapInteraction)
                {
                    _isHoldActive = true;
                }

                _holdStartPosition = _currentTilePosition;
                RenderFill();
            }
            else
            {
                if (_isHoldActive)
                {
                    _isHoldActive = false;
                    DrawFill(GetTilemap());
                }
                else
                {
                    DrawSingle();
                }
            }
        }

        private void DrawSingle()
        {
            GetTilemap().SetTile(_currentTilePosition, _currentTile);
        }

        private void DrawFill(ITilemapService tilemapService)
        {
            for (var x = _fillBounds.xMin; x <= _fillBounds.xMax; x++)
            {
                for (var y = _fillBounds.yMin; y <= _fillBounds.yMax; y++)
                {
                    tilemapService.SetTile(new Vector3Int(x, y, 0), _currentTile);
                }
            }
        }

        private void RenderSingle()
        {
            RemoveLastTile();
            _previewTilemap.SetTile(_currentTilePosition, _currentTile);
        }

        private void RemoveLastTile()
        {
            _previewTilemap.ClearTile(_previousTilePosition);
        }

        private void RenderFill()
        {
            _previewTilemap.ClearAllTiles();
            _fillBounds.xMin =
                _currentTilePosition.x < _holdStartPosition.x
                    ? _currentTilePosition.x
                    : _holdStartPosition.x;
            _fillBounds.xMax =
                _currentTilePosition.x > _holdStartPosition.x
                    ? _currentTilePosition.x
                    : _holdStartPosition.x;
            _fillBounds.yMin =
                _currentTilePosition.y < _holdStartPosition.y
                    ? _currentTilePosition.y
                    : _holdStartPosition.y;
            _fillBounds.yMax =
                _currentTilePosition.y > _holdStartPosition.y
                    ? _currentTilePosition.y
                    : _holdStartPosition.y;
            DrawFill(_previewTilemap);
        }

        private void HandleDeselect() => SelectMaterial(null);

        private void HandleMouseMove(Vector2 position) =>
            UpdatePreview(_inputService.GetMousePosition());
    }
}
