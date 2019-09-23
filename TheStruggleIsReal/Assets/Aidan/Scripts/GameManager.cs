﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }
	private PlayerControls playerControls = null;

	public int EnemiesAlive { get; set; }
	public int EnemiesKilled { get; set; }

    public int PlayerBulletsAlive { get; set; }

    [SerializeField] TextMeshProUGUI killCountText = null;
	[SerializeField] GameObject pauseMenuObject = null;
	[SerializeField] GameObject gameOverObject = null;

	public bool GameIsPaused { set; get; }

	void Awake()
	{
		// Makes sure that this is the only instance of this object in the scene
		if (Instance != null)
		{
			if (Instance != this)
			{
				Destroy(this.gameObject);
			}
		}
		else
		{
			Instance = this;
			DontDestroyOnLoad(this);
		}

		// Set up input
		playerControls = new PlayerControls();
	}

	// Update is called once per frame
	void Update()
	{
		// Update the kill count text in the ui
		killCountText.text = "KILL COUNT: " + EnemiesKilled;
	}

	private void TogglePause()
	{
		// Checks if the pause menu is active or not and does the opposite
		pauseMenuObject.SetActive(!pauseMenuObject.activeSelf);
	}

	public void ResetGame()
	{
		gameOverObject.SetActive(false);
		EnemiesKilled = 0;
	}

	public void GameOver()
	{
		// Checks if the game over screen is active or not and does the opposite
		gameOverObject.SetActive(true);
	}

	private void OnEnable()
	{
		playerControls.Gameplay.Pause.performed += ctx => TogglePause();
		playerControls.Enable();
	}

	private void OnDisable()
	{
		playerControls.Gameplay.Pause.performed -= ctx => TogglePause();
		playerControls.Disable();
	}
}
