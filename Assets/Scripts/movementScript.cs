using SimpleInputNamespace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementScript : MonoBehaviour
{
    // Start is called before the first frame update

    public CharacterController2D controller;

    public static float playerSpeed = 40f;

    float horizontalMove = 0f;

    public Animator animator;

    public Joystick joystick;

    bool jump = false;
    bool crouch = false;

    public AudioClip clip;
    public AudioSource source;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!source.isPlaying && Mathf.Abs(horizontalMove) > 0.01f)
            source.PlayOneShot(clip);
        horizontalMove = joystick.xAxis.value * playerSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

  

    }
    public void Jumper()
    {
        jump = true;
        animator.SetBool("IsJumping", true);
    }
    
    public void Croucher()
    {
        crouch = !crouch;
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }

    public void OnCrouching(bool isCrouching)
    {
        animator.SetBool("IsCrouching", isCrouching);
    }
    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }
}
