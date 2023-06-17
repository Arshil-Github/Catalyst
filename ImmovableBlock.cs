using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmovableBlock : MonoBehaviour
{
    // Update is called once per frame
    void Start()
    {
        Invoke("SnaptoGrid", 0.001f);
    }
    void SnaptoGrid()
    {
        transform.position = GetClosestObject(GameObject.FindGameObjectsWithTag("Blocks")).position;
    }
    Transform GetClosestObject(GameObject[] objects)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (GameObject t in objects)
        {
            float dist = Vector3.Distance(t.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = t.transform;
                minDist = dist;
            }
        }
        return tMin;
    }
}
