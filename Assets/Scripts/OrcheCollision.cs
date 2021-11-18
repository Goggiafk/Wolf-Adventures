using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcheCollision : MonoBehaviour
{
    public static int killedOrches = 0;
    public void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(this.gameObject);
        if(other.gameObject.tag == "Bullet")
            killedOrches++;
       
    }

    
}
