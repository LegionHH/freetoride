using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CarController : MonoBehaviour
{
    [Header("Forward Movement")]
    public float acceleration = 10f;    
    public float brakeDecel = 20f;    
    public float coastDecel = 5f;     
    public float maxForwardSpeed = 20f;    
    public float minForwardSpeed = 0f;     
    [Header("Lateral Movement")]
    public float sideSpeed = 8f;     
    public float laneWidth = 3f;     
    public int totalLanes = 5;      

    private Rigidbody2D rb;
    private float forwardSpeed = 0f;
    private bool isCrashed = false;

   
    private float maxXOffset => (totalLanes - 1) / 2f * laneWidth;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        transform.rotation = Quaternion.identity;
    }

    void Update()
    {
        if (isCrashed) return;

    
        float v = Input.GetAxisRaw("Vertical"); // +1 W/?, -1 S/?
        if (v > 0.1f) forwardSpeed += acceleration * Time.deltaTime;
        else if (v < -0.1f) forwardSpeed -= brakeDecel * Time.deltaTime;
        else forwardSpeed -= coastDecel * Time.deltaTime;

        forwardSpeed = Mathf.Clamp(forwardSpeed, minForwardSpeed, maxForwardSpeed);
    }
    public float GetForwardSpeed()
    {
        return forwardSpeed;
    }

    void FixedUpdate()
    {
        if (isCrashed)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        
        float h = Input.GetAxisRaw("Horizontal"); 
        Vector2 vel = new Vector2(h * sideSpeed, forwardSpeed);
        rb.velocity = vel;

     
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -maxXOffset, +maxXOffset);
        transform.position = pos;
    }

 
    public void HandleCrash()
    {
        if (isCrashed) return;
        isCrashed = true;
        rb.velocity = Vector2.zero;
    }
}
