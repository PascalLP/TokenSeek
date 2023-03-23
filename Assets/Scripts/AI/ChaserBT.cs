using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;


public class ChaserBT : BTree
{
    public Transform[] waypoints;

    // Speed of all Chasers
    public static float speed = 2f;

    public static float fovRange = 3f;

    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new FOVDetect(transform),
                new SeekTarget(transform),
            }),
            new Patrolling(transform, waypoints),
        });

        return root;
    }
}
