using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLifespanController : MonoBehaviour
{
    public WeaponInfo weaponInfo;
    public static event Action<GameObject> AsteroidIsHit;
    float lifespan = 10f;
    void Update()
    {
        if (lifespan<0)
        {
            gameObject.SetActive(false);
        }
        lifespan-=Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Asteroid") 
        {
            AsteroidIsHit?.Invoke(collision.gameObject);
            gameObject.SetActive(false);
        }
    }
}
