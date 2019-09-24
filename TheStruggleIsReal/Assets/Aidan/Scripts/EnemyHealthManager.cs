using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
	[SerializeField] private int maxHealth = 4;
	private int currentHealth;
	private Animator anim;
	private SpriteRenderer spriteRenderer = null;
	private Color originalSpriteColor = Color.white;

	private void Awake()
	{
		// Get the animator
		anim = GetComponent<Animator>();
		if (anim == null)
		{
			Debug.Log("Animator for " + gameObject + " not found!");
		}

		// Get the sprite renderer
		spriteRenderer = transform.Find("Graphics").GetComponent<SpriteRenderer>();
		if (spriteRenderer == null)
		{
			Debug.Log("Sprite renderer not found!");
		}

		// Store the original sprite colour
		originalSpriteColor = spriteRenderer.color;

		anim.updateMode = AnimatorUpdateMode.UnscaledTime;
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

		// Decrement alive enemies
		GameManager.Instance.EnemiesAlive--;

		// Increment player kill count
		GameManager.Instance.EnemiesKilled++;

		ResetEnemy();
	}

	public void ResetEnemy()
	{
		currentHealth = maxHealth;
		anim.SetTrigger("Reset");
		spriteRenderer.color = originalSpriteColor;
	}
}
