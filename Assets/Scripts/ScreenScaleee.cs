using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenScaleee : MonoBehaviour
{
    public float resoX;
    public float resoY;

    private CanvasScaler canvasScaler;
    void Start()
    {
        canvasScaler = GetComponent<CanvasScaler>();
        GetInfo();
    }

    public void GetInfo()
    {
        resoX = (float)Screen.currentResolution.width;
        resoY = (float)Screen.currentResolution.height;
        canvasScaler.referenceResolution = new Vector2(resoX/2, resoY/2);
    }
}
