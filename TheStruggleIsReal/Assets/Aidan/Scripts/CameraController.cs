using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	[SerializeField] private Transform target;
	[SerializeField] private float smoothTime = 0.125f;
	[SerializeField] private Vector3 offset = Vector2.zero;
	private Vector3 velocity = Vector3.one;

	// Update is called once per frame
	void LateUpdate()
	{
		Vector3 desiredPos = target.position + offset;
		Vector3 smoothedPos = Vector3.SmoothDamp(transform.position, desiredPos, ref velocity, smoothTime);
		transform.position = smoothedPos;
	}
}
