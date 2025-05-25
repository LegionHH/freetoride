using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Game Settings")]
    public float obstacleSpeed = 5f; 

    [Header("UI Settings")]
    public GameObject gameOverPanel;
    public Button restartButton;
    public float delayBeforeShow = 0.5f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        if (restartButton != null) restartButton.onClick.AddListener(RestartGame);
        else Debug.LogError("Restart Button not assigned!", this);
    }

    public void ShowGameOver()
    {
        if (gameOverPanel == null) return;

        Time.timeScale = 0f;
        Invoke(nameof(ActivateUI), delayBeforeShow);
    }

    private void ActivateUI() => gameOverPanel.SetActive(true);

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}