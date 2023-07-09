using UnityEngine;

public class EndGameCollider : MonoBehaviour
{
    private WinUIManager gameManager;


    private void Start()
    {
        gameManager = FindObjectOfType<WinUIManager>();

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("touch");
            gameManager.AfterWinGame();
        }
    }
}
