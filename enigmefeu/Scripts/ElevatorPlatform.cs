using UnityEngine;

public class ElevatorPlatform : MonoBehaviour
{
    public float liftHeight = 10.0f;
    public float liftSpeed = 1.0f;  

    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private bool isActivated = false;

    void Start()
    {
        initialPosition = transform.position; 
        targetPosition = initialPosition + new Vector3(0, liftHeight, 0);
    }

    void Update()
    {
        if (isActivated)
        {
            
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, liftSpeed * Time.deltaTime);

           
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                isActivated = false;
            }
        }
    }

    public void ActivatePlatform()
    {
        Debug.Log("Platform activated!"); 
        isActivated = true;
    }
}
