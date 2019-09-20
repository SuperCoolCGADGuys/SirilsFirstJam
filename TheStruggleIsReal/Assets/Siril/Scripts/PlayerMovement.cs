using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Camera cam;

    Vector2 movement;
    Vector2 mousePos;
	Vector2 velocity = Vector2.zero;

	private float knockbackCounter = 0f;

	void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

		// Calculate the velocity after the knockback has happened
		if (knockbackCounter <= 0)
		{
			// Calculate the velocity
			velocity = movement * moveSpeed;
		}
		else
		{
			knockbackCounter -= Time.deltaTime;
		}

	}

    void FixedUpdate()
    {
		// Move the player
		rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);

        //get direction to look at:
        Vector2 lookDir = mousePos - rb.position;
        //convert to angle:
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

	public void KnockBack(Vector2 knockBackDir, float knockBackForce, float knockBackTime)
	{
		// Handle player knockback
		knockbackCounter = knockBackTime;
		StartCoroutine(KnockCo(knockBackTime));
		velocity = knockBackDir * knockBackForce;
	}

	private IEnumerator KnockCo(float knockTime)
	{
		yield return new WaitForSeconds(knockTime);
		velocity = Vector2.zero;
	}

}
