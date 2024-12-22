using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Domain
{
    public class Node : MonoBehaviour
    {
        public Node previousNode;
        public List<Node> neighbors;
        public float gCost;
        public float hCost;
        public float FCost => gCost + hCost;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            if (neighbors != null)
            {
                foreach (var neighbor in neighbors)
                {
                    Gizmos.DrawLine(transform.position, neighbor.transform.position);
                }
            }
        }
    }
}
