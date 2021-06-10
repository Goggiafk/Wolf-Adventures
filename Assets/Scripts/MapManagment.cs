using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManagment : MonoBehaviour
{
    public GameObject player;
    public GameObject cameraMan;

    public GameObject devMenu;
    public void enableLocation(GameObject location)
    {
        player.transform.position = new Vector3(location.transform.position.x, location.transform.position.y, -1);
        cameraMan.transform.position = location.transform.position + new Vector3(0, 0, -15);
        location.SetActive(true);
    }

    void Update()
    {
        var campos = cameraMan.transform.position;
        cameraMan.GetComponent<Rigidbody2D>().MovePosition(new Vector2(player.transform.position.x, 0));

        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            devMenu.SetActive(!devMenu.activeInHierarchy);
        }
    }
}
