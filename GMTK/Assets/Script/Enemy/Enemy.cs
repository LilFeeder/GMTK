using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float wallDetectionDistance = 5f;
    public float groundDetectionRadius = 0.2f;
    public float speed;
    public LayerMask wallLayer;
    public LayerMask groundLayer;
    public Transform groundDetectionPoint;
    private float jumpCooldown = 4f;
    private float jumpTimer = 0f;
    public Transform targetPosition;
    public Transform targetPosition2;

    private bool canMove = false;
    private bool movetTohitPlayer = false;
    private Rigidbody2D rb;
    public bool isJumping = false;
    private Transform player;
    private bool isFacingLeft;
    private bool hasReachedTarget = false;
    public Animator anima;
    public bool hitPlayer = false;
    public GameObject enemyToMove;
    public Transform respawnPoint;
    public Transform respawnPoint2;
    public Transform enemyPoint;
    public Transform enemyPoint2;
    private float delayTimer = 0f;
    public PlayerScript playerScript;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        StartCoroutine(StartMovingAfterDelay(1f));
        //targetPosition = GameObject.Find("TargetPosition").transform;
        //targetPosition2 = GameObject.Find("TargetPosition2").transform;

    }

    private void Update()
    {
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, wallDetectionDistance, wallLayer);
        RaycastHit2D groundHit = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, groundLayer);
        Vector2 raycastDirection = isFacingLeft ? Vector2.left : Vector2.right;
        RaycastHit2D wallHit = Physics2D.Raycast(transform.position, raycastDirection, wallDetectionDistance, wallLayer);


        Vector3 playerPosition = player.transform.position;

        if (playerPosition.x > transform.position.x)
        {
            isFacingLeft = false;
        }
        else
        {
            isFacingLeft = true;
        }
        flip();

        if (hasReachedTarget)
        {
            //float distanceToTarget = Vector2.Distance(transform.position, targetPosition.position);
            //if (distanceToTarget < 0.1f)
            //{
            Jump();
            //}
        }

        if (wallHit.collider != null && !isJumping)
        {
            Jump();
        }

        if (canMove)
        {
            if (player != null)
            {
                Vector2 direction = player.transform.position - transform.position;
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            }
        }
        //else
        //{
        //    AudioManager.Instance.StopCurrentSound();
        //}

        //if (IsGrounded() && !hasAutoJumped)
        //{
        //    hasAutoJumped = false;
        //}

        if (movetTohitPlayer)
        {
            delayTimer += Time.deltaTime;
            if (delayTimer >= 1f)
            {
                movetTohitPlayer = false;
                StartCoroutine(StartMovingAfterDelay(1f));
                delayTimer = 0f;
            }
        }
    }

    private void Jump()
    {
        Debug.Log("Hello");
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        //rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        isJumping = true;
        //ForceMode2D.Impulse

        //Collider2D groundCollider = Physics2D.OverlapCircle(groundDetectionPoint.position, groundDetectionRadius, groundLayer);
        //if (groundCollider != null)
        //{
        //    isJumping = false;
        //}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground") || collision.collider.CompareTag("Wall"))
        {
            isJumping = false;
            hasReachedTarget = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hasReachedTarget = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        hasReachedTarget = false;
    }

    private bool IsGrounded()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, groundDetectionRadius, groundLayer);
        return collider != null;
        //Collider2D groundCollider = Physics2D.OverlapCircle(groundDetectionPoint.position, groundDetectionRadius, groundLayer);
        //return groundCollider != null;
    }

    private void flip()
    {
        Vector3 localScale = transform.localScale;
        if (isFacingLeft)
        {
            localScale.x = -Mathf.Abs(localScale.x);
        }
        else
        {
            localScale.x = Mathf.Abs(localScale.x);
        }
        transform.localScale = localScale;
    }

    public void HitPlayer()
    {


        if (hitPlayer && playerScript.isChangeRespawn == false)
        {
            if (canMove)
            {
                player.transform.position = respawnPoint.position;
                enemyToMove.transform.position = enemyPoint.position;
                canMove = false;
                movetTohitPlayer = true;
                PlayerDie.count++;
            }

        }
        else if (hitPlayer && playerScript.isChangeRespawn == true)
        {
            if (canMove)
            {
                player.transform.position = respawnPoint2.position;
                enemyToMove.transform.position = enemyPoint2.position;
                canMove = false;
                movetTohitPlayer = true;
                PlayerDie.count++;
            }


        }

    }

    private IEnumerator StartMovingAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        canMove = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 raycastStart = transform.position;
        Vector3 raycastEnd;

        if (isFacingLeft)
        {
            raycastEnd = raycastStart - transform.right * wallDetectionDistance;
        }
        else
        {
            raycastEnd = raycastStart + transform.right * wallDetectionDistance;
        }

        Gizmos.DrawLine(raycastStart, raycastEnd);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundDetectionPoint.position, groundDetectionRadius);
    }

}