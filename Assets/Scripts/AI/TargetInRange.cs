using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;

public class TargetInRange : Node
{
    private Transform _transform;

    public TargetInRange(Transform transform)
    {
        _transform = transform;
    }

    public override NodeState Evaluate()
    {
        Debug.Log("TargetInRange Running");
        Transform target = (Transform)GetData("target");

        if (Vector3.Distance(_transform.position, target.position) > ChaserBT.fovRange)
        {
            ChaserBT.lastSeenPosition = target;
            state = NodeState.FAILURE;
        }

        state = NodeState.SUCCESS;
        return state;
    }
}
