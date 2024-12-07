using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class personagem : MonoBehaviour
{
    public float velocidade;
    public float jumpforce;
    private Rigidbody2D rig;
    public bool isJumping;
    public bool doubleJump;
    public GameObject bulletPrefab; // Prefab da bala
    public float bulletSpeed = 10f; // Velocidade da bala
    public float bulletOffset = 0.5f; // Distância para instanciar a bala à frente do player
    public bool facingRight = true; // Indica se o personagem está virado para a direita
    private Animator anim;
    // Variável que armazena o próximo momento em que é permitido atirar
    private float nextShootTime = 0f;
    // Intervalo de tempo entre disparos (em segundos)
    public float shootCooldown = 1.5f;
   

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();

        if (Input.GetKeyDown("z"))
        {
            Shoot();
        }

        
    }

    void Move()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * velocidade;

        if(Input.GetAxis("Horizontal") > 0f){
            anim.SetBool("Walk", true);
            facingRight = true;
            transform.eulerAngles = new Vector3 (0f, 0f, 0f);
        }
        if(Input.GetAxis("Horizontal") < 0f){
            anim.SetBool("Walk", true);
            facingRight = false;
            transform.eulerAngles = new Vector3 (0f, 180f, 0f);
        }
        if(Input.GetAxis("Horizontal") == 0f){
            anim.SetBool("Walk", false);
            
        }
        
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (!isJumping)
            {
                rig.AddForce(new Vector2(0f, jumpforce), ForceMode2D.Impulse);
                doubleJump = true;
                anim.SetBool("Pulo", true);
                isJumping = true;
            }
            else
            {
                if (doubleJump)
                {
                    rig.AddForce(new Vector2(0f, jumpforce), ForceMode2D.Impulse);
                    doubleJump = false;
                }
            }
        }
    }

    void Shoot(){
        // Verifica se o tempo atual é maior ou igual ao próximo momento permitido para atirar
        if (Time.time >= nextShootTime){
            // Calcular a posição de disparo à frente do player
            Vector3 bulletSpawnPosition = transform.position + (facingRight ? Vector3.right : Vector3.left) * bulletOffset;

            // Instanciar a bala na posição calculada
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPosition, transform.rotation);

            // Definir a velocidade da bala na direção correta
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = (facingRight ? Vector2.right : Vector2.left) * bulletSpeed;

            // Atualizar o tempo para o próximo disparo permitido
            nextShootTime = Time.time + shootCooldown;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isJumping = false;
            doubleJump = false; 
            anim.SetBool("Pulo", false);
        }
        
    }

    void Flip()
    {
        // Inverter a direção do personagem
        facingRight = !facingRight;

        // Multiplicar a escala x por -1 para virar o personagem
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
