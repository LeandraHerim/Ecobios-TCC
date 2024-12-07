using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class boss : MonoBehaviour
{
    public Transform player;
    public bool isFlipped = false;

    public static boss instance; // Instância singleton

    public int vida;
    public int vidaMaxima;
    public Image[] coracaoFumalus;
    public Sprite cheio;
    public Sprite vazio;

    void Awake()
    {
        if (instance == null)
        {
            instance = this; // Define a instância única
             Debug.Log("Life inicializado com sucesso!"); // Confirmação de inicialização
        }
        else
        {
            Destroy(gameObject); // Garante que apenas uma instância de Life exista
        }
    }
    // Start is called before the first frame update
    public void LookAtplayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if(transform.position.x > player.position.x && isFlipped){
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }else if(transform.position.x < player.position.x && !isFlipped){
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
        
    }
    void Update()
    {
        HealthLogic();
    }

    void HealthLogic()
    {
        if (vida > vidaMaxima)
        {
            vida = vidaMaxima;
        }

        for (int i = 0; i < coracaoFumalus.Length; i++)
        {
            coracaoFumalus[i].sprite = i < vida ? cheio : vazio;
            coracaoFumalus[i].enabled = i < vidaMaxima;
        }
    }

    
}
