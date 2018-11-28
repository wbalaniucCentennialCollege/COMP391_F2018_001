using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// NEW USING STATEMENTS
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using UnityEngine.Audio;

// This is where our game logic will be run
public class GameController : MonoBehaviour {

    // Create waves of hazards
    [Header("Wave Settings")]   
    public GameObject hazard;   // What are we spawning?
    public Vector2 spawn;       // Where do we spawn our hazards?
    public int hazardCount;     // How many hazards per wave?
    public float startWait;     // How long until the first wave?
    public float spawnWait;     // How long between each hazard in each wave?
    public float waveWait;      // How long between each wave of hazards?

    [Header("UI Settings")]
    public Text scoreText;      // Reference to the Text component of the ScoreText UI object
    public Text gameOverText;   // Reference to the Text component of the GameOverText UI object
    public Text restartText;    // Reference to the Text component of the RestartText UI object

    [Header("Audio Settings")]
    public AudioMixerSnapshot gameAudioSnapshot;
    public AudioMixerSnapshot pauseAudioSnapshot;

    // Private variables
    private int score;
    private bool gameOver;
    private bool restart;

	// Use this for initialization
	void Start () {
        score = 0;
        UpdateScore();
        gameOver = false;
        restart = false;

        StartCoroutine(SpawnWaves()); // Runs a function separate from the rest of the code (in it's own thread)
	}

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        // Check whether you are restarting
        if(restart)
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                // Restart our game!
                // THE OLD WAY. DON'T USE THIS
                // Application.LoadLevel("Level1");
                // THE NEW WAY. USE THIS!
                // SceneManager.LoadScene("Level1"); // <-- Easy way of loading a specific scene
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Acces current active scene and returns the build index Number
            }
        }
    }

    // Function dedicated to spawning waves of hazards
    // Coroutine
    IEnumerator SpawnWaves()
    {
        // Delay until first wave appears
        yield return new WaitForSeconds(startWait); // Pause. This will "wait" for "startWait" seconds
        while(true)
        {
            // Spawning our hazards
            for(int i = 0; i < hazardCount; i++)
            {
                Vector2 spawnPosition = new Vector2(spawn.x, Random.Range(-spawn.y, spawn.y));
                //                                     11                   -4          4
                Instantiate(hazard, spawnPosition, Quaternion.identity);
                yield return new WaitForSeconds(spawnWait); // Wait time between spawning each hazard
            }
            yield return new WaitForSeconds(waveWait); // Delay between each wave of enemies

            if(gameOver)
            {// Start "restart" sequence
                // Activate the Restart UI text
                restartText.enabled = true;
                // (Optional) Set restart text
                // restartText.text = ""
                // Set restart boolean value to true
                restart = true;

                break; // Stops any more waves from being generated
            }
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        // Debug.Log("Score: " + score);
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        // What happens when my game is over?
        gameOver = true;

        gameOverText.enabled = true;
    }
}
