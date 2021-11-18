using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOrches : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject orchePrefab;
    public GameObject saveObject;
    public Transform pointOfSpawn;
    void Start()
    {
        StartCoroutine(Timer(0.3f, () => {; }));
    }

    IEnumerator Timer(float time, System.Action action)
    {
        saveObject = Instantiate(orchePrefab); saveObject.transform.position = new Vector2(pointOfSpawn.position.x + Random.Range(-6, 7), pointOfSpawn.position.y);
        while (time > 0)
        {
            time -= Time.deltaTime;
            yield return null;
        }
        
        StartCoroutine(Timer(0.3f, () => {; }));

    }
}
