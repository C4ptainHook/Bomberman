using UnityEngine;

namespace Assets.Scripts.Presentation
{
    public class MovementView : MonoBehaviour
    {
        public Rigidbody2D Rigidbody { get; private set; }
        public float Velocity { get; private set; }
        private Vector2 _direction = Vector2.down;

        public KeyCode up = KeyCode.W;
        public KeyCode down = KeyCode.S;
        public KeyCode left = KeyCode.A;
        public KeyCode right = KeyCode.D;

        public AnimatedSpriteRenderer spriteRendererUp;
        public AnimatedSpriteRenderer spriteRendererDown;
        public AnimatedSpriteRenderer spriteRendererLeft;
        public AnimatedSpriteRenderer spriteRendererRight;
        private AnimatedSpriteRenderer _currentSpriteRenderer;

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            _currentSpriteRenderer = spriteRendererDown;
            Velocity = 5f;
        }

        private void Update()
        {
            UpdateDirection();
        }

        private void FixedUpdate()
        {
            var position = Rigidbody.position;
            var translation = _direction * Velocity * Time.fixedDeltaTime;
            Rigidbody.MovePosition(position + translation);
        }

        private void UpdateDirection()
        {
            if (Input.GetKey(up))
            {
                SetDirection(Vector2.up, spriteRendererUp);
            }
            else if (Input.GetKey(down))
            {
                SetDirection(Vector2.down, spriteRendererDown);
            }
            else if (Input.GetKey(left))
            {
                SetDirection(Vector2.left, spriteRendererLeft);
            }
            else if (Input.GetKey(right))
            {
                SetDirection(Vector2.right, spriteRendererRight);
            }
            else
            {
                SetDirection(Vector2.zero, _currentSpriteRenderer);
            }
        }

        private void SetDirection(Vector2 direction, AnimatedSpriteRenderer spriteRenderer)
        {
            _direction = direction;
            spriteRendererUp.enabled = spriteRenderer == spriteRendererUp;
            spriteRendererDown.enabled = spriteRenderer == spriteRendererDown;
            spriteRendererLeft.enabled = spriteRenderer == spriteRendererLeft;
            spriteRendererRight.enabled = spriteRenderer == spriteRendererRight;

            _currentSpriteRenderer = spriteRenderer;
            _currentSpriteRenderer.IsIdle = direction == Vector2.zero;
        }
    }
}
