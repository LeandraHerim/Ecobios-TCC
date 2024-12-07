using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss_walk : StateMachineBehaviour
{
    Transform player;
    Rigidbody2D rb;
    public float speed = 2.5f;
    public GameObject bulletPrefab; // Prefab da bala
    public float bulletSpeed = 10f; // Velocidade da bala
    public float bulletOffset = 0.5f; // Distância para instanciar a bala à frente do player
    public float attackRange = 5f; // Alcance de ataque
    public float shootCooldown = 2f; // Intervalo entre disparos em segundos
    bool facingRight = true;

    private float nextShootTime; // Armazena o próximo tempo em que o vilão pode atirar
    Animator animatorRef;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        rb = animator.GetComponent<Rigidbody2D>();
        animatorRef = animator;

        if (player == null || rb == null)
        {
            Debug.LogError("Jogador ou Rigidbody2D do Boss não encontrado!");
        }

        nextShootTime = Time.time; // Permite que o vilão dispare imediatamente ao entrar no estado
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (player == null || rb == null) return;

        // Virar em direção ao jogador
        LookAtPlayer();

        // Movimentar-se na direção do jogador
        Vector2 target = new Vector2(player.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

        // Verifica a distância para atirar e o tempo de espera para o próximo disparo
        if (Vector2.Distance(player.position, rb.position) <= attackRange && Time.time >= nextShootTime)
        {
            Shoot();
            nextShootTime = Time.time + shootCooldown; // Atualiza o tempo para o próximo disparo
        }
        
    }

    void LookAtPlayer()
    {
        if (player == null) return;

        // Verifica a direção do jogador e ajusta o vilão
        if ((player.position.x > rb.position.x && !facingRight) || (player.position.x < rb.position.x && facingRight))
        {
            facingRight = !facingRight;
            Vector3 localScale = animatorRef.transform.localScale;
            localScale.x *= -1; // Inverte a escala no eixo X para virar o vilão
            animatorRef.transform.localScale = localScale;
        }
    }

    void Shoot()
    {
        Vector3 bulletSpawnPosition = animatorRef.transform.position + (facingRight ? Vector3.right : Vector3.left) * bulletOffset;

        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPosition, animatorRef.transform.rotation);

        Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();
        rbBullet.velocity = (facingRight ? Vector2.right : Vector2.left) * bulletSpeed;

        Debug.Log("Disparo realizado!");
    }

    
}
