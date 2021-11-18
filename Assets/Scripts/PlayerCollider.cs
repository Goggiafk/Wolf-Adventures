using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerCollider : MonoBehaviour
{
    public GameObject bulletPrefab;
    public ExodusManager manager;
    public GameObject restBody;
    public GameObject restCollider;
    public Exodus badEnd;
    public Exodus goodEnd;
    public GameObject endstick;
    void Update()
    {
        if(OrcheCollision.killedOrches >= 10)
        {
            endstick.SetActive(false);
            restCollider.GetComponent<BoxCollider2D>().enabled = true;
            restCollider.GetComponent<CircleCollider2D>().enabled = true;

            restBody.SetActive(true);
            manager.Change(goodEnd);
            gameObject.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            
        }
    }

    public void shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.transform.position = new Vector2(transform.position.x, transform.position.y + 3);
        bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, transform.position.y + 30), ForceMode2D.Impulse);
    }

    public void moveRight()
    {
        restCollider.GetComponent<Rigidbody2D>().MovePosition(new Vector2(transform.position.x - 1, restCollider.transform.position.y));
    }

    public void moveLeft()
    {
        restCollider.GetComponent<Rigidbody2D>().MovePosition(new Vector2(transform.position.x + 1, restCollider.transform.position.y));
    }
    
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Orche")
        {
            endstick.SetActive(false);
            restCollider.GetComponent<BoxCollider2D>().enabled = true;
            restCollider.GetComponent<CircleCollider2D>().enabled = true;
            
            restBody.SetActive(true);
            manager.Change(badEnd);
            gameObject.SetActive(false);
        }
            

    }
}
