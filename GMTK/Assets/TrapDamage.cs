using System.Collections;
using System.Collections.Generic;
using UnityEditor.Compilation;
using UnityEngine;

public class TrapDamage : MonoBehaviour
{
    public Transform respawnPoint;
    public GameObject playerPrefab;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
        }

        RespawnPlayer();
        
    }

    private void RespawnPlayer()
    {
        GameObject player = Instantiate(Resources.Load<GameObject>("PlayerPrefab"), respawnPoint.position, Quaternion.identity);
    }
}
