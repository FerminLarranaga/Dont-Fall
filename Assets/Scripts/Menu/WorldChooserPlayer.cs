using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldChooserPlayer : MonoBehaviour
{
    public float maxSpeed = 3f;
    public float speed = 75f;
    public float jumpPower = 7f;

    public AudioClip Jump;

    private Rigidbody2D rb2d;
    private Animator anim;
    private SpriteRenderer spr;
    private Vector2 respawnPoint, startScale;
    private AudioSource PlayerSound;

    private float offSetJumpTime = 0.2f, offSetGroundedTime = 0.2f;
    private float offSetJump, offSetGrounded = 0;
    private bool jump, doubleJump;
    private bool movement = true;
    private bool grounded;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
        respawnPoint = transform.position;
        PlayerSound = GetComponent<AudioSource>();
        startScale = transform.localScale;
    }

    void Update()
    {
        anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));
        anim.SetBool("Grounded", grounded);

        ManageJump();
    }

    private void ManageJump()
    {
        offSetJump -= Time.deltaTime;
        offSetGrounded -= Time.deltaTime;

        if (grounded)
        {
            offSetGrounded = offSetGroundedTime;
            doubleJump = true;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            offSetJump = offSetJumpTime;
        }

        if (offSetJump > 0)
        {
            if (offSetGrounded > 0)
            {
                offSetGrounded = 0;
                offSetJump = 0;
                jump = true;
                doubleJump = true;
            }
            else if (doubleJump)
            {
                offSetGrounded = 0;
                offSetJump = 0;
                jump = true;
                doubleJump = false;
            }
        }
    }

    void FixedUpdate()
    {
        ManageHorizontalMovement();

        ManageVerticalMovement();

        if (transform.position.y < -10)
        {
            transform.position = respawnPoint;
        }
    }

    private void ManageHorizontalMovement()
    {
        Vector3 fixedVelocity = rb2d.velocity;
        fixedVelocity.x *= 0.75f;

        if (grounded)
        {
            rb2d.velocity = fixedVelocity;
        }

        float h = Input.GetAxisRaw("Horizontal");

        if (!movement)
        {
            h = 0;
        }

        rb2d.AddForce(new Vector2(speed * h, 0));

        float limitedSpeed = Mathf.Clamp(rb2d.velocity.x, -maxSpeed, maxSpeed);
        rb2d.velocity = new Vector2(limitedSpeed, rb2d.velocity.y);

        if (h > 0.1)
        {
            transform.localScale = new Vector3(startScale.x, startScale.y, 1);
        }
        else if (h < -0.1)
        {
            transform.localScale = new Vector3(-startScale.x, startScale.y, 1);
        }
    }

    private void ManageVerticalMovement()
    {
        if (jump)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
            rb2d.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            jump = false;
            PlayerSound.clip = Jump;
            PlayerSound.Play();
        }
    }

    public void SetJump(bool jump)
    {
        this.jump = jump;
    }

    public void SetDoubleJump(bool doubleJump)
    {
        this.doubleJump = doubleJump;
    }

    public void SetGrounded(bool grounded)
    {
        this.grounded = grounded;
    }
}
