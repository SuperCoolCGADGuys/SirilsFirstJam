using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
	[SerializeField] GameObject HUDObject;

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
	}

	private void OnDisable()
	{
		Time.timeScale = 1f;
		HUDObject.SetActive(true);
		GameManager.Instance.GameIsPaused = false;
	}
}
