using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; 
    public float smoothSpeed = 0.125f; 
    public Vector3 offset; 
    private float fixedXPosition; 

    void Start()
    {
        if (target == null)
        {
            Debug.LogError("Target не назначен в CameraFollow!");
            return;
        }
       
        fixedXPosition = transform.position.x;
       
        offset = new Vector3(0, transform.position.y - target.position.y, transform.position.z - target.position.z);
    }

    void LateUpdate()
    {
        if (target != null)
        {
         
            Vector3 desiredPosition = transform.position;
            desiredPosition.y = target.position.y + offset.y;
            desiredPosition.x = fixedXPosition;

          
            Vector3 smoothedPosition = transform.position;
            smoothedPosition.y = Mathf.Lerp(transform.position.y, desiredPosition.y, smoothSpeed);
            smoothedPosition.x = fixedXPosition;
            transform.position = smoothedPosition;

          
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }
}