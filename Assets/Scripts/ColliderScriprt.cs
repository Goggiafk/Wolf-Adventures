using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderScriprt : MonoBehaviour
{
    public MapManagment mapManagment;
    public GameObject spawnPoint;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (this.gameObject.name == "towerEnter")
            mapManagment.enableLocation(spawnPoint);
        if (this.gameObject.name == "towerFall")
            mapManagment.enableLocation(spawnPoint);
    }
}
