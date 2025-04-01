using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager2 : MonoBehaviour
{
    public static GameManager2 Instance { get; private set; }

    // Oyun durumu
    public int score = 0;
    public int lives = 3;
    public bool isGameOver = false;

    // UI referansları (daha sonra eklenecek)
    public Text scoreText;
    public Text livesText;
    public GameObject gameOverPanel;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int points)
    {
        score += points;
        if (score < 0)
        {
            GameOver();
        }
        UpdateUI();
    }

    public void LoseLife()
    {
        lives--;
        if (lives <= 0)
        {
            GameOver();
        }
        UpdateUI();
    }

    private void GameOver()
    {
        isGameOver = true;
        // Oyun sonu panelini göster
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void UpdateUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Puan: " + score.ToString();
        }
        if (livesText != null)
        {
            livesText.text = "Can: " + lives.ToString();
        }
    }
} 