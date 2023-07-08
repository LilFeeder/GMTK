using UnityEngine;

public class PlayerCheckpoint : MonoBehaviour
{
    public Transform checkpoint;
    public GameObject enemy;
    public float delayTime = 2f;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Touch");
            Invoke("MoveEnemyToCheckpoint", delayTime);
        }
    }

    private void MoveEnemyToCheckpoint()
    {
        enemy.transform.position = checkpoint.position;
    }
}
