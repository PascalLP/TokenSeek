using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenSearch : MonoBehaviour
{
    public float speed = 5f; 
    public float pickupRadius = 3f;

    // Tokens in the scene
    public GameObject[] tokens; 
    private int currentTokenIndex = 0;

    void Start()
    {
        // Find all Tokens
        tokens = GameObject.FindGameObjectsWithTag("Token");
    }

    void Update()
    {
        if (tokens.Length == 0)
        {
            return;
        }

        // Find the closest token
        GameObject closestToken = null;
        float closestDistance = Mathf.Infinity;

        for (int i = 0; i < tokens.Length; i++)
        {
            if (tokens[i] == null && i != 0)
                continue;

            float distance = Vector3.Distance(transform.position, tokens[i].transform.position);
            if (distance < closestDistance)
            {
                closestToken = tokens[i];
                closestDistance = distance;
            }
        }

        // Move towards the closest token
        Vector3 direction = closestToken.transform.position - transform.position;
        transform.position += direction.normalized * speed * Time.deltaTime;
        transform.LookAt(closestToken.transform.position);

        // Check if the token is close enough to pick up
        if (closestDistance < pickupRadius)
        {
            // Destroy the token and move on to the next one
            Destroy(closestToken, 1f);
            currentTokenIndex++;

            // Reset after picking up all of them
            if (currentTokenIndex >= tokens.Length)
            {
                currentTokenIndex = 0;
            }
        }
    }
}
