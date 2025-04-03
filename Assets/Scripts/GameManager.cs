using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // TextMeshPro için gerekli

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // Oyun durumu
    public int score = 0;
    public int lives = 3;
    public bool isGameOver = false;
    private int highScore = 0;

    // UI referansları
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public GameObject gameOverPanel;
    public TextMeshProUGUI gameOverScoreText;
    public TextMeshProUGUI highScoreText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadHighScore();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateUI();
    }

    public void AddScore(int points)
    {
        score += points;
        if (score < 0) score = 0;
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
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            if (gameOverScoreText != null)
                gameOverScoreText.text = "Skor: " + score.ToString();
            if (highScoreText != null)
                highScoreText.text = "High Score: " + highScore.ToString();
        }
    }

    public void RestartGame()
    {
        score = 0;
        lives = 3;
        isGameOver = false;
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
        UpdateUI();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
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