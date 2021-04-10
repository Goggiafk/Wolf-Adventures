using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManagment : MonoBehaviour
{
    public GameObject[] locations;
    public void enableLocation(GameObject location)
    {
        Camera.main.transform.position = location.transform.position + new Vector3(0, 0, 10);
        location.SetActive(true);
    }
}
