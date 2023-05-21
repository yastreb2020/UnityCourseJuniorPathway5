using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI pausedText;
    public AudioClip menuSound;
    public AudioClip gameSound;
    public AudioClip goodObjectSound;
    public AudioClip badObjectSound;
    public AudioClip buttonSound;
    public GameObject titleScreen;
    public GameObject pauseScreen;
    public GameObject mouseTrail;
    public Button restartButton;
    public bool isGameActive;
    private bool isGamePaused;
    private int score;
    private int lives;
    private float spawnRate = 1.5f;
    
    private Trailing trailScript;

    void Start()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(menuSound);
        isGameActive = false;
        isGamePaused = false;
        
        trailScript = mouseTrail.GetComponent<Trailing>();
    }

    void Update()
    {
        if (isGameActive)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isGamePaused = !isGamePaused;
                if (!isGamePaused)
                {
                    pausedText.gameObject.SetActive(true);
                    pauseScreen.SetActive(true);
                    Time.timeScale = 0;
                }
                else
                {
                    pausedText.gameObject.SetActive(false);
                    pauseScreen.SetActive(false);
                    Time.timeScale = 1;
                }
            } else if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("mouse down");
                mouseTrail.SetActive(true);
                trailScript.OnActive();
                //mouseTrail.GetComponent<Trailing>().OnActive();
            } else if (Input.GetMouseButtonUp(0))
            {
                Debug.Log("mouse up");
                mouseTrail.SetActive(false);
            }
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        if (scoreToAdd > 0)
            gameObject.GetComponent<AudioSource>().PlayOneShot(goodObjectSound);
        else if (scoreToAdd < 0)
            gameObject.GetComponent<AudioSource>().PlayOneShot(badObjectSound);
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    private void GameOver()
    {
        restartButton.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void UpdateLives()
    {
        if (isGameActive)
        {
            lives -= 1;
            livesText.text = "Lives: " + lives;
            if (lives <= 0)
            {
                GameOver();
            }
        }
    }

    public void StartGame(int difficulty)
    {
        gameObject.GetComponent<AudioSource>().Stop();
        gameObject.GetComponent<AudioSource>().PlayOneShot(buttonSound);
        // make gameSound the clip for AudioSource component, so that it loops
        gameObject.GetComponent<AudioSource>().clip = gameSound;
        gameObject.GetComponent<AudioSource>().Play();
        isGameActive = true;
        spawnRate /= difficulty;
        StartCoroutine(SpawnTarget());
        score = 0;
        lives = 5;
        livesText.text = "Lives: " + lives;
        UpdateScore(0);
        titleScreen.gameObject.SetActive(false);
    }
}
