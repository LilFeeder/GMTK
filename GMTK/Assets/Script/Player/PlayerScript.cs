using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float climbSpeed;

    public Rigidbody2D playerRb;
    private BoxCollider2D playerCollider;
    private float horizontal;
    private float verticle;
    private bool isClimbing;
    private bool isLadder;
    public bool isChangeRespawn = false;

    // Start is called before the first frame update
    void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        walk();
    }

    private bool isGrounded()
    {
        RaycastHit2D hitGround = Physics2D.BoxCast(playerCollider.bounds.center, playerCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return hitGround.collider != null;
    }

    private void walk()
    {
        playerRb.velocity = new Vector2(horizontal * speed, playerRb.velocity.y);

        if (horizontal > 0f)
        {
            transform.localScale = new Vector2(1, 1);
        }
        else
        {
            transform.localScale = new Vector2(-1, 1);
        }

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            //AudioManager.Instance.PlayJumpSound();
            playerRb.velocity = new Vector2(playerRb.velocity.x, jumpForce);
        }

        verticle = Input.GetAxis("Vertical");

        if(isLadder && (verticle > 0f || verticle < 0f) && isGrounded())
        {
            isClimbing = true;
        }
        else if(isLadder && isGrounded())
        {
            isClimbing = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = true;
        }

        if (collision.gameObject.CompareTag("Respawn"))
        {
            isChangeRespawn = true;
            Debug.Log("got touch respwan");
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = false;
            isClimbing = false;
        }
    }

    private void FixedUpdate()
    {
        if(isClimbing)
        {
            playerRb.gravityScale = 0f;
            playerRb.velocity = new Vector2(playerRb.velocity.x, verticle * 3f);
        }
        else
            playerRb.gravityScale = 1f;

    }

    


}
