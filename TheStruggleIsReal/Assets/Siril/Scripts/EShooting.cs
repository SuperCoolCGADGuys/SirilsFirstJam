using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EShooting : MonoBehaviour
{
    public Transform eFirePoint;

    [SerializeField] private ObjectPooling bulletPool;
    public float bulletForce = 20.0f;

    //reference player position:
    public Transform player;
    //how close the enemy can get before shooting:
    public float shootingRange;

    //shooting speed:
    private float currentCooldown;
    public float cooldownTime;
    //shooting spread angle:
    [SerializeField] private float spreadAngle;

    [SerializeField] private LayerMask viewMask;   


    void Start()
    {
        //reset the timer:
        currentCooldown = cooldownTime;
    }

    // Update is called once per frame
    void Update()
    {
        //ray cast:
        if (CanSeeTarget())
        {
            //if cooldown is finished:
            if (currentCooldown <= 0)
            {
                //shoot:
                Shoot();
                //reset cooldown:
                currentCooldown = cooldownTime;
            }
        }  
        //keep counting down:
        if (currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;
        }
    }

    void Shoot()
    {
        GameObject newBullet = bulletPool.RetrieveInstance();
        newBullet.transform.position = eFirePoint.position;
       // newBullet.transform.rotation = eFirePoint.rotation;

        //weapon spread angle:
        float rotZ = transform.eulerAngles.z;
        rotZ = Random.Range(rotZ - spreadAngle, rotZ + spreadAngle);
        //apply the weapon spread:
        newBullet.transform.rotation = Quaternion.Euler(0, 0, rotZ);

        Rigidbody2D rb = newBullet.GetComponent<Rigidbody2D>();
        rb.AddForce(newBullet.transform.up * bulletForce, ForceMode2D.Impulse);
    }


    private bool CanSeeTarget()
    {
        if (Vector2.Distance(transform.position, player.position) < shootingRange)
        {
            if (!Physics2D.Linecast(transform.position, player.position, viewMask))
            {
                return true;
            }
        }
        return false;
    }

}
