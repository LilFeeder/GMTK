using System.Collections;
using System.Collections.Generic;
using UnityEditor.Compilation;
using UnityEngine;

public class TrapDamage : MonoBehaviour
{
    private PlayerScript playerScript;
    public Transform respawnPoint;
    public Transform respawnPoint2  ;
    public Transform enemyPoint;
    public Transform enemyPoint2;
    public GameObject enemyToMove;


    private void Start()
    {
        playerScript = GetComponent<PlayerScript>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            MovePlayerPosition(collision.gameObject);
            MoveEnemyPosition();
        }
        if (collision.gameObject.CompareTag("enemy"))
        {
            MoveEnemyPosition();
        }

    }


    private void MovePlayerPosition(GameObject player)
    {
        if (playerScript.isChangeRespawn == true)
        {
            player.transform.position = respawnPoint2.position;
        }
        else
        {
            player.transform.position = respawnPoint.position;
        }
    }

    private void MoveEnemyPosition()
    {
        if (playerScript.isChangeRespawn == true)
        {
            enemyToMove.transform.position = enemyPoint2.position;
        }
        else
        {
            enemyToMove.transform.position = enemyPoint.position;
        }
    }
}
