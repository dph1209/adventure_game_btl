using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private Animator animator;
    private SpriteRenderer sprite;
    [SerializeField] private LayerMask jumpableGround;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
	[SerializeField] private float jumpForce = 14f;

    public bool keepIdle = false;
    private enum movementState { idle, running, jumping, falling }

    [SerializeField] private AudioSource jumpSoundEffect;

	// Start is called before the first frame update
	private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
	    if (GameMenu.IsPause)
	    {
		    return;
	    }
	    
        if (keepIdle == true)
        {
            movementState state = movementState.idle;
            animator.SetInteger("state", (int)state);
            return;
        }

        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpSoundEffect.Play();
        }

        updateAnimationState();
    }

    private void updateAnimationState()
    {
        movementState state;
		if (dirX > 0f)
		{
			state = movementState.running;
			sprite.flipX = false;
		}
		else if (dirX < 0f)
		{
			state = movementState.running;
			sprite.flipX = true;
		}
		else
		{
			state = movementState.idle;
		}

        if(rb.velocity.y > .1f)
        {
            state = movementState.jumping;
        }
        else if(rb.velocity.y < -.1f)
        {
            state = movementState.falling;
        }

        animator.SetInteger("state", (int)state);
	}

    private bool isGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
