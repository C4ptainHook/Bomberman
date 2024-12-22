using UnityEngine;

namespace Assets.Scripts.Presentation
{
    public class DestructibleView : MonoBehaviour
    {
        public float duration = 1f;

        private void Start()
        {
            Destroy(gameObject, duration);
        }
    }
}
