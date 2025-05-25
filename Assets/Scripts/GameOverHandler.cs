using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverHandler : MonoBehaviour
{
    public static GameOverHandler Instance { get; private set; }

    [Header("UI Settings")]
    [Tooltip("�������� UI ������� Game Over")]
    public GameObject gameOverUI;
    [Tooltip("������ �����������")]
    public Button restartButton;
    [Tooltip("�������� ����� ������� UI")]
    public float delayBeforeShow = 0.5f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        Initialize();
    }

    private void Initialize()
    {
        if (gameOverUI == null)
        {
            Debug.LogError("GameOver UI �� ��������!", this);
            return;
        }

        gameOverUI.SetActive(false);

        if (restartButton != null)
        {
            restartButton.onClick.AddListener(RestartGame);
        }
        else
        {
            Debug.LogError("������ �������� �� ���������!", this);
        }
    }

    public void ShowGameOver()
    {
        if (gameOverUI == null) return;

        Time.timeScale = 0f;
        Invoke(nameof(ActivateUI), delayBeforeShow);
    }

    private void ActivateUI()
    {
        gameOverUI.SetActive(true);
    }

    private void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}