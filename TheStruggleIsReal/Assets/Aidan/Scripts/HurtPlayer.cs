using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
	[SerializeField]
	int damageToGive;

	[SerializeField]
	float knockBackForce;
	[SerializeField]
	float knockBackTime;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			Vector2 knockBackDir = other.transform.position - transform.position;
			knockBackDir = knockBackDir.normalized;
			other.gameObject.GetComponent<PlayerHealthManager>().Damage(damageToGive, knockBackDir, knockBackForce, knockBackTime);
		}
	}
}
