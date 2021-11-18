using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public ExodusManager manager;
    public AchievementManager achievement;


    void OnCollisionEnter2D(Collision2D col)
    {

            switch (this.gameObject.name)
            {

                case "ticker":
                    achievement.RequestStats();
                    achievement.SetAchievement("NEW_ACHIEVEMENT_1_5");
                    manager.adTheGet("Ведро", "Bucket", "Ведро, найденное на болоте", "The bucket you got from swamps", "Vedro", 10, 'M');
                    break;

            }
            Destroy(this.gameObject);
        
    }
}
