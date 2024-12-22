using Assets.Scripts.Application.Services;
using Assets.Scripts.Domain;
using Assets.Scripts.Domain.Interfaces;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Presentation
{
    public class EditorView : Singleton<EditorView>
    {
        [SerializeField]
        private Tilemap previewTilemap;

        [SerializeField]
        private Tilemap affectedTilemap;

        [SerializeField]
        private Tilemap forbiddenTilemap;
        private EditorService _editorService;
        private IInputService _inputService;

        protected override void Awake()
        {
            base.Awake();

            _inputService = new EditorInputService(Camera.main);
            _editorService = new EditorService(
                new TilemapService(previewTilemap),
                new TilemapService(affectedTilemap),
                new TilemapService(forbiddenTilemap),
                _inputService,
                new MaterialMapper()
            );
        }

        private void OnEnable()
        {
            _inputService.Initialize();
            _inputService.Enable();
        }

        private void OnDisable()
        {
            _inputService.Disable();
        }

        public void SelectMaterial(MapMaterial material)
        {
            _editorService.SelectMaterial(material);
        }
    }
}
