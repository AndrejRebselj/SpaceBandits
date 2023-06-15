using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsLifespanController : MonoBehaviour
{
    float lifespan = 0.6f;
    void Update()
    {
        if (lifespan < 0f)
        {
            gameObject.SetActive(false);
        }
        lifespan -= Time.deltaTime;
    }
}
