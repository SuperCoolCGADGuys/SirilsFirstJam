using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
	[SerializeField] GameObject HUDObject;

    public void ResumeButtonPressed()
	{
		gameObject.SetActive(false);
	}

	public void QuitButtonPressed()
	{
		Debug.Log("Quit!");
		Application.Quit();
	}

	private void OnEnable()
	{
		Time.timeScale = 0f;
		GameManager.Instance.GameIsPaused = true;
		HUDObject.SetActive(false);
	}

	private void OnDisable()
	{
		Time.timeScale = 1f;
		GameManager.Instance.GameIsPaused = false;
		HUDObject.SetActive(true);
	}
}
