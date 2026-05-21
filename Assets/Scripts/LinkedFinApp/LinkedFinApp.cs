using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LinkedFinApp : MonoBehaviour
{
    [SerializeField] GameObject appCanvas;
    [SerializeField] TMP_Text yourConnectionsText;
    [SerializeField] Transform connectionsListParent;
    [SerializeField] GameObject connectionProfilePrefab;

    private readonly List<ConnectionSO> connections = new List<ConnectionSO>();

    private PlayerMovement playerMovement;

    public static LinkedFinApp Instance;
    public bool isAvailable;

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
    }

    void OnEnable()
    {
        Actions.OnConnectionMade += AddConnection;
        Actions.ToggleFinApp += ToggleApp;
    }

    void OnDisable()
    {
        Actions.OnConnectionMade -= AddConnection;
        Actions.ToggleFinApp += ToggleApp;
    }

    public void ToggleApp()
    {
        if (isAvailable)
        {
            if (appCanvas.activeSelf)
                HideApp();
            else
                ShowApp();
        }
    }
    
    public void ShowApp()
    {
        InitializeAppUI();
        appCanvas.SetActive(true);
        // TODO: Put this in a pause game helper script.
        // Same with DialogueManager.
        Time.timeScale = 0;
        playerMovement.isPaused = true;
    }

    public void HideApp()
    {
        appCanvas.SetActive(false);
        Time.timeScale = 1;
        playerMovement.isPaused = false;
    }

    public void InitializeAppUI()
    {
        yourConnectionsText.text = "Your Connections (" + connections.Count + ")";
    }

    public void AddConnection(ConnectionSO connectionSO)
    {
        if (!connections.Contains(connectionSO))
        {
            connections.Add(connectionSO);
            GameObject newConn = Instantiate(connectionProfilePrefab, connectionsListParent);
            newConn.GetComponent<ConnectionProfile>().SetInfo(connectionSO);
        }
    }

    public List<ConnectionSO> GetConnections()
    {
        return connections;
    }
}
