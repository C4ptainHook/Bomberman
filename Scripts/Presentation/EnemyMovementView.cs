using System.Collections.Generic;
using Assets.Scripts.Domain;
using UnityEngine;

namespace Assets.Scripts.Presentation
{
    public class EnemyMovementView : MonoBehaviour
    {
        [SerializeField]
        private Node currentNode;
        private List<Node> path;
        private PathFinding _pathFindingService;
        private GameObject _player;

        private void Awake()
        {
            _pathFindingService = PathFinding.GetInstance();
            _player = GameObject.FindGameObjectWithTag("Player");
        }

        private void FixedUpdate()
        {
            BuildPath();
        }

        private void BuildPath()
        {
            if (path.Count > 0)
            {
                var targetNode = path[0];
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    new Vector3(
                        targetNode.transform.position.x,
                        targetNode.transform.position.y,
                        2
                    ),
                    Time.fixedDeltaTime * 3
                );
                if (Vector2.Distance(transform.position, targetNode.transform.position) < 0.1f)
                {
                    currentNode = targetNode;
                    path.Remove(targetNode);
                }
            }
            else
            {
                while (path == null || path.Count == 0)
                {
                    var targetNode = FindClosestToGameObject(_player);
                    path = _pathFindingService.GeneratePath(currentNode, targetNode);
                }
            }
        }

        private Node FindClosestToGameObject(GameObject gameObject)
        {
            var nodes = FindObjectsOfType<Node>();
            Node closestNode = null;
            foreach (var node in nodes)
            {
                if (
                    closestNode == null
                    || Vector2.Distance(node.transform.position, gameObject.transform.position)
                        < Vector2.Distance(
                            closestNode.transform.position,
                            gameObject.transform.position
                        )
                )
                {
                    closestNode = node;
                }
            }
            return closestNode;
        }
    }
}
