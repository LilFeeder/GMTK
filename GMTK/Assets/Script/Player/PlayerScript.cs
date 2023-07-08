using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask groundLayer;

    public Rigidbody2D playerRb;
    private BoxCollider2D playerCollider;
    private float horizontal;

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
    }
}
