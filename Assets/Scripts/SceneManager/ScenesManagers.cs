using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManagers : MonoBehaviour
{
    public async void LoadScenes(string nameScene)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(nameScene, LoadSceneMode.Single);
        operation.allowSceneActivation = false;

        while (operation.progress < 0.9f)
        {
            Debug.Log("Carregando: " + (operation.progress * 100) + "%");
            Debug.Log(operation.progress);
            await Task.Yield(); // espera o próximo frame
        }

        await Task.Delay(2000);

        operation.allowSceneActivation = true;
    }
}
