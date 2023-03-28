using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;

// Searches for the player or seeker using ray casting
public class RayCastDetect : Node
{
    public float detectionRadius = 100f;
    public LayerMask obstacleLayer;

    private Transform _transform;
    private Animator _animator;

    private GameObject player;
    private GameObject seeker;

    public RayCastDetect(Transform transform, LayerMask layer)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        seeker = GameObject.FindGameObjectWithTag("Seeker");
        _transform = transform;
        _animator = transform.GetComponent<Animator>();
        obstacleLayer = layer;
    }

    public override NodeState Evaluate()
    {
        object t = GetData("target");

        if (player == null)
        {
            state = NodeState.FAILURE;
            return state;
        }

        if(t == null)
        {
            bool canSeePlayer = false;
            bool canSeeSeeker = false;

            // Check if player is within detection radius
            if (Vector3.Distance(_transform.position, player.transform.position) <= detectionRadius)
            {
                // Check if there is an obstruction between enemy and player
                RaycastHit hit;
                if (Physics.Raycast(_transform.position, player.transform.position - _transform.position, out hit, detectionRadius, obstacleLayer))
                {
                    Debug.Log("Raycast hit: " + hit.collider.tag);
                    if (hit.collider.CompareTag("Player"))
                    {
                        canSeePlayer = true;
                    }else if (hit.collider.CompareTag("Seeker"))
                    {
                        canSeeSeeker = true;
                    }
                }
            }

            if (canSeePlayer)
            {
                _parent._parent.SetData("target", player.transform);
                state = NodeState.SUCCESS;
                Debug.Log("Found the Player!");
                _animator.SetBool("Walking", true);
                return state;
            }
            else if(canSeeSeeker)
            {
                _parent._parent.SetData("target", seeker.transform);
                state = NodeState.SUCCESS;
                Debug.Log("Found the Seeker!");
                _animator.SetBool("Walking", true);
                return state;
            }

            _parent._parent.ClearData("target");
            Debug.Log("Did NOT find a Seeker! But I still looked");
            state = NodeState.FAILURE;
            return state;
        }

        Debug.Log("There was already a seeker detected");
        state = NodeState.SUCCESS;
        return state;


    }
}
