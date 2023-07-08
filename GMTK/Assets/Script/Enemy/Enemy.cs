using UnityEngine;

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

    private Rigidbody2D rb;
    private bool isJumping = false;
    private Transform player;
    private bool isFacingLeft;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, wallDetectionDistance, wallLayer);
        RaycastHit2D groundHit = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, groundLayer);
        Vector2 raycastDirection = isFacingLeft ? Vector2.left : Vector2.right;
        RaycastHit2D wallHit = Physics2D.Raycast(transform.position, transform.right, wallDetectionDistance, wallLayer);

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

        if (groundHit.collider != null || wallHit.collider != null)
        {
            isJumping = false;
        }
        if (wallHit.collider != null)
        {
            Jump();
        }

        if (player != null)
        {
            Vector2 direction = player.transform.position - transform.position;
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }

    private void Jump()
    {
            rb.AddForce(Vector2.up * jumpForce);
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
        }
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
