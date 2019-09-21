using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }

	public int EnemiesAlive { get; set; }
	public int EnemiesKilled { get; set; }

	[SerializeField] TextMeshProUGUI killCountText;

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
	}

    // Update is called once per frame
    void Update()
    {
		// Update the kill count text in the ui
		killCountText.text = "KILL COUNT: " + EnemiesKilled;
	}
}
