using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Presentation
{
    public class BombView : MonoBehaviour
    {
        [Header("Bomb")]
        public GameObject bombPrefab;
        public KeyCode dropBomb = KeyCode.Space;
        public int bombLimit = 1;
        private int bombInReserve;
        public float fuseTime = 3f;

        [Header("Explosion")]
        public float explosionDuration = 1f;
        public int explosionDistance = 1;
        public ExplosionView explosionPrefab;
        public LayerMask mapLayer;

        [Header("Destructible")]
        public DestructibleView destructiblePrefab;
        public Tilemap destructibleLayer;

        private void Awake()
        {
            bombInReserve = bombLimit;
        }

        private void Update()
        {
            if (bombInReserve > 0 && Input.GetKeyDown(dropBomb))
            {
                StartCoroutine(DropBomb());
            }
        }

        public IEnumerator DropBomb()
        {
            var position = transform.position;
            position.x = Mathf.Round(position.x);
            position.y = Mathf.Round(position.y);

            var bomb = Instantiate(bombPrefab, position, Quaternion.identity);
            bombInReserve--;
            yield return new WaitForSeconds(fuseTime);

            var explosionPosition = bomb.transform.position;
            explosionPosition.x = Mathf.Round(explosionPosition.x);
            explosionPosition.y = Mathf.Round(explosionPosition.y);
            var explosion = Instantiate(explosionPrefab, explosionPosition, Quaternion.identity);
            explosion.SetActiveRenderer(explosion._start);
            ExplodeInDirection(Vector2.up, explosionPosition, explosionDistance);
            ExplodeInDirection(Vector2.right, explosionPosition, explosionDistance);
            ExplodeInDirection(Vector2.down, explosionPosition, explosionDistance);
            ExplodeInDirection(Vector2.left, explosionPosition, explosionDistance);
            Destroy(explosion.gameObject, explosionDuration);
            Destroy(bomb);
            bombInReserve++;
        }

        private void ExplodeInDirection(Vector2 direction, Vector2 position, int distance)
        {
            if (distance <= 0)
            {
                return;
            }

            position += direction;

            if (Physics2D.OverlapBox(position, Vector2.one / 2, 0, mapLayer))
            {
                var tilePosition = destructibleLayer.WorldToCell(position);
                TileBase tile = destructibleLayer.GetTile(tilePosition);
                if (tile != null)
                {
                    var destructible = Instantiate(
                        destructiblePrefab,
                        position,
                        Quaternion.identity
                    );
                    destructibleLayer.SetTile(tilePosition, null);
                    Destroy(destructible.gameObject, destructiblePrefab.duration);
                }
                return;
            }

            var explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
            explosion.SetActiveRenderer(distance > 1 ? explosion._middle : explosion._end);
            explosion.SetDirection(direction);
            Destroy(explosion.gameObject, explosionDuration);
            ExplodeInDirection(direction, position, distance - 1);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Bomb"))
            {
                if (other != null)
                {
                    other.isTrigger = false;
                }
            }
        }
    }
}
