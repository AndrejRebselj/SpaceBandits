using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovingController : MonoBehaviour
{
    int speed=5;
    Touch touch;
    GameObject player;
    Rigidbody2D playerBody;
    Gyroscope gyroscope;
    bool useSensor = false;
    Vector2 movement;
    void Start()
    {
        player = GameObject.Find("Player");
        if (PlayerPrefs.GetInt("UseSensors",0)==1&&SystemInfo.supportsGyroscope)
        {
            useSensor = true;
            gyroscope = Input.gyro;
            gyroscope.enabled = true;
            playerBody=player.AddComponent<Rigidbody2D>();
            playerBody.bodyType = RigidbodyType2D.Dynamic;
            playerBody.gravityScale = 0;
        }
    }
    void Update()
    {
        if (useSensor)
        {
            movement = new Vector2(gyroscope.attitude.x * speed, gyroscope.attitude.y * speed);
            player.transform.position = new Vector2(Mathf.Clamp(player.transform.position.x, -2.815f, 2.815f), Mathf.Clamp(player.transform.position.y, -5f, 5f));
        }
        else if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
        }
    }
    private void FixedUpdate()
    {
        if (useSensor)
        {
            playerBody.velocity = movement;
        }
        else if(touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
        {
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));
            player.transform.position = Vector3.MoveTowards(player.transform.position, touchPosition, speed * Time.deltaTime);
        }
    }
}
