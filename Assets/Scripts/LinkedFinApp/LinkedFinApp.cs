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
    }

    void OnDisable()
    {
        Actions.OnConnectionMade -= AddConnection;
    }
    
    public void ShowApp()
    {
        InitializeAppUI();
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

    public void InitializeAppUI()
    {
        yourConnectionsText.text = "Your Connections (" + connections.Count + ")";
        // foreach (var connSO in connections)
        // {
        //     // FIX!! Only add if new.
        //     GameObject newConn = Instantiate(connectionProfilePrefab, connectionsListParent);
        //     newConn.GetComponent<ConnectionProfile>().SetInfo(connSO);
        // }
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
