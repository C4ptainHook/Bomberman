using UnityEngine;

namespace Assets.Scripts.Presentation
{
    public class ExplosionView : MonoBehaviour
    {
        [SerializeField]
        public AnimatedSpriteRenderer _start;

        [SerializeField]
        public AnimatedSpriteRenderer _middle;

        [SerializeField]
        public AnimatedSpriteRenderer _end;

        public void SetActiveRenderer(AnimatedSpriteRenderer spriteRenderer)
        {
            _start.enabled = spriteRenderer == _start;
            _middle.enabled = spriteRenderer == _middle;
            _end.enabled = spriteRenderer == _end;
        }

        public void SetDirection(Vector2 direction)
        {
            var angle = Mathf.Atan2(direction.y, direction.x);
            transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
        }
    }
}
