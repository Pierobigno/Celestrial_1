using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LucianJump : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private LucianGroundDetection lucianGroundDetection;
    private PlayerInput input;
    private LucianMovement lucianMovement;
    public GameObject doubleJumpShockWave;

    [Header("Gestion du timer pour saut plus ou moins haut")]
    public bool isJumping = false;
    public float jumpingTimer;
    public float jumpingTimerLimit;
    public float jumpForce;
    private bool timerOn = false;

    [Header("Gestion du double saut")]
    public float doubleJumpForce;
    public bool canJump;
    public bool isDoubleJumping;

    [Header("Gestion de la chute après le saut")]
    public bool isFallingAfterJump;

    void Awake()
    {
        lucianGroundDetection = GetComponent<LucianGroundDetection>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        lucianMovement = FindObjectOfType<LucianMovement>();
    }


    void Start()
    {
        input = new PlayerInput();
        input.Lucian.Enable();
        input.Lucian.Jump.performed += x => Jump_performed();
        input.Lucian.Jump.canceled += x => Jump_canceled();
        jumpingTimer = 0;
    } 

    void Update()
    {
        if(isJumping && !isFallingAfterJump && canJump)
        {
            rb.velocity = new Vector2 (rb.velocity.x, jumpForce * jumpingTimer);
        }

        // Gestion du timer pour saut plus ou moins haut
        if(timerOn)
        {
            jumpingTimer += Time.deltaTime;
        }

        if(jumpingTimer > jumpingTimerLimit)
        {
            PlayerJumps();
        }

        if(lucianGroundDetection.isGrounded)
        {
            isDoubleJumping = false;
            isFallingAfterJump = false;
            
            if(lucianGroundDetection.isGrounded)
            {
                canJump = true;
            }
        }
    }

    void Jump_performed()
    {
        PlayerPreparesJump();
    }

    void Jump_canceled()
    {
        PlayerJumps();
    }

    void PlayerPreparesJump()
    {
        timerOn = true;
        isJumping = true;
        animator.SetTrigger("isPreparingToJump");

            if(!lucianGroundDetection.isGrounded && canJump)
            {
                StartCoroutine(DoubleJump());
            }
    }

    void PlayerJumps()
    {
        timerOn = false;
        isJumping = false;
        animator.SetTrigger("isJumping");
        isFallingAfterJump = true;
        jumpingTimer = 0;
    }


    IEnumerator DoubleJump()
    {
        animator.SetTrigger("isDoubleJumping");
        Instantiate(doubleJumpShockWave, lucianGroundDetection.groundPoint.position, lucianGroundDetection.groundPoint.rotation);
        rb.velocity = new Vector2 (rb.velocity.x, doubleJumpForce);
        isDoubleJumping = true;
        canJump = false;
        isJumping = false;
        yield return new WaitForSeconds(0.5f);
        isDoubleJumping = false;
    }
}
