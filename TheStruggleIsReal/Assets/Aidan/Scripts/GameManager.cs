using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }

	public int EnemiesAlive { get; set; }
	public int EnemiesKilled { get; set; }

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

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
