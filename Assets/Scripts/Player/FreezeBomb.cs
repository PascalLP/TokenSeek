using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeBomb : MonoBehaviour
{
    public float radius = 5f; // the radius of the freeze bomb effect
    public float duration = 5f; // the duration of the freeze effect


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)) // check if the player presses the space bar
        {
            FreezeChaser(); // call the FreezeChaser function
        }
        
    }

    void FreezeChaser()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius); // get all colliders within the radius
        GameObject closestChaser = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Chaser"))
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                if (distance < closestDistance)
                {
                    closestChaser = collider.gameObject;
                    closestDistance = distance;
                }
            }
        }

        if (closestChaser != null)
        {
            Chaser chaserController = closestChaser.GetComponent<Chaser>();
            if (chaserController != null)
            {
                chaserController.Freeze(duration); // call the Freeze function on the ChaserController script
            }
        }
    }
}
