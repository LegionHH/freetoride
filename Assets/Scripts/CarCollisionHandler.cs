using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CarCollisionHandler : MonoBehaviour
{
    private bool isGameOver = false;
    private float totalScore = 0f;

    [Header("UI References")]
    public GameObject gameOverPanel;
    public Text scoreText;
    public Text finalScoreText;
    public Button restartButton;

    private CarController carController;

    void Start()
    {
        carController = GetComponent<CarController>();

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        if (restartButton != null)
            restartButton.onClick.AddListener(RestartGame);
    }

    void Update()
    {
        if (isGameOver) return;

        if (carController != null)
        {
            float speed = carController.GetForwardSpeed();
            totalScore += speed * Time.deltaTime;
        }

        if (scoreText != null)
            scoreText.text = "Score: " + Mathf.FloorToInt(totalScore);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isGameOver) return;
        if (collision.collider.CompareTag("Obstacle"))
            GameOver();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isGameOver) return;
        if (other.CompareTag("Obstacle"))
            GameOver();
    }

    void GameOver()
    {
        isGameOver = true;

        carController?.HandleCrash();

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        if (finalScoreText != null)
            finalScoreText.text = "Total Score: " + Mathf.FloorToInt(totalScore);
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
