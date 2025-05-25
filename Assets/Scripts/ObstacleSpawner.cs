using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Настройки")]
    public float moveSpeed = 5f;
    public float spawnDistance = 500f;
    public float destroyDistance = 200f;
    public float laneWidth = 150f;
    public int lanes = 3;

    [Header("Точки спавна (Empty Objects)")]
    public Transform[] spawnPoints; 

    [Header("Препятствия (Images)")]
    public Image[] obstacleImages; 

    private RectTransform player;
    private List<Obstacle> activeObstacles = new List<Obstacle>();

    private class Obstacle
    {
        public Image image;
        public float speed;
        public int currentLane;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<RectTransform>();


        foreach (Image img in obstacleImages)
        {
            if (img == null) continue;

            Obstacle newObstacle = new Obstacle()
            {
                image = img,
                speed = moveSpeed * Random.Range(0.8f, 1.2f),
                currentLane = Random.Range(0, lanes)
            };
            activeObstacles.Add(newObstacle);
            ResetObstacle(newObstacle);
        }
    }

    void Update()
    {
        if (player == null) return;

     
        UpdateSpawnPoints();

   
        foreach (Obstacle obstacle in activeObstacles)
        {
            if (obstacle.image == null) continue;

            obstacle.image.rectTransform.anchoredPosition -= Vector2.up * obstacle.speed * Time.deltaTime;

            
            if (obstacle.image.rectTransform.anchoredPosition.y < player.anchoredPosition.y - destroyDistance)
            {
                ResetObstacle(obstacle);
            }
        }
    }

    void UpdateSpawnPoints()
    {
        if (spawnPoints == null) return;

        for (int i = 0; i < spawnPoints.Length && i < lanes; i++)
        {
            if (spawnPoints[i] == null) continue;

       
            spawnPoints[i].position = new Vector3(
                (i - (lanes - 1) / 2f) * laneWidth,
                player.position.y + spawnDistance,
                0
            );
        }
    }

    void ResetObstacle(Obstacle obstacle)
    {
        if (obstacle.image == null || spawnPoints == null || spawnPoints.Length == 0) return;

        obstacle.currentLane = Random.Range(0, spawnPoints.Length);

        if (spawnPoints[obstacle.currentLane] != null)
        {
        
            obstacle.image.rectTransform.position = spawnPoints[obstacle.currentLane].position;
            obstacle.speed = moveSpeed * Random.Range(0.8f, 1.2f);
        }
    }
}