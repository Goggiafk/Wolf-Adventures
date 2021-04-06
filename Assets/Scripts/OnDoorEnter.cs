using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnDoorEnter : MonoBehaviour
{
    public GameObject sitText;
    bool checkRoomKingRoom = false;
    bool checkIsSitting = false;
    public GameObject menu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if (checkIsSitting == true)
            {
                movementScript.playerSpeed = 40;
                menu.SetActive(false);
                sitText.SetActive(true);
                checkIsSitting = false;
            }
            else
            {
                movementScript.playerSpeed = 0;
                menu.SetActive(true);
                sitText.SetActive(false);
                checkIsSitting = true;
            }
        }
    }
    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("lol");
        Vector3 cameraPosition = Camera.main.transform.position;
        if (collision.gameObject.tag == "Door1" && checkRoomKingRoom == false)
        {
            checkRoomKingRoom = true;
            Camera.main.transform.position = new Vector3(-19.6f, cameraPosition.y, cameraPosition.z);
        } else
        {
            checkRoomKingRoom = false;
            Camera.main.transform.position = new Vector3(-1.84f, cameraPosition.y, cameraPosition.z);
        }

        if(collision.gameObject.tag == "Throne")
        {
            checkIsSitting = true;
            sitText.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Throne")
        {
            sitText.SetActive(false);
            checkIsSitting = false;
        }
    }
}
