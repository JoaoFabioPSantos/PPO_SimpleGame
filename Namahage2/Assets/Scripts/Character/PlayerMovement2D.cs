using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2D : MonoBehaviour
{

    public static float move;
    [SerializeField] private float moveSpeed = 5f;

    float speed;

    private bool jumping;
    [SerializeField] private float jumpSpeed = 5f;

    [SerializeField] private bool isGrounded;
    public Transform feetPosition;
    public Vector2 sizeCapsule;
    [SerializeField] public float angleCapsule = -90f;
    public LayerMask whatIsGround;

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 3f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    public bool attackingBool; 

    Rigidbody2D rb;
    SpriteRenderer sprite;
    Animator animationPlayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animationPlayer = GetComponent<Animator>();

        speed = moveSpeed;
        sizeCapsule = new Vector2(0.356f, 0.007046581f); 

    }

    void Update()
    {
        //reconhecer chão
        //isGrounded = Physics2D.OverlapCircle(feetPosition.position, sizeRadius, whatIsGround);
        isGrounded = Physics2D.OverlapCapsule(feetPosition.position, sizeCapsule, CapsuleDirection2D.Horizontal, angleCapsule, whatIsGround);

        if (isDashing)
        {
            return;
        }

        //input de movimento horizontal
        move = Input.GetAxis("Horizontal");

        //input de pulo do personagem
        if (Input.GetButtonDown("Jump") && isGrounded || Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            jumping = true;
        }

        //input de ataque
        if (Input.GetButtonDown("Fire3"))
        {
            attackingBool = true;
            animationPlayer.SetBool("ATKPlayer", true);

        }

        //input do dash
        if(Input.GetKeyDown(KeyCode.K) && canDash)
        {
            if (!isDashing)
            {
                StartCoroutine(Dash());
            }
        }

        //inverte personagem
        if (move < 0)
        {
            sprite.flipX = true;

        }else if (move > 0)
        {
            sprite.flipX = false;   
        }
        
        //animação do player pulando
        if (isGrounded)
        {
            animationPlayer.SetBool("Jumping", false);
            animationPlayer.SetBool("Falling", false);

            //animação player andando
            if (move == 0 || rb.velocity.x == 0)
            {
                animationPlayer.SetBool("Walking", false);
            }
            else
            {
                animationPlayer.SetBool("Walking", true);
            }

        }
        else
        {
            animationPlayer.SetBool("Walking", false);

            if (rb.velocity.y > 0)
            {
                animationPlayer.SetBool("Jumping", true);
                animationPlayer.SetBool("Falling", false);
            }
            if (rb.velocity.y < 0)
            {
                animationPlayer.SetBool("Jumping", false);
                animationPlayer.SetBool("Falling", true);
            }
           
        }

        //para ignorar a colisão entre jogador e outros npcs
        
    }

    void FixedUpdate()
    {
        //movimentação do personagem
        rb.velocity = new Vector2(move * moveSpeed, rb.velocity.y);

        if (isDashing)
        {
            return;
        }

        //pulo do personagem
        if (jumping)
        {
            rb.velocity = Vector2.up * jumpSpeed;
            //rb.AddForce(new Vector2(0f, jumpSpeed), ForceMode2D.Impulse);

            jumping = false;
        }

    }

    IEnumerator Dash()
    {
        isDashing = true;
        moveSpeed *= dashingPower;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;

        animationPlayer.SetBool("Dashing", true);

        yield return new WaitForSeconds(dashingTime);

        rb.gravityScale = originalGravity;
        moveSpeed = speed;
        isDashing = false;

        animationPlayer.SetBool("Dashing", false);

        canDash = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    void EndAnimationATK()
    {
        animationPlayer.SetBool("ATKPlayer", false);
        attackingBool = false;
    }

}
