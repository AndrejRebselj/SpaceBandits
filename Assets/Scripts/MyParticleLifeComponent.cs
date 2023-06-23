using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyParticleLifeComponent : MonoBehaviour
{
    public float lifeSpan;

    private void Update()
    {
        lifeSpan-=Time.deltaTime;
        if (lifeSpan <= 0) 
        {
            gameObject.SetActive(false);
        }
    }

}
