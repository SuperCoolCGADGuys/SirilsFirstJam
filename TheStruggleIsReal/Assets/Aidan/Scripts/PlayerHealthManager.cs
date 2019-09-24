using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class PlayerHealthManager : MonoBehaviour
{
	[SerializeField] private int maxHealth = 4;
	[SerializeField] private float invincibilityTime = 2f;
	[SerializeField] SpriteRenderer spriteRenderer;
	[SerializeField] private float flashLength = 1f;
	[SerializeField] private Transform spawnPosition = null;

	private float elapsedInvincibilityTime = 0;
	private float flashCounter;
	private int currentHealth;

	// Start is called before the first frame update
	void Start()
	{
		currentHealth = maxHealth;
	}

	private void Update()
	{
		// If the player has been hit enable invincibility time
		if (elapsedInvincibilityTime > 0)
		{
			elapsedInvincibilityTime -= Time.unscaledDeltaTime;
			flashCounter -= Time.unscaledDeltaTime;

			if (flashCounter <= 0)
			{
				spriteRenderer.enabled = !spriteRenderer.enabled;
				flashCounter = flashLength;
			}

			if (elapsedInvincibilityTime <= 0)
			{
				spriteRenderer.enabled = true;
			}
		}
	}

	public void Damage(int amount, Vector2 knockBackDir, float knockBackForce, float knockBackTime)
	{
		if (elapsedInvincibilityTime <= 0)
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
			}
			gameObject.GetComponent<PlayerMovement>().KnockBack(knockBackDir, knockBackForce, knockBackTime);
			elapsedInvincibilityTime = invincibilityTime;
			flashCounter = flashLength;

			// Shake the screen
			CameraShaker.Instance.ShakeOnce(4f, 4f, .2f, .1f);
		}
	}

	private void Kill()
	{
		// Play death animation sound and particles

		GameManager.Instance.GameOver();
	}

	public void ResetPlayer()
	{
		currentHealth = maxHealth;
		Shooting shooting = GetComponent<Shooting>();
		shooting.ResetBullets();
		transform.position = spawnPosition.position;
		elapsedInvincibilityTime = 0;
		flashCounter = 0;
		spriteRenderer.enabled = true;
	}
}
