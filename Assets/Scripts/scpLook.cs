using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scpLook : MonoBehaviour
{
    public Transform player;
    public Animator animation;

    public void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(player.transform.position + new Vector3(1.5f, 0, 0), Vector2.right * 8);
        if(CharacterController2D.isTurned)
            hit = Physics2D.Raycast(player.transform.position + new Vector3(1.5f, 0, 0), Vector2.left * 8);
        Debug.DrawRay(player.transform.position + new Vector3(1.5f, 0, 0), Vector2.right * 8, Color.red);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "scp")
                animation.speed = 0;
            else
                animation.speed = 1;
        } else
            animation.speed = 1;
    }
}
