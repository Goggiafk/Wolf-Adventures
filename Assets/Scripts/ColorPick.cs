using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPick : MonoBehaviour
{
    public SpriteRenderer BalkRenderer;
    public Text textOfWolf;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeColor(int id)
    {
        switch (id)
        {
            case 1:
                BalkRenderer.color = Color.green; break;
            case 2:
                BalkRenderer.color = Color.blue; break;
            case 3:
                BalkRenderer.color = Color.yellow; break;
            case 4:
                BalkRenderer.color = Color.magenta;  break;
            case 5:
                BalkRenderer.color = Color.red; break;
            case 6:
                BalkRenderer.color = Color.white; break;
        }
    }

}
