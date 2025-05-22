using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class FirstCutscene : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(GoToScene());
    }

    IEnumerator GoToScene() 
    {
        yield return new WaitForSeconds(80f);
        SceneManager.LoadScene(2);
    }
}
