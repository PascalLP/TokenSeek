using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;

public class FOVDetect : Node
{
    private static int _enemyLayerMask = 6;
    private Transform _transform;
    private Animator _animator;

    public FOVDetect(Transform transform)
    {
        _transform = transform;
        _animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        object t = GetData("target");
        if (t == null)
        {
            Collider[] colliders = Physics.OverlapSphere(_transform.position, ChaserBT.fovRange, _enemyLayerMask);
            
            if(colliders.Length > 0)
            {
                _parent._parent.SetData("target", colliders[0].transform);
                _animator.SetBool("Walking", true);
                state = NodeState.SUCCESS;
                return state;
            }

            //_parent._parent.ClearData("target");
            state = NodeState.FAILURE;
            return state;
        }

        state = NodeState.SUCCESS;
        return state;
    }
}
