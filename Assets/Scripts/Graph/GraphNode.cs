using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphNode : MonoBehaviour
{
    // For debug only
    [SerializeField] public List<GraphNode> adjacencyList = new List<GraphNode>();

    public Color _nodeGizmoColor = new Color(Color.white.r, Color.white.g, Color.white.b, 0.5f);

    private Graph graph;
    private Graph Graph
    {
        get
        {
            if(graph == null)
                graph = GetComponent<Graph>();
            return graph;
        }
    }

    private void OnDestroy()
    {
        if (Graph != null)
        {
            Graph.Remove(this);
        }
    }
}
