using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mudaCena : MonoBehaviour
{
    // Função para carregar a nova cena
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
