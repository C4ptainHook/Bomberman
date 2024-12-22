using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Domain;
using UnityEngine;

namespace Assets.Scripts.Domain
{
    public class PathFinding : Singleton<PathFinding>
    {
        public List<Node> GeneratePath(Node start, Node end)
        {
            var openSet = new HashSet<Node>();
            foreach (var node in FindObjectsOfType<Node>())
            {
                node.gCost = float.MaxValue;
            }

            start.gCost = 0;
            start.hCost = Vector2.Distance(start.transform.position, end.transform.position);
            openSet.Add(start);

            while (openSet.Count > 0)
            {
                var lowestFCostNode = openSet.First();
                foreach (var node in openSet)
                {
                    if (lowestFCostNode == null || node.FCost < lowestFCostNode.FCost)
                    {
                        lowestFCostNode = node;
                    }
                }
                var currentNode = lowestFCostNode;
                openSet.Remove(currentNode);
                if (currentNode == end)
                {
                    var path = new List<Node>();
                    path.Insert(0, end);
                    while (currentNode != start)
                    {
                        currentNode = currentNode.previousNode;
                        path.Add(currentNode);
                    }
                    path.Reverse();
                    return path;
                }

                foreach (var neighbor in currentNode.neighbors)
                {
                    float tentativeGCost =
                        currentNode.gCost
                        + Vector2.Distance(
                            currentNode.transform.position,
                            neighbor.transform.position
                        );
                    if (tentativeGCost < neighbor.gCost)
                    {
                        neighbor.previousNode = currentNode;
                        neighbor.gCost = tentativeGCost;
                        neighbor.hCost = Vector2.Distance(
                            neighbor.transform.position,
                            end.transform.position
                        );
                        if (!openSet.Contains(neighbor))
                        {
                            openSet.Add(neighbor);
                        }
                    }
                }
            }
            return null;
        }
    }
}
