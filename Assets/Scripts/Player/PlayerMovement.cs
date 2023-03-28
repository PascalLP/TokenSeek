using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Animator m_Animator;
    [SerializeField] private Camera m_Camera;
    [SerializeField] public float speed;

    public int playerCoins;
    public static PlayerMovement instance;

    // Start is called before the first frame update
    void Start()
    {
        playerCoins = 0;
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 xMove = m_Camera.transform.right * horizontal;
        Vector3 zMove = m_Camera.transform.forward * vertical;
        zMove.y = 0;
        

        Vector3 movement = xMove + zMove;

        // Movement
        horizontal *= speed * Time.deltaTime;
        vertical *= speed * Time.deltaTime;

        // Camera

        if (movement.magnitude != 0)
        {
            transform.rotation = Quaternion.LookRotation(movement);
            transform.Translate(horizontal, 0, vertical);
            //TODO 2: Control your animator
            m_Animator.SetBool("isWalking", true);

            if (Input.GetKey(KeyCode.LeftShift))
            {
                //m_Animator.SetBool("isWalking", true);
                m_Animator.SetBool("isRunning", true);
            }
            else
            {
                m_Animator.SetBool("isRunning", false);
                //m_Animator.SetBool("isWalking", true);
            }

        }
        else
        {
            //TODO 2: Control your animator
            m_Animator.SetBool("isWalking", false);
            m_Animator.SetBool("isRunning", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }


    public void AddCoin()
    {
        playerCoins++;
    }
}
