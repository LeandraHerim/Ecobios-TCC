using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public ParticleSystem deathEffectFumalus; // Referência ao Particle System

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemy"))
        {
            Debug.Log("Colisão detectada com o inimigo!");
            boss.instance.vida--; // Reduz a vida do inimigo

            if (boss.instance.vida < 0)
            {
                Instantiate(deathEffectFumalus, transform.position, Quaternion.identity); // Efeito de derrota
                Destroy(collision.gameObject); // Destroi o inimigo
                SceneManager.LoadScene("WinScene"); 
               
            }

            Destroy(gameObject); // Destroi a bala
        }
        else if (collision.CompareTag("bulletEnemy"))
        {
            Destroy(collision.gameObject);
        }
    }

    void Update()
    {
        Destroy(gameObject, 3f); // Destruir a bala após 3 segundos
    }

}
