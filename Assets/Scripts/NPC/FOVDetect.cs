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

    void OnDrawGizmos()
    {
        Gizmos.DrawLine(_transform.position, _transform.position + new Vector3(ChaserBT.fovRange,0,0));
    }

    public override NodeState Evaluate()
    {
        Debug.Log("FOVDetect Running");
        object t = GetData("target");
        if (t == null)
        {
            Collider[] colliders = Physics.OverlapSphere(_transform.position, ChaserBT.fovRange);
            Debug.Log(colliders[0].tag.ToString());
            if (colliders.Length > 0)
            {
                Debug.Log("There are colliders");
                foreach (Collider collider in colliders)
                {
                    Debug.Log(collider.tag.ToString());
                    if (collider.CompareTag("Seeker") || collider.CompareTag("Player"))
                    {
                        _parent._parent.SetData("target", collider.transform);
                        _animator.SetBool("Walking", true);
                        state = NodeState.SUCCESS;
                        Debug.Log("Found a Seeker!");
                        return state;
                    }
                }
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
