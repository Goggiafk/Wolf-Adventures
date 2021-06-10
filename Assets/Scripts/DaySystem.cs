using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaySystem : MonoBehaviour
{
    public static int dayNumber = -1;
    public static int spawnCount;
    public ExodusManager exodusManager;
    void Start()
    {
       DaySystem.StartDay();
    }
    
    public static void StartDay()
    {
        dayNumber++;
        spawnCount = 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
