using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MovingTexture : MonoBehaviour
{

    public float scrollSpeed = 0.5f;
    Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        float offset = Time.time * scrollSpeed;
        rend.material.SetTextureOffset("_BaseMap", new Vector2(offset, 0));
    }
}
