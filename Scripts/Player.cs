using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public static Player play;
    private SpriteRenderer sr;
    private Animator anim;
    private Rigidbody2D rb;


    [Header("Ground_Check")]
    public Transform groundCheck;
    public LayerMask groundLayer;
    public bool isGrounded;


    [Header("Logic Dash")]
    [SerializeField] private bool canDash = true; //Posso usar o dash?
    [SerializeField] private bool isDashing; // Está dando dash
    [SerializeField] private float dashForce;
    [SerializeField] private float dashTime; // Duração do dash
    [SerializeField] private float dashColdown;
    [SerializeField] private TrailRenderer tr;


    [Header("Logic Jump")]
    [SerializeField] private float jumpForce;
    [SerializeField] bool isJumping;
    [SerializeField] public float jumpDuration;
    [SerializeField] float countJumpTime;


    [Header("Movimentação Player")]
    [SerializeField] private float speed;


    [Header("Life")]
    [SerializeField] private int vida;
    private int vidaMaxima = 3;


    [Header("KnokBack")]
    [SerializeField] private float kbForce; // Força do Knokback
    [HideInInspector] public bool isKB; // Verificar a posição do Knokback
    [HideInInspector] public float kbCount; // Contador knockback
    public float bktime;


    [Header("Invencibilidade")]
    public bool isInvencible;
    public float invencibleTime;
    [HideInInspector] public float invencibleCount;


    [Header("Skills")]
    public bool liberarJump;
    public bool liberarDash;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        tr = GetComponent<TrailRenderer>();

        countJumpTime = jumpDuration;

        vida = vidaMaxima;
        play = this;
    }
    void Update()
    {
        LogicGroundCheck();

    }
    void FixedUpdate()
    {
        KBLogic();
    }

    void Move()
    {
        if(isDashing)
        {
            return;
        }

        if(!isDashing)
        {
            //O input.getAxis vai retornar um numero entre -1 e 1
            // Se eu apertar a tecla pra direita ele retorna 1, e atualiza o vector2(a posição dele)
            if(Input.GetKey(KeyCode.LeftArrow))
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
                anim.SetBool("Run", true);
                sr.flipX = true;
            }
            else if(Input.GetKey(KeyCode.RightArrow))
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
                anim.SetBool("Run", true);
                sr.flipX = false;
            }
            else 
            {
                rb.velocity = new Vector2(0f, rb.velocity.y);
                anim.SetBool("Run", false);
            }
            if(liberarDash)
            {
                if(Input.GetKeyDown(KeyCode.LeftShift) && canDash)
                {
                    StartCoroutine(Dash());
                }
            }
        }
    }

    private IEnumerator Dash()
    {
        canDash = false; // Drurante o dash, não posso dar outro
        isDashing = true;
        float originalGravity = rb.gravityScale; // Salva o valor atual da gravidade do objeto para restaurá-lo após o dash.
        rb.gravityScale = 0f; // Zera a gravidade do personagem durante o dash para ele não cair

        if(sr.flipX == false) // Verifica se está olhando para a direita
        {
            rb.velocity = new Vector2(transform.localScale.x * dashForce, 0f); // Adiciona veloidade para a direção
        }
        else rb.velocity = new Vector2(transform.localScale.x * -dashForce, 0f);

        tr.emitting = true; // Ativa a trilha do dash

        yield return new WaitForSeconds(dashTime); // yield return faz o codigo esperar

        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;

        yield return new WaitForSeconds(dashColdown); // Espera o tempo para dar outro dash

        canDash = true;
    }
    void LogicGroundCheck()
    {
        isGrounded = Physics2D.OverlapCapsule(groundCheck.position, new Vector2(0.17f, 0.03f), CapsuleDirection2D.Horizontal, 0f, groundLayer);
        if(isGrounded)
        {
            anim.SetBool("Fall", false);
            anim.SetBool("Jump", false);
        }
        else
        {
            // Animação de "Fall" só ativa quando o personagem está em queda (velocidade Y negativa)
            if (rb.velocity.y < 0)
            {
                anim.SetBool("Fall", true);
                anim.SetBool("Jump", false);
            }
            // "Jump" ativa somente quando o personagem sobe (velocidade Y positiva)
            else if (rb.velocity.y > 0)
            {
                anim.SetBool("Jump", true);
                anim.SetBool("Fall", false);
            }
        }

    }

    void LogicJumpVariable()
    {
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            isJumping = true;
            anim.SetBool("Jump", true);
            rb.velocity = new Vector2(rb.velocity.x, 5f);
        }
        if(Input.GetButton("Jump") && isJumping)
        {
            if(countJumpTime > 0)
            {
                countJumpTime -= Time.deltaTime;
                anim.SetBool("Jump", true);
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
            else isJumping = false;
        }
        if(Input.GetButtonUp("Jump"))
        {
            isJumping = false;
            countJumpTime = jumpDuration;
        }
    }
    void Damage()
    {
        vida -= 1;
        HUD_Controler.hud.DesableLife(vida);

        if(vida <= 0)
        {
            vida = 0;
            HUD_Controler.hud.ActiveGameOver();
            Destroy(gameObject);
        }
    }

    public void TakeInvencible()
    {
        if(!isInvencible)
        {
            Damage();
            isInvencible = true;
            invencibleCount = invencibleTime;
        }
    }

    void KBLogic()
    {
        // Conta os segundos em negativo
        kbCount -= Time.deltaTime;
        invencibleCount -= Time.deltaTime;
        
        // Se o contador da invencibilidade for menor que zero, o boleano vira false
        if(invencibleCount <= 0)
        {
            isInvencible = false;
        }
        // Verifica se o contador é menor que zero
        if(kbCount < 0)
        {
            Move();
            if(liberarJump)
            {
                LogicJumpVariable();
            }
            anim.SetBool("Hit", false);
            isInvencible = false;
        }
        else
        {
            // Se não for menor e iskn for igual a true, joga o personagem para a esquerda
            if(isKB == true)
            {

                rb.velocity = new Vector2(-kbForce, kbForce);
                anim.SetBool("Hit", true);
            }
            // Se for false joga o personagem para a direta
            else
            {
                rb.velocity = new Vector2(kbForce, kbForce);
                anim.SetBool("Hit", true);
            }
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        //Layer 6 == Ground
        // Esquando eu estiver colidindo com o chão o jump é verdadeiro e eu posso pular
        /*
        if(collision.gameObject.layer == 6)
        {
            anim.SetBool("Fall", false)
            anim.SetBool("Jump", false)
            jump = true;
        }*/
        //Layer 8 == Trap
        if(collision.gameObject.layer == 8)
        {
            // Se eu colidir com um inimigo eu rodo o game over
            HUD_Controler.hud.ActiveGameOver();
            Destroy(gameObject);
        }
    }
}