using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    [SerializeField]
    GameObject pointsIndicator;
    [SerializeField]
    GameObject deathScreen;
    GameObject player;
    GameObject cannon01;
    GameObject cannon02;
    [SerializeField]
    List<GameObject> weapon1;
    [SerializeField]
    List<GameObject> weapon2;
    GameObject projectile;
    float cooldown = 2f;
    int projectileSpeed=7;
    InterstitialAdExample initAd;
    AudioSource gunNoise;
    Material playerMaterial;
    bool death = false;
    float fade = 1f;

    void Start()
    {
        initAd = GetComponent<InterstitialAdExample>();
        gunNoise = GetComponent<AudioSource>();
        gunNoise.volume = PlayerPrefs.GetFloat("AudioVolume", 1f);
        initAd.LoadAd();
        switch (StartGameData.PickedWeapon)
        {
            case 1:
                projectile = weapon1[PrefabVariables.Weapon1Level-1];
                break;
            case 2:
                projectile = weapon2[PrefabVariables.Weapon2Level - 1];
                break;
            default:
                projectile = weapon1[0];
                break;
        }
        player = GameObject.Find("Player");
        cannon01 = player.GetComponentsInChildren<Transform>()[2].gameObject;
        cannon02 = player.GetComponentsInChildren<Transform>()[3].gameObject;
        playerMaterial = player.GetComponent<Material>();
        PlayerCollisionController.PlayerIsHit += PlayerHasBeenHit;
        ProjectileLifespanController.AsteroidIsHit += AsteroidHasBeenHit;
    }

    private void PlayerHasBeenHit()
    {
        death = true;
        initAd.ShowAd();
        player.SetActive(false);
        deathScreen.SetActive(true);
        Time.timeScale = 0;
        
    }

    public void AsteroidHasBeenHit(GameObject collision) 
    {
        AsteroidController ac = collision.gameObject.GetComponent<AsteroidController>();
        ac.Health-=projectile.GetComponent<ProjectileLifespanController>().weaponInfo.Damage;
        if (ac.Health<=0)
        {
            pointsIndicator.GetComponent<TextMesh>().text = $"+{ac.Points}";
            Instantiate(pointsIndicator, new Vector3(collision.transform.position.x, collision.transform.position.y, collision.transform.position.z), Quaternion.identity);
            PrefabVariables.GoldCurrency += ac.Points;
            Destroy(collision.gameObject);
        }

    }

    private void Update()
    {
        if (cooldown<0)
        {
            cooldown = 2f;
            gunNoise.Play();
            GameObject proj1 = Instantiate(projectile, cannon01.transform);
            GameObject proj2 = Instantiate(projectile, cannon02.transform);
            proj1.GetComponent<Rigidbody2D>().velocity = Vector2.up*projectileSpeed;
            proj2.GetComponent<Rigidbody2D>().velocity = Vector2.up*projectileSpeed;
        }
        if (death)
        {
            fade-=Time.deltaTime;
            if (fade<=0f)
            {
                fade = 0f;
                death = false;
            }
            playerMaterial.SetFloat("_Fade", fade);

        }
        cooldown-=Time.deltaTime;
    }

    private void OnDestroy()
    {
        PlayerCollisionController.PlayerIsHit -= PlayerHasBeenHit;
        ProjectileLifespanController.AsteroidIsHit -= AsteroidHasBeenHit;
    }
}
