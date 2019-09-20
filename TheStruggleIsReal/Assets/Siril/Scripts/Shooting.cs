using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
   // public GameObject bulletPrefab;

    [SerializeField] private ObjectPooling bulletPool;
    public float bulletForce = 20.0f;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();

        }

    }

    void Shoot()
    {
        GameObject newBullet = bulletPool.RetrieveInstance();
        newBullet.transform.position = firePoint.position;
        newBullet.transform.rotation = firePoint.rotation;

        Rigidbody2D rb = newBullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);

    }
}
