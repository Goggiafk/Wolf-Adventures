using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScript : MonoBehaviour
{
   
    public Image image;
    public Text text;
    public static string[] appear = new string[100];
    public static string[] appearText = new string[100];
    public static int idAppear = 0;

    public void appearScript()
    {
        text.text = appearText[idAppear];
        var spam = Resources.LoadAll("Character/" + appear[idAppear]);
        image.sprite = spam[1] as Sprite; 
        idAppear--;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
