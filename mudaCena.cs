using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mudaCenaMenu : MonoBehaviour
{
    // Função para carregar a nova cena
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
