using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public GameObject hitEffect;
    [SerializeField] private float knockbackForce;
    [SerializeField] private float knockbackTime;
	[SerializeField] private float timeUntilBulletGetsDestroyed = 5f;
	[SerializeField] private int amountOfDamage = 1;
	private float elapsedTimeSinceSpawned = 0;

	private void OnTriggerEnter2D(Collider2D collider)
    {
		//create effect:
		// GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
		// Destroy(effect, 5.0f);

		//deactivate the object:
		if (collider.tag != "PlayerBullet" && collider.tag != "EnemyBullet")
		{
			gameObject.SetActive(false);
		}
        
        if (collider.tag == "Player" && gameObject.tag == "EnemyBullet")
        {
			// Handle player damage and knockback
			Vector2 knockBackDir = collider.transform.position - transform.position;
			knockBackDir = knockBackDir.normalized;
			collider.GetComponent<PlayerHealthManager>().Damage(amountOfDamage, knockBackDir, knockbackForce, knockbackTime);
        }

        if (collider.tag == "Enemy" && gameObject.tag == "PlayerBullet")
		{
			collider.GetComponent<EnemyHealthManager>().Damage(amountOfDamage);
		}
    }

	private void OnEnable()
	{
		// When bullet is spawned in set the timer to go off for despawning the bullet if it doesn't hit anything
		elapsedTimeSinceSpawned = timeUntilBulletGetsDestroyed;
	}

	private void Update()
	{
		if (elapsedTimeSinceSpawned > 0)
		{
			elapsedTimeSinceSpawned -= Time.deltaTime;

			if (elapsedTimeSinceSpawned <= 0)
			{
				gameObject.SetActive(false);
			}
		}
	}

}
