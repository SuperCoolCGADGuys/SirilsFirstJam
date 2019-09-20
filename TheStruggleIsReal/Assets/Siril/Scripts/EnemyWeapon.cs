using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
	[SerializeField] private float fireRate = 1f;
	[SerializeField] private float spreadAngle = 0f;
	[SerializeField] private float shootingRange = 0f;
	[SerializeField] private LayerMask viewMask;
	private Transform weaponTarget = null;
	private ObjectPooling bulletPool;
	private Transform firePoint;
	private float timeToFire;

	// Start is called before the first frame update
	void Awake()
	{
		// Find the firepoint object
		firePoint = transform.Find("FirePoint");
		if (firePoint == null)
		{
			Debug.Log("Firepoint not found!");
		}

		// Find the bulletPool object
		bulletPool = transform.Find("BulletPool").GetComponent<ObjectPooling>();
		if (bulletPool == null)
		{
			Debug.Log("BulletPool not found!");
		}

		// Find the player and make the weapon target equal to the player's position
		weaponTarget = GameObject.FindGameObjectWithTag("Player").transform;
		if (weaponTarget == null)
		{
			Debug.Log("Player not found in Enemy weapon script!");
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (CanSeeTarget())
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

	private bool CanSeeTarget()
	{
		if (Vector2.Distance(transform.position, weaponTarget.position) < shootingRange)
		{
			if (!Physics2D.Linecast(transform.position, weaponTarget.position, viewMask))
			{
				return true;
			}
		}
		return false;
	}

	private void Shoot()
	{
		// Play weapon shoot sound
		//AudioManager.Instance.PlaySound("Pistol");

		GameObject bullet = bulletPool.RetrieveInstance();
		bullet.transform.position = firePoint.position;

		float rotZ = transform.eulerAngles.z;
		rotZ = Random.Range(rotZ - spreadAngle, rotZ + spreadAngle);

		bullet.transform.rotation = Quaternion.Euler(0, 0, rotZ);
	}
}
