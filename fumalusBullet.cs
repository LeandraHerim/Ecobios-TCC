using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class fumalusBullet : MonoBehaviour
{
    public ParticleSystem deathEffect; // Referência ao Particle System

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Colisão detectada com o Player!");
            Life.instance.vida--; // Reduz a vida

            if (Life.instance.vida < 0)
            {
                Destroy(collision.gameObject); // Destroi o Player
                Instantiate(deathEffect, transform.position, Quaternion.identity); // Efeito de derrota
                SceneManager.LoadScene("LostScene"); // Troca de cena
            }
            Destroy(gameObject); // Destroi a bala
        }
        else if (collision.CompareTag("bulletPlayer"))
        {
            Destroy(collision.gameObject);
        }
    }

    void Update()
    {
        Destroy(gameObject, 3f); // Destruir a bala após 3 segundos
    }
    
}
