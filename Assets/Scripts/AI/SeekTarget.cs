using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;

public class SeekTarget : Node
{
    private Transform _transform;

    public SeekTarget(Transform transform)
    {
        _transform = transform;
    }

    public override NodeState Evaluate()
    {
        Debug.Log("SeekTarget Running");
        Transform target = (Transform)GetData("target");
        //Debug.Log("Chaser Position: " + _transform.position + "  Seeker Position: " + target.position);
        if(Vector3.Distance(_transform.position, target.position) > 0.01f)
        {
            if (Vector3.Distance(_transform.position, target.position) > ChaserBT.fovRange)
            {
                Debug.Log("Stopped Chasing");
                state = NodeState.FAILURE;
                return state;
            }
            Debug.Log("Seeking Target");
            _transform.position = Vector3.MoveTowards(_transform.position, target.position, ChaserBT.speed * Time.deltaTime);
            _transform.LookAt(target.position);
            state = NodeState.RUNNING;
            return state;
        }
        Debug.Log("Seek stuck running");
        state = NodeState.FAILURE;
        return state;
    }
}
