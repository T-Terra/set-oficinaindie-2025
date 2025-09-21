using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
        Debug.Log("Sair do jogo..."); // Isso só aparece no Editor
        Application.Quit(); // Funciona no build final
    }
}
