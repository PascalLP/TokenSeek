using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class Token : MonoBehaviour
{
    public GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(GameManager.instance != null)
        {
            if (other.tag == "Seeker")
            {
                CoinSpawner.instance.tokenTotal--;
                GameManager.instance.AddScore("Seeker");
                //Destroy(gameObject);
            }
            else if (other.tag == "Player")
            {
                CoinSpawner.instance.tokenTotal--;
                GameManager.instance.AddScore("Player");
                Destroy(gameObject);
            }

        }
    }

    private void OnDestroy()
    {
        if(CoinSpawner.instance.tokenTotal == 0)
        {

        }
    }
}