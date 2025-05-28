using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

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
