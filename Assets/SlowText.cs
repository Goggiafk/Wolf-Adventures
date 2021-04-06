using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlowText : MonoBehaviour
{
    public float delay = 0.07f;
    string currentText;
    string fullText;

    void Start()
    {
        startTextCoroutine(this.GetComponent<Text>().text);
    }

    void Update()
    {
        
    }
    
    public void startTextCoroutine(string lol)
    {
        StopAllCoroutines();
        StartCoroutine(showText(lol));
    }

     IEnumerator showText(string given)
    {
        fullText = given;
        this.GetComponent<Text>().text = "";
        for (int i = 0; i <= fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            this.GetComponent<Text>().text = currentText;
            yield return new WaitForSeconds(delay);
        }
    }
}
