using UnityEngine;

public class Lift : MonoBehaviour
{
    public Transform liftCollisionStop;
    public Transform liftPointA;
    public float moveSpeed = 2f;
    private bool isMovingUp = false;
    private bool isMovingDown = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Invoke("StartMovingUp", 1f); // Start moving up after 1 second
            Invoke("StartMovingDown", 4f); // Start moving down after 4 seconds
        }
        else if (collision.gameObject.CompareTag("LiftPointA"))
        {
            StopMoving();
        }
    }

    private void Update()
    {
        if (isMovingUp)
        {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
            if (transform.position.y >= liftCollisionStop.position.y)
            {
                isMovingUp = false;
            }
        }
        else if (isMovingDown)
        {
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
            if (transform.position.y <= liftPointA.position.y)
            {
                isMovingDown = false;
            }
        }
    }

    private void StartMovingUp()
    {
        isMovingUp = true;
    }

    private void StartMovingDown()
    {
        isMovingDown = true;
    }

    private void StopMoving()
    {
        isMovingUp = false;
        isMovingDown = false;
        // Additional code to handle lift stop behavior
        Debug.Log("Lift stopped");
    }
}
