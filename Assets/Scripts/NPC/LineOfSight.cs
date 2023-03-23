using UnityEngine;

public class LineOfSight : MonoBehaviour
{
    public Transform target;  // The target that the agent is trying to see

    public float sightRadius = 10f;  // The radius within which the agent can see the target
    public float fieldOfViewAngle = 90f;  // The angle of the agent's field of view

    private bool hasLineOfSight;  // Whether the agent currently has a clear line of sight to the target

    private void Update()
    {
        // Check if the target is within sight radius
        if (Vector3.Distance(transform.position, target.position) <= sightRadius)
        {
            // Check if the target is within field of view angle
            Vector3 directionToTarget = target.position - transform.position;
            float angle = Vector3.Angle(directionToTarget, transform.forward);
            if (angle <= fieldOfViewAngle * 0.5f)
            {
                // Check if there are any obstacles between the agent and target
                RaycastHit hitInfo;
                if (Physics.Raycast(transform.position, directionToTarget, out hitInfo, sightRadius))
                {
                    if (hitInfo.transform == target)
                    {
                        // The target is visible to the agent
                        hasLineOfSight = true;
                        return;
                    }
                }
            }
        }

        // The target is not visible to the agent
        hasLineOfSight = false;
    }

    public bool HasLineOfSight()
    {
        return hasLineOfSight;
    }
}

