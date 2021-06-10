using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    public Animator animator;
    public GameObject menu;
    public GameObject settings;
    public GameObject allUI;
    public bool checkMenuClosed = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (settings.activeInHierarchy)
            {
                settings.SetActive(false);
                menu.SetActive(true);
            }
            else if (menu.activeInHierarchy)
            {
                allUI.SetActive(true);
                menu.SetActive(false);
                Time.timeScale = 1;
            }
            else
            {
                allUI.SetActive(false);
                menu.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }

    void Start()
    {
        if (PlayerPrefs.HasKey("shuffle"))
        {
            animator.SetInteger("shuffle", PlayerPrefs.GetInt("shuffle"));
            if (PlayerPrefs.GetInt("shuffle")  <= 0)
            {
                
            }
        }
    }
    public void contin()
    {
        if (PlayerPrefs.HasKey("shuffle"))
        {
            animator.SetInteger("shuffle", PlayerPrefs.GetInt("shuffle"));
        }
        Time.timeScale = 1;
        menu.SetActive(false);
    }
}
