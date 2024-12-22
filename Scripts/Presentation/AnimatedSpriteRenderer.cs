using UnityEngine;

namespace Assets.Scripts.Presentation
{
    public class AnimatedSpriteRenderer : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        public float frameInterval = 0.25f;
        public Sprite iddleSprite;
        private int currentFrameIndex;
        public Sprite[] sprites;

        public bool IsLooped = true;

        public bool IsIdle = true;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        void Start()
        {
            InvokeRepeating(nameof(NextFrame), frameInterval, frameInterval);
        }

        private void OnEnable()
        {
            _spriteRenderer.enabled = true;
        }

        private void OnDisable()
        {
            _spriteRenderer.enabled = false;
        }

        private void NextFrame()
        {
            currentFrameIndex++;

            if (currentFrameIndex >= sprites.Length)
            {
                if (IsLooped)
                {
                    currentFrameIndex = 0;
                }
                else
                {
                    currentFrameIndex = sprites.Length - 1;
                    OnDisable();
                }
            }

            if (IsIdle)
            {
                _spriteRenderer.sprite = iddleSprite;
            }
            else
            {
                _spriteRenderer.sprite = sprites[currentFrameIndex];
            }
        }
    }
}
