using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{

    public void StartGame()
    {
        // TODO: Add a fade to black
        SceneManager.LoadScene(1);
    }
}
