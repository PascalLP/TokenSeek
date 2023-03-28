using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;

public class GoToLastPosition : Node
{
    private Transform _transform;

    private float waitTime;
    private float maxWaitTime;

    public GoToLastPosition(Transform transform, float waitingTime)
    {
        _transform = transform;
        maxWaitTime = waitingTime;
        waitTime = 0f;
    }

    public override NodeState Evaluate()
    {

        if (ChaserBT.lastSeenPosition != null)
        {
            if (Vector3.Distance(_transform.position, ChaserBT.lastSeenPosition.position) >= 0.05f)
            {
                Debug.Log("Going to last position of target");
                _transform.position = Vector3.MoveTowards(_transform.position, ChaserBT.lastSeenPosition.position, ChaserBT.speed * Time.deltaTime);
                _transform.LookAt(ChaserBT.lastSeenPosition.position);
                return NodeState.RUNNING;
            }
            else if (Vector3.Distance(_transform.position, ChaserBT.lastSeenPosition.position) < 0.05f * ChaserBT.speed * Time.deltaTime);
            {
                Debug.Log("Reached last position of target");
                ChaserBT.animator.SetBool("Walking", false);
                if(waitTime < maxWaitTime)
                {
                    waitTime += Time.deltaTime;
                    return NodeState.RUNNING;
                }
                else
                {
                    waitTime = 0f;
                    ChaserBT.lastSeenPosition = null;
                    return NodeState.SUCCESS;
                }

            }
        }

        state = NodeState.SUCCESS;
        return state;
    }

    IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        // Move towards the target position until the agent reaches it
        while (_transform.position != targetPosition)
        {
            _transform.position = Vector3.MoveTowards(_transform.position, targetPosition, ChaserBT.speed * Time.deltaTime);
            _transform.LookAt(_transform.forward);
            yield return null;
        }

        // When the agent reaches the last known position of the target, wait for 3 seconds
        /*isMovingToLastKnownPosition = false;
        isWaitingAtLastKnownPosition = true;*/
        yield return new WaitForSeconds(3f);
    }
}
