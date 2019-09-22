using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
	[SerializeField] GameObject HUDObject;

	private void Start()
	{
		Time.timeScale = 0f;
		GameManager.Instance.GameIsPaused = true;
	}

	public void PlayButtonPressed()
	{
		gameObject.SetActive(false);
	}

	public void QuitButtonPressed()
	{
		Debug.Log("Quit!");
	}

	private void OnDisable()
	{
		Time.timeScale = 1f;
		HUDObject.SetActive(true);
		GameManager.Instance.GameIsPaused = false;
	}
}
