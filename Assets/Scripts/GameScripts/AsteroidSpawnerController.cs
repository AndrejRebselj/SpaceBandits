using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawnerController : MonoBehaviour
{
    public float cooldown = 5f;
    public float level1 = 5f;
    public float level2 = 4f;
    public float level3 = 3f;
    public float level4 = 2f;
    float force = 1f;
    [SerializeField]
    List<GameObject> asteroids = new List<GameObject>();
    MyLabel timer;
    float timePassed = 0f;
    void Start()
    {
        timer = new MyLabel("Timer");
    }

    void Update()
    {
        timePassed+=Time.deltaTime;
        timer.SetText(timePassed.ToString("0.0"));
        if (timePassed < 30f)
        {
            if (cooldown < 0)
            {
                cooldown = level1;
                GameObject a = Instantiate(asteroids[0], new Vector3(Random.Range(-2f, 2f), 5.25f, 0), Quaternion.identity);
                a.GetComponent<Rigidbody2D>().velocity = Vector2.down * force;
            }
            cooldown -= Time.deltaTime;
        }
        else if (timePassed < 60f)
        {
            if (cooldown < 0)
            {
                cooldown = level2;
                GameObject a = Instantiate(asteroids[Random.Range(0, 2)], new Vector3(Random.Range(-2f, 2f), 5.25f, 0), Quaternion.identity);
                a.GetComponent<Rigidbody2D>().velocity = Vector2.down * force;
            }
            cooldown -= Time.deltaTime;
        }
        else if (timePassed < 90f)
        {
            if (cooldown < 0)
            {
                cooldown = level3;
                GameObject a = Instantiate(asteroids[Random.Range(0, 3)], new Vector3(Random.Range(-2f, 2f), 5.25f, 0), Quaternion.identity);
                a.GetComponent<Rigidbody2D>().velocity = Vector2.down * force;
            }
            cooldown -= Time.deltaTime;
        }
        else 
        {
            if (cooldown < 0)
            {
                cooldown = level4;
                GameObject a = Instantiate(asteroids[Random.Range(0, 3)], new Vector3(Random.Range(-2f, 2f), 5.25f, 0), Quaternion.identity);
                a.GetComponent<Rigidbody2D>().velocity = Vector2.down * force*1.5f;
            }
            cooldown -= Time.deltaTime;
        }
    }
}
