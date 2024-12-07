using UnityEngine;
using UnityEngine.UI;

public class Life : MonoBehaviour
{
    public static Life instance; // Instância singleton

    public int vida;
    public int vidaMaxima;
    public Image[] coracao;
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

        for (int i = 0; i < coracao.Length; i++)
        {
            coracao[i].sprite = i < vida ? cheio : vazio;
            coracao[i].enabled = i < vidaMaxima;
        }
    }
}
