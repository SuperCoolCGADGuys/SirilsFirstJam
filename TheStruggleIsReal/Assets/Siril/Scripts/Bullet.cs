using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public GameObject hitEffect;
    [SerializeField] private float knockbackForce;
    [SerializeField] private float knockbackTime;


    private void OnTriggerEnter2D(Collider2D collider)
    {
        //create effect:
        // GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        // Destroy(effect, 5.0f);
        
        if(collider.tag != "Player")
        {
            //deactivate the object:
            gameObject.SetActive(false);
        }
        else if (gameObject.tag == "EnemyBullet")
        {
            gameObject.SetActive(false);
            collider.GetComponent<PlayerHealthManager>().Damage(1, gameObject.transform.position - collider.transform.position, knockbackForce, knockbackTime);
        }

        if (collider.tag == "Enemy")
		{
			collider.GetComponent<EnemyHealthManager>().Damage(1);
		}
    }
    
}
