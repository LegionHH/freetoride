using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class SideBarrier : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        CarController car = collision.gameObject.GetComponent<CarController>();
        if (car != null) car.HandleCrash();

        if (GameManager.Instance != null)
            GameManager.Instance.ShowGameOver();
        else
            Debug.LogError("GameManager Instance not found!");
    }
}