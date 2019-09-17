using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TempPlayerMovementScript : MonoBehaviour
{
	private PlayerControls playerControls = null;
	[SerializeField] private float movementSpeed = 5f;
	[SerializeField] private float smoothMoveTime = .1f;
	private float smoothInputMagnitude = 0f;
	private float smoothMoveVelocity = 0f;
	private Vector2 velocity = Vector2.zero;
	private Vector2 move = Vector2.zero;

	Rigidbody2D rb;

	private void Awake()
	{
		playerControls = new PlayerControls();

		rb = GetComponent<Rigidbody2D>();
		if (rb == null)
		{
			Debug.Log("Rigidbody not found in player controller!");
		}
	}

	private void Update()
	{
		float moveMagnitude = move.magnitude;
		smoothInputMagnitude = Mathf.SmoothDamp(smoothInputMagnitude, moveMagnitude, ref smoothMoveVelocity, smoothMoveTime);

		velocity = move * smoothInputMagnitude * movementSpeed;
	}

	private void FixedUpdate()
	{
		rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
	}

	private void OnEnable()
	{
		playerControls.Gameplay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
		playerControls.Gameplay.Move.canceled += ctx => move = Vector2.zero;
		playerControls.Enable();
	}

	private void OnDisable()
	{
		playerControls.Gameplay.Move.performed -= ctx => move = ctx.ReadValue<Vector2>();
		playerControls.Gameplay.Move.canceled -= ctx => move = Vector2.zero;
		playerControls.Disable();
	}
}
