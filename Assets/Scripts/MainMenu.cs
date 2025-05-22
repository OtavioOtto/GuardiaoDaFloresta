using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void GoToScene()
    {
        Debug.Log("Tentando carregar a cena...");
        SceneManager.LoadScene(1);  
    }

    public void QuitApp()
    {
        Debug.Log("Saindo do aplicativo...");
        Application.Quit();
    }
}
