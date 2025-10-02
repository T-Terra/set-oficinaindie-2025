using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : ScenesManagers
{
    public void StartGame(string sceneName)
    {
        LoadScenes(sceneName);
    }

    public void ExitGame()
    {
        Debug.Log("Sair do jogo..."); // Isso só aparece no Editor
        Application.Quit(); // Funciona no build final
    }
}
