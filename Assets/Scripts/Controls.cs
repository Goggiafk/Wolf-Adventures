using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    public Animator animator;
    public GameObject menu;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menu.SetActive(true);
            Time.timeScale = 0;
        }
    }

    void Start()
    {
        if (PlayerPrefs.HasKey("shuffle"))
        {
            animator.SetInteger("shuffle", PlayerPrefs.GetInt("shuffle"));
        }
    }
    public void contin()
    {
        Time.timeScale = 1;
        menu.SetActive(false);
    }
}
