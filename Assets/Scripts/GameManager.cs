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
    public AudioClip menuSound;
    public AudioClip gameSound;
    public AudioClip goodObjectSound;
    public AudioClip badObjectSound;
    public AudioClip buttonSound;
    public GameObject titleScreen;
    public Button restartButton;
    public bool isGameActive;
    private int score;
    private int lives;
    private float spawnRate = 1.5f;

    void Start()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(menuSound);
    }

    void Update()
    {
        
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
        gameObject.GetComponent<AudioSource>().PlayOneShot(gameSound);
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
