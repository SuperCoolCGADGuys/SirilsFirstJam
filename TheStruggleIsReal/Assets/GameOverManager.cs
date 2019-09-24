using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverManager : MonoBehaviour
{
	[SerializeField] GameObject HUDObject;
	[SerializeField] TextMeshProUGUI scoreText;
	[SerializeField] TextMeshProUGUI highScoreText;

	public void ResetButtonPressed()
	{
		gameObject.SetActive(false);
		GameManager.Instance.ResetGame();
	}

	public void QuitButtonPressed()
	{
		Debug.Log("Quit!");
	}

	private void OnEnable()
	{
		Time.timeScale = 0f;
		HUDObject.SetActive(false);
		GameManager.Instance.GameIsPaused = true;
		scoreText.text = "Score: " + GameManager.Instance.EnemiesKilled;
		highScoreText.text = "High Score: " + GameManager.Instance.HighScore;
	}

	private void OnDisable()
	{
		Time.timeScale = 1f;
		HUDObject.SetActive(true);
		GameManager.Instance.GameIsPaused = false;
	}
}
