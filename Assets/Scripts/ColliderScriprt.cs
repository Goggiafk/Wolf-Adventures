using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderScriprt : MonoBehaviour
{
    public MapManagment mapManagment;
    public GameObject spawnPoint;
    public AchievementManager manager;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
            mapManagment.enableLocation(spawnPoint);
        if (this.gameObject.name == "towerFall")
            manager.SetAchievement("NEW_ACHIEVEMENT_1_1");

    }


}
