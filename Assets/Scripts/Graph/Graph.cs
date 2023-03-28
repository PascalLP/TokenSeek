using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{

    [SerializeField]
    public List<GraphNode> nodeList = new List<GraphNode>();
    [SerializeField]
    public GameObject node;

    public int Count => nodeList.Count;

    public int gridCols = 1;
    public int gridRows = 1;
    public float gridCellSize = 1;


    public GraphNode FindAdjacentNode(GameObject obj)
    {
        GraphNode closestNode = null;

        float closestDistance = Mathf.Infinity;

        foreach (GraphNode node in nodeList)
        {

            float currentDistance = Vector3.Distance(obj.transform.position, node.transform.position);

            if (currentDistance < closestDistance)
            {

                closestDistance = currentDistance;

                closestNode = node;

            }
        }
        return closestNode;
    }


    // Remove one node from graph
    // To remove a node, remove this node from neighborNode's adjacency List, then remove the node it self;
    public void Remove(GraphNode node)
    {
        if (node == null || !nodeList.Contains(node)) return;

        foreach (GraphNode neighborNode in node.adjacencyList)
        {
            neighborNode.adjacencyList.Remove(node);
        }
        nodeList.Remove(node);
    }

    //Remove nodes ref and destroy child instance;
    public void Clear()
    {
        nodeList.Clear();
        //gameObject.DestroyChildren();
    }

    public List<GraphNode> GetNeighbors(GraphNode node)
    {
        return node.adjacencyList;
    }

    public void GenerateGrid(bool checkCollisions = true)
    {
        Clear();

        // declear a two-dimentional array;
        GraphNode[,] nodeGrid = new GraphNode[gridRows, gridCols];

        // if genGridCols or genGridRows <= 0, their corresponding width or height value will be zero, other wise, £¨genGrid* - 1)  * genGridCellSize
        // ex. say if we have 10 cols, then there are 9 space between them
        float width = (gridCols > 0 ? gridCols - 1 : 0) * gridCellSize;
        float height = (gridRows > 0 ? gridRows - 1 : 0) * gridCellSize;


        Vector3 genPosition = new Vector3(transform.position.x - (width / 2), transform.position.y, transform.position.z - (height / 2));

        //first step: generate nodes
        for (int r = 0; r < gridRows; ++r)
        {
            float startingX = genPosition.x;
            for (int c = 0; c < gridCols; ++c)
            {
                //Check Collisions, if Collides, genPosition change to next position, then skip to next one.
                if (checkCollisions &&
                   Physics.CheckBox(genPosition, Vector3.one / 2, Quaternion.identity, LayerMask.GetMask("Obstacle")))
                {
                    genPosition = new Vector3(genPosition.x + gridCellSize, genPosition.y, genPosition.z);
                    continue;
                }

                //start generate game object
                GameObject obj;
                if (node == null)
                {
                    obj = new GameObject("Node", typeof(GraphNode));
                }
                else { obj = Instantiate(node); }

                obj.name = $"Node ({nodeList.Count})";
                obj.tag = "Node";
                obj.transform.parent = transform;
                obj.transform.position = genPosition;
                //end of generate Object


                GraphNode addedNode = obj.GetComponent<GraphNode>();
                nodeList.Add(addedNode);
                nodeGrid[r, c] = addedNode;

                //next col
                genPosition = new Vector3(genPosition.x + gridCellSize, genPosition.y, genPosition.z);
            }
            //next row
            genPosition = new Vector3(startingX, genPosition.y, genPosition.z + gridCellSize);
        }

        //second step:  create adjacency lists(edges)
        //the relative neighbor position offset;
        int[,] operations = new int[,] {{ -1, 1 }, { 0, 1 }, { 1, 1 },
                                        { -1, 0 },           { 1, 0 },
                                        { -1,-1 }, { 0,-1 }, { 1,-1 }};
        for (int r = 0; r < gridRows; ++r)
        {
            for (int c = 0; c < gridCols; ++c)
            {
                //the nodeGrid is two dimention array that is storing generated node instance;
                if (nodeGrid[r, c] == null) continue;

                //each loop, check 8 neighbor around that node, and see if operation bring us out of bounds, if so, check next one
                //GetLength(0), find 1st dimension length.
                for (int i = 0; i < operations.GetLength(0); ++i)
                {
                    int[] neighborId = new int[2] { r + operations[i, 0], c + operations[i, 1] };

                    // check to see if operation brings us out of bounds,if so continues
                    if (neighborId[0] < 0 || neighborId[0] >= nodeGrid.GetLength(0) || neighborId[1] < 0 || neighborId[1] >= nodeGrid.GetLength(1))
                    {
                        continue;
                    }

                    // a neighbor position that is valid, and it has a created instance
                    GraphNode neighbor = nodeGrid[neighborId[0], neighborId[1]];

                    // use raycast to check collisions, if so continues, else store it.
                    if (neighbor != null)
                    {
                        if (checkCollisions)
                        {
                            Vector3 direction = neighbor.transform.position - nodeGrid[r, c].transform.position;
                            if (Physics.Raycast(nodeGrid[r, c].transform.position, direction, direction.magnitude, LayerMask.GetMask("Obstacle")))
                            {
                                continue;
                            }
                        }
                        nodeGrid[r, c].adjacencyList.Add(neighbor);
                    }
                }
            }
        }
    }
    
}

