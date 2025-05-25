using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public Text SpeedText;
    public Text ScoreText;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void UpdateSpeedAndScore(float speed, float score)
    {
        if (SpeedText != null && ScoreText != null)
        {
            SpeedText.text = $"Скорость: {speed:F1}";
            ScoreText.text = $"Очки: {Mathf.FloorToInt(score)}";
        }
    }
}