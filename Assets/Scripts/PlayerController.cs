using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField] private GameObject jumpEffect;

    [Header("Components")]
    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] private Transform mouthAndEyeTransform;
    [SerializeField] private Animator eyeAnimator;
    [SerializeField] private Animator mouthAnimator;
    [SerializeField] private Color hitColor;

    [Header("LayerMasks")]
    [SerializeField] private LayerMask whatIsGround;

    private ParticleSystem particleSystem;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidbody2D;
    private Transform transform;
    private Controls controls;

    public bool isGroving;
    public bool isMoving;
    public bool isGround;
    public bool isDead;

    public int nubmerOfStars;
    public float startPlayerHealth;

    [Header("Parameters")]
    [Range(0, 100)] public float playerHealth;
    [Range(0, 10)] [SerializeField] private float playerRadius;
    [Range(0, 10)] [SerializeField] private float checkRadius;
    [Range(0, 10)] [SerializeField] private float jumpForce;
    [Range(0, 10)] [SerializeField] private float fallMultiplier;
    [Range(0, 10)] [SerializeField] private float growSpeed;
    [Range(0, 1000)] [SerializeField] private float rotateSpeed;
    [Range(0, 1000)] [SerializeField] private float speed;
    [Range(0, 100)] [SerializeField] private float hitMultiplicator;
    [Range(0, 100)] [SerializeField] private float enemyDamage;

    [Header("Limitations")]
    [Range(0, 1000)] [SerializeField] private float maxRotationSpeed;
    [Range(0, 1000)] [SerializeField] private float maxSpeed;
    [Range(0, 10)] [SerializeField] private float maxSpeedToResize;
    [Range(0, 10)] [SerializeField] private float minScale;
    [Range(0, 10)] [SerializeField] private float maxScale;

    private void Awake()
    {
        controls = new Controls();
        controls.Player.Jump.performed += context => Jump();
    }
    private void Start()
    {
        startPlayerHealth = playerHealth;

        particleSystem = groundCheckTransform.GetComponent<ParticleSystem>();
      
        spriteRenderer = GetComponent<SpriteRenderer>();
        transform = GetComponent<Transform>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        PlayerDead();
        FollowToPlayer();
    }
    private void FixedUpdate()
    {
        IsGround();
        IsMoving();
        Move();
        Shrink();
        Grow();
    }
    private void FollowToPlayer()
    {
        groundCheckTransform.position = transform.position - new Vector3(0, playerRadius, 0);
        mouthAndEyeTransform.position = transform.position;
        mouthAndEyeTransform.rotation = transform.rotation;
    }
    private void Jump()
    {
        if (isGround && !isDead)
        {
            rigidbody2D.AddForce(new Vector2(0, 5 * jumpForce),ForceMode2D.Impulse);
            Instantiate(jumpEffect,new Vector2(transform.position.x, transform.position.y - playerRadius),Quaternion.identity);
        }
    }
    private void Move()
    {
        float moveInput = controls.Player.Move.ReadValue<float>();

        if (Math.Abs(rigidbody2D.angularVelocity) < maxRotationSpeed && !isDead)
        {
            rigidbody2D.AddTorque(moveInput * -rotateSpeed );
        }

        if (rigidbody2D.velocity.magnitude < maxSpeed && !isDead)
        {
            rigidbody2D.AddForce(new Vector2(moveInput * speed, 0));
        }

    }
    private void Grow()
    {
        if (isGroving && transform.localScale.x <= maxScale)
        {
            Vector3 scale = new Vector3(transform.localScale.x + Time.fixedDeltaTime * 0.01f * growSpeed, transform.localScale.y + Time.fixedDeltaTime * 0.01f * growSpeed, 1);
            transform.localScale = scale;

            playerRadius += Time.fixedDeltaTime * 0.02f * growSpeed; 
        }
    }
    private void PlayerDead()
    {
        if (isDead)
        {
            eyeAnimator.SetBool("isDead", true);
            mouthAnimator.SetBool("isDead", true);
        }
    }
    private void Shrink()
    {
        if (isMoving && transform.localScale.x >= minScale && isGround)
        {
            Vector3 scale = new Vector3(transform.localScale.x - Time.fixedDeltaTime * 0.01f, transform.localScale.y - Time.fixedDeltaTime * 0.01f, 1);
            transform.localScale = scale;

            playerRadius -= Time.fixedDeltaTime * 0.02f;

            particleSystem.Play();
        }
    }
    private void ReturnDefaultColor()
    {
        spriteRenderer.color = Color.white;
    }
    private void IsGround()
    {
        isGround = Physics2D.OverlapCircle(groundCheckTransform.position, checkRadius, whatIsGround);
    }
    private void IsMoving()
    {
        isMoving = rigidbody2D.velocity.magnitude > maxSpeedToResize * 5 ? true : false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            Vector2 enemyForce = new Vector2(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y) - collision.GetContact(0).point;

            if(playerHealth >= 0) playerHealth -= enemyDamage;

            rigidbody2D.AddForce(-hitMultiplicator * (enemyForce + Vector2.down * 1.5f), ForceMode2D.Impulse);

            spriteRenderer.color = hitColor;
            Invoke("ReturnDefaultColor", 0.1f);

            if (playerHealth <= 0) isDead = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Resizer")) isGroving = true;

        if (collision.CompareTag("Star"))
        {
            nubmerOfStars++;
            Destroy(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Resizer")) isGroving = false;
    }
    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
    }
}
