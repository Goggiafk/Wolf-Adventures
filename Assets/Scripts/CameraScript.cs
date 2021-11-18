using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    
    public static bool isLerping = false;
    static Vector3 cameraStartPosition;
    static Vector3 target;
    public float speed = 0.05f;
    public GameObject cameraOf;

    public static void moveCamera(Vector3 transformOfObject, GameObject camera)
    {
        isLerping = true;
        cameraStartPosition = camera.transform.position;
        target = transformOfObject;
    }


    void FixedUpdate()
    {
        if (isLerping)
        {
            
            var mapeer = Mathf.Lerp(cameraOf.transform.position.x, target.x, speed * Time.deltaTime);
            cameraOf.GetComponent<Rigidbody2D>().MovePosition(new Vector2(mapeer, 0));
            //if (target == etransform.position)
            //   isLerping = false;
        }
    }
}
