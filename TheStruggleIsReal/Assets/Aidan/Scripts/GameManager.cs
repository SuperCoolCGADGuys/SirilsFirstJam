using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }
	private PlayerControls playerControls = null;

	public int EnemiesAlive { get; set; }
	public int EnemiesKilled { get; set; }
	public int HighScore { get; set; }
	public bool GameIsPaused { set; get; }
    public int PlayerBulletsAlive { get; set; }

    [SerializeField] private TextMeshProUGUI killCountText = null;
	[SerializeField] private GameObject pauseMenuObject = null;
	[SerializeField] private GameObject gameOverObject = null;
	[SerializeField] private GameObject mainMenuObject = null;
	[SerializeField] private PlayerHealthManager playerHealthManager = null;
	[SerializeField] private EnemySpawner enemySpawner = null;


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
		}

		// Set up input
		playerControls = new PlayerControls();

		Debug.Log("GameManager awake!");
	}

	// Update is called once per frame
	void Update()
	{
		// Update the kill count text in the ui
		killCountText.text = "KILL COUNT: " + EnemiesKilled;

		if (EnemiesKilled > HighScore)
		{
			HighScore = EnemiesKilled;
		}
	}

	private void TogglePause()
	{
		// Checks if the pause menu is active or not and does the opposite
		pauseMenuObject.SetActive(!pauseMenuObject.activeSelf);
	}

	public void ResetGame()
	{
		playerHealthManager.ResetPlayer();
		enemySpawner.ResetSpawner();
		EnemiesKilled = 0;
		EnemiesAlive = 0;
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

		Debug.Log("GameManager enabled!");
	}

	private void OnDisable()
	{
		playerControls.Gameplay.Pause.performed -= ctx => TogglePause();
		playerControls.Disable();

		Debug.Log("GameManager disabled!");
	}
}
