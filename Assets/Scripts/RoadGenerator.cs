using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    public Transform player;
    public GameObject roadSegmentPrefab;
    public GameObject sideBarrierPrefab;
    public float segmentLength = 5f;
    public int maxSegments = 5;
    public Sprite roadSprite;
    public float roadWidth = 20f;
    public float roadHeight = 5f;
    public float distanceBetweenBarriers = 16f;

    private float spawnZ;
    private Queue<GameObject> segments = new Queue<GameObject>();

    void Start()
    {
        if (player == null || roadSegmentPrefab == null || roadSprite == null || sideBarrierPrefab == null)
        {
            Debug.LogError("Player, roadSegmentPrefab, roadSprite или sideBarrierPrefab не назначены в RoadGenerator!");
            return;
        }
        spawnZ = player.position.y - (maxSegments * segmentLength) + segmentLength;
        for (int i = 0; i < maxSegments; i++)
        {
            SpawnSegment();
            Debug.Log("Сегмент создан на позиции Y: " + spawnZ);
        }
    }

    void Update()
    {
        if (player == null || segments.Count == 0) return;

        GameObject firstSegment = segments.Peek();
        if (player.position.y - segmentLength > firstSegment.transform.position.y)
        {
            Destroy(firstSegment);
            segments.Dequeue();
            SpawnSegment();
        }
    }

    void SpawnSegment()
    {
        if (roadSegmentPrefab != null)
        {
            Vector3 pos = new Vector3(0, spawnZ, 0);
            GameObject seg = Instantiate(roadSegmentPrefab, pos, Quaternion.identity, transform);
            SpriteRenderer sr = seg.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.sprite = roadSprite;
                sr.enabled = true;

                float spriteWidth = sr.sprite.bounds.size.x;
                float spriteHeight = sr.sprite.bounds.size.y;
                float scaleX = roadWidth / spriteWidth;
                float scaleY = roadHeight / spriteHeight;
                seg.transform.localScale = new Vector3(scaleX, scaleY, 1);

                SpawnSideBarriers(seg.transform);

                Debug.Log("Сегмент спавнился с размерами: " + roadWidth + "x" + roadHeight + " на Y: " + spawnZ);
            }
            else
            {
                Debug.LogError("Sprite Renderer отсутствует на сегменте!");
            }
            segments.Enqueue(seg);
            spawnZ += segmentLength;
        }
    }

    void SpawnSideBarriers(Transform roadTransform)
    {
        if (sideBarrierPrefab != null)
        {
            float halfBarrierWidth = (roadWidth - distanceBetweenBarriers) / 2;

            Vector3 leftPos = roadTransform.position + new Vector3(-halfBarrierWidth - 0.5f, 0, 0);
            GameObject leftBarrier = Instantiate(sideBarrierPrefab, leftPos, Quaternion.identity, roadTransform);
            leftBarrier.transform.localScale = new Vector3(1, roadHeight / 5f, 1);
            if (leftBarrier.tag != "SideBarrier")
            {
                Debug.LogWarning("Тег SideBarrier не установлен на префабе, устанавливаем вручную.");
                leftBarrier.tag = "SideBarrier";
            }

            Vector3 rightPos = roadTransform.position + new Vector3(halfBarrierWidth + 0.5f, 0, 0);
            GameObject rightBarrier = Instantiate(sideBarrierPrefab, rightPos, Quaternion.identity, roadTransform);
            rightBarrier.transform.localScale = new Vector3(1, roadHeight / 5f, 1);
            if (rightBarrier.tag != "SideBarrier")
            {
                Debug.LogWarning("Тег SideBarrier не установлен на префабе, устанавливаем вручную.");
                rightBarrier.tag = "SideBarrier";
            }
        }
    }
}