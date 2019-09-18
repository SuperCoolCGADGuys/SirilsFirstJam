using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
	[SerializeField] private int maxHealth = 4;
	private int currentHealth;
	private Animator anim;

	private void Awake()
	{
		anim = GetComponent<Animator>();
		if (anim == null)
		{
			Debug.Log("Animator for " + gameObject + " not found!");
		}
	}

	// Start is called before the first frame update
	void Start()
	{
		currentHealth = maxHealth;
	}

	public void Damage(int amount)
	{
		if ((currentHealth - amount) <= 0)
		{
			// Dead
			currentHealth = 0;
			Kill();
		}
		else
		{
			currentHealth -= amount;
			anim.SetTrigger("Hurt");
		}
	}

	private void Kill()
	{
		// Play death animation sound and particles

		// Set object be be inactive
		gameObject.SetActive(false);
	}

	public void Reset()
	{
		currentHealth = maxHealth;
	}
}
