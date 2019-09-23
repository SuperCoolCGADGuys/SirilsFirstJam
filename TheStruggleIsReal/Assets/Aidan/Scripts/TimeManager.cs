using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
	private PlayerControls playerControls = null;

	[SerializeField] private float slowDownFactor = 0.05f;
	[SerializeField] private float slowInTime = .5f;
	[SerializeField] private float slowOutTime = .5f;

	[Space]

	[SerializeField] private GameObject slowTimeBar = null;
	[SerializeField] private float timeUntilBarRunsOut = 5f;
	private float timeLeft = 0f;

	private bool slowMoEnabled = false;

	private void Awake()
	{
		playerControls = new PlayerControls();
		timeLeft = timeUntilBarRunsOut;
	}

	void DoSlowMotion()
	{
		if (Time.timeScale > slowDownFactor)
		{
			if (Time.timeScale - (1f / slowInTime) * Time.unscaledDeltaTime > 0)
			{
				Time.timeScale -= (1f / slowInTime) * Time.unscaledDeltaTime;
			}
		}
		else
		{
			Time.timeScale = slowDownFactor;
		}
		Time.fixedDeltaTime = Time.timeScale * .02f;

		timeLeft -= Time.unscaledDeltaTime;
		timeLeft = Mathf.Clamp(timeLeft, 0f, timeUntilBarRunsOut);
		slowTimeBar.transform.localScale = new Vector2(timeLeft / timeUntilBarRunsOut, slowTimeBar.transform.localScale.y);
	}

	void ReturnTimeToNormal()
	{
		Time.timeScale += (1f / slowOutTime) * Time.unscaledDeltaTime;
		Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);

		timeLeft += Time.unscaledDeltaTime;
		timeLeft = Mathf.Clamp(timeLeft, 0f, timeUntilBarRunsOut);
		slowTimeBar.transform.localScale = new Vector2(timeLeft / timeUntilBarRunsOut, slowTimeBar.transform.localScale.y);
	}

	private void Update()
	{
		// If the game is paused we don't want this code to run, so return
		if (GameManager.Instance.GameIsPaused)
		{
			return;
		}

		// If the bar runs out then turn off slowmo mode
		if (timeLeft <= 0)
		{
			slowMoEnabled = false;
		}

		// Slow down when there is juice left in the bar and turn return time to normal when the control button is let go
		if (slowMoEnabled && timeLeft > 0)
		{
			DoSlowMotion();
		}
		else
		{
			ReturnTimeToNormal();
		}
	}

	private void OnEnable()
	{
		playerControls.Gameplay.SlowDownTime.performed += ctx => slowMoEnabled = true;
		playerControls.Gameplay.SlowDownTime.canceled += ctx => slowMoEnabled = false;
		playerControls.Enable();
	}

	private void OnDisable()
	{
		playerControls.Gameplay.SlowDownTime.performed -= ctx => slowMoEnabled = true;
		playerControls.Gameplay.SlowDownTime.canceled -= ctx => slowMoEnabled = false;
		playerControls.Disable();
	}
}
