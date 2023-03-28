using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : MonoBehaviour
{
    private Rigidbody rb;
    private bool isFrozen = false;
    private float freezeDuration = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isFrozen)
        {
            // if frozen, stop moving and rotating
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            freezeDuration -= Time.deltaTime;
            if (freezeDuration <= 0f)
            {
                isFrozen = false;
            }
        }
    }

    public void Freeze(float duration)
    {
        isFrozen = true;
        freezeDuration = duration;
    }
}
