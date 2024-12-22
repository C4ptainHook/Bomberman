using Assets.Scripts.Domain;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Presentation
{
    public class MaterialInteractionHandler : MonoBehaviour
    {
        [SerializeField]
        private MapMaterial material;
        private Button button;
        private EditorView editorView;

        private void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(OnButtonClicked);
            editorView = EditorView.GetInstance();
        }

        private void OnButtonClicked()
        {
            editorView.SelectMaterial(material);
        }
    }
}
