using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using BehaviourTree;


public class ChaserBT : BTree
{
    public Transform[] waypoints;

    //Animator
    public static Animator animator;

    // Speed of all Chasers
    [SerializeField] public static float speed = 1.5f;

    [SerializeField]
    public static float fovRange = 8f;

    // Layer to ignore walls
    public LayerMask obstacleLayer;

    // Last detected position of the target
    public static Transform lastSeenPosition;

    private void FixedUpdate()
    {
        if (_root != null)
            _root.Evaluate();
    }

    protected override Node SetupTree()
    {
        animator = GetComponent<Animator>();

        Debug.Log("Starting Selector");
        Node root = new Selector(new List<Node>
        {

            new Sequence(new List<Node>
            {
                new FOVDetect(transform)
                /*new RayCastDetect(transform, obstacleLayer)*/,
                new TargetInRange(transform),
                new SeekTarget(transform),
            }),
            new Sequence(new List<Node> {
                /*new GoToLastPosition(transform, 3f),*/
                new Patrolling(transform, waypoints),
            })
        });
        Debug.Log("Selector Ended");
        return root;
    }
}

// Alternate version
/*public class ChaserBT : MonoBehaviour
{

    public Transform[] waypoints;

    // Speed of all Chasers
    public static float speed = 2f;

    [SerializeField]
    public static float fovRange = 3f;

    private void FixedUpdate()
    {
        if (_root != null)
            _root.Evaluate();
    }

    protected override Node SetupTree()
    {
        Debug.Log("Starting Selector");
        Node root = new Selector(new List<Node>
        {

            new Sequence(new List<Node>
            {
                new FOVDetect(transform),
                new SeekTarget(transform),
            }),
            new Patrolling(transform, waypoints),
        });
        Debug.Log("Selector Ended");
        return root;
    }*/