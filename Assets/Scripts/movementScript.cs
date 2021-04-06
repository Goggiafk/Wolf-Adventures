using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementScript : MonoBehaviour
{
    // Start is called before the first frame update

    public CharacterController2D controller;

    public static float playerSpeed = 40f;

    float horizontalMove = 0f;

    bool jump = false;
    bool crouch = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * playerSpeed;

        if (Input.GetKeyDown(KeyCode.Space)) {
            jump = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            crouch = true;
        } else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            crouch = false;
        }
    }
    
    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }
}
