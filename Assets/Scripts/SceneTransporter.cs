using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransporter : MonoBehaviour
{
    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject shipCamera;
    [SerializeField] GameObject mountainCamera;

    public static SceneTransporter Instance;

    private PlayerMovement playerMovement;
    private GameObject player;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        playerMovement = FindFirstObjectByType<PlayerMovement>();
        player = playerMovement.gameObject;

        // -1.81, -0.76
    }

    public void TransportToShip()
    {
        mainCamera.SetActive(false);
        shipCamera.SetActive(true);
        player.transform.position = new Vector3(47f, 35f);
    }

    public void TransportToMountain()
    {
        mainCamera.SetActive(false);
        player.transform.position = new Vector3(-8.5f, 33.5f);
        mountainCamera.SetActive(true);
    }

    public void TransportFromShip()
    {
        shipCamera.SetActive(false);
        mainCamera.SetActive(true);
        player.transform.position = new Vector3(5.5f, 11.4f);
    }

    public void TransportFromMountain()
    {
        mountainCamera.SetActive(false);
        player.transform.position = new Vector3(-15f, 8f);
        mainCamera.SetActive(true);
    }

    public void GoToStartMenu()
    {
        SceneManager.LoadScene(0);
    }
}
