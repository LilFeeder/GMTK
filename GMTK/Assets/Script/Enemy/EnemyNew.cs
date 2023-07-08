using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNew : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;

    public GameObject player;
    public float speed;
    public float distance;
    public bool isJumping=false;
    public Transform castPoint;
    public float agroRange;
    public bool isFacingLeft;
    public Transform Enemy;
    public float jumpForce;

    private Vector2 endPos;
    private Rigidbody2D rb;
    private BoxCollider2D playerCollider;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = player.transform.position - transform.position;
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

        if (isFacingLeft)
        {
            endPos = Enemy.position + (Vector3.right * agroRange);
            Debug.Log("is left");
        }
        else
        {
            endPos = Enemy.position + (Vector3.left * agroRange);
        }

        if (CanSeekPlayer(agroRange))
        {
            jump();
        }
    }

    public void jump()
    {
        if (isJumping)
        {
            RaycastHit2D hit = Physics2D.Raycast(castPoint.position, Vector2.down, distance);
            if (hit.collider != null && hit.collider.CompareTag("Wall") && isGrounded())
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                isJumping = false;
            }
        }
    }

    bool CanSeekPlayer(float distance)
    {
        bool val = false;
        float castDisk = distance;

        RaycastHit2D hit = Physics2D.Linecast(castPoint.position, endPos, 1 << LayerMask.NameToLayer("Action"));
        //Vector2 direction = endPos - (Vector2)castPoint.position;
        //RaycastHit2D hit = Physics2D.Raycast(castPoint.position, direction, distance, 1 << LayerMask.NameToLayer("Action"));

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Wall"))
            {
                val = true;
            }
            else
            {
                val = false;
            }
            Debug.DrawLine(castPoint.position, hit.point, Color.yellow);
        }
        else
        {
            Debug.DrawLine(castPoint.position, endPos, Color.blue);
        }
        return val;

    }

    private bool isGrounded()
    {
        RaycastHit2D hitGround = Physics2D.BoxCast(playerCollider.bounds.center, playerCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return hitGround.collider != null;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Wall"))
        {
            isJumping = true;
        }
    }
}
