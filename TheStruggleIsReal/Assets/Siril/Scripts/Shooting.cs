using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
   // public GameObject bulletPrefab;

    [SerializeField] private ObjectPooling bulletPool;
    public float bulletForce = 20.0f;
    //fire rate:
    [SerializeField] private float fireRate = 1f;
    private float timeToFire = 0.0f;


    // Update is called once per frame
    void Update()
    {
		// This code shouldn't run if the game is paused
		if (GameManager.Instance.GameIsPaused)
		{
			return;
		}

        if(Input.GetButton("Fire1"))
        {
            if (GameManager.Instance.PlayerBulletsAlive < bulletPool.poolSize)
            {
                if (fireRate == 0)
                {
                    Shoot();
                }
                else
                {
                    if (Time.time > timeToFire)
                    {
                        timeToFire = Time.time + 1 / fireRate;
                        Shoot();
                    }
                }
            }
        }
    }

    void Shoot()
    {
        GameObject newBullet = bulletPool.RetrieveInstance();
        newBullet.transform.position = firePoint.position;
        newBullet.transform.rotation = firePoint.rotation;

        Rigidbody2D rb = newBullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);

        GameManager.Instance.PlayerBulletsAlive++;
    }
}
