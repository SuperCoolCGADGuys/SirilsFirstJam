using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public GameObject hitEffect;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //create effect:
       // GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
       // Destroy(effect, 5.0f);

        if(collider.tag != "Player")
        {
            Debug.Log("Hit Something!!");
            //deactivate the object:
            gameObject.SetActive(false);
        }

        
    }
    
}
