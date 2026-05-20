using UnityEngine;

public class LinkedFinApp : MonoBehaviour
{
    [SerializeField] GameObject appCanvas;

    private PlayerMovement playerMovement;

    void Start()
    {
        playerMovement = FindFirstObjectByType<PlayerMovement>();
    }
    
    public void ShowApp()
    {
        appCanvas.SetActive(true);
        // TODO: Put this in a pause game helper script.
        // Same with DialogueManager.
        Time.timeScale = 0;
        playerMovement.enabled = false;
    }

    public void HideApp()
    {
        appCanvas.SetActive(false);
        Time.timeScale = 1;
        playerMovement.enabled = true;
    }
}
