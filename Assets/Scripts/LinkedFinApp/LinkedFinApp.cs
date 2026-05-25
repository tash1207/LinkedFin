using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LinkedFinApp : MonoBehaviour
{
    [SerializeField] GameObject appCanvas;
    [SerializeField] TMP_Text yourConnectionsText;
    [SerializeField] TMP_Text noConnectionsText;
    [SerializeField] TMP_Text yourMessagesText;
    [SerializeField] TMP_Text noMessagesText;

    [SerializeField] Transform connectionsListParent;
    [SerializeField] GameObject connectionProfilePrefab;
    [SerializeField] Transform messagesListParent;
    [SerializeField] GameObject messageItemPrefab;
    [SerializeField] GameObject fintroduceListCanvas;
    [SerializeField] Transform fintroduceListParent;
    [SerializeField] TMP_Text fintroduceHeaderText;
    [SerializeField] GameObject fintroductionListItemPrefab;

    [SerializeField] GameObject connectionsPanel;
    [SerializeField] GameObject messagesPanel;
    [SerializeField] GameObject newMessageIndicator;
    [SerializeField] GameObject gameOverCanvas;

    [SerializeField] MessageSO[] allMessages;

    private readonly List<ConnectionSO> connections = new List<ConnectionSO>();
    private readonly List<MessageSO> messages = new List<MessageSO>();

    private PlayerMovement playerMovement;

    public static LinkedFinApp Instance;
    public bool isAvailable;

    private ConnectionSO selectedProfile;

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
        Actions.ShowFintroduceList += ShowFintroduceList;
        Actions.Fintroduce += Fintroduce;
        Actions.UpdateProfile += UpdateProfile;
        Actions.OnGameOver += GameOver;
    }

    void OnDisable()
    {
        Actions.OnConnectionMade -= AddConnection;
        Actions.ToggleFinApp += ToggleApp;
        Actions.ShowFintroduceList -= ShowFintroduceList;
        Actions.Fintroduce -= Fintroduce;
        Actions.UpdateProfile -= UpdateProfile;
        Actions.OnGameOver -= GameOver;
    }

    void Update()
    {
        if (appCanvas.activeSelf && messagesPanel.activeSelf)
        {
            newMessageIndicator.SetActive(false);
        }
    }

    public void ToggleApp()
    {
        if (isAvailable)
        {
            if (appCanvas && appCanvas.activeSelf)
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
        yourConnectionsText.text = "Connections\n(" + connections.Count + ")";
        yourMessagesText.text = "Messages\n(" + messages.Count + ")";
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        playerMovement.isPaused = true;
        gameOverCanvas.SetActive(true);
    }

    public void AddConnection(ConnectionSO connectionSO)
    {
        if (!connections.Contains(connectionSO))
        {
            noConnectionsText.text = "";
            connections.Add(connectionSO);
            GameObject newConn = Instantiate(connectionProfilePrefab, connectionsListParent);
            newConn.GetComponent<ConnectionProfile>().SetInfo(connectionSO);

            if (connectionSO.connName == "Whale Buffett")
            {
                // Add message from Shark Mark
                MessageSO message = Resources.Load<MessageSO>("SharkMark");
                AddMessage(message);
                MessageFollowUpActions(message);
            }
        }

        if (connections.Count == 7)
        {
            // Add message from Shark Lori
            MessageSO message = Resources.Load<MessageSO>("SharkLori");
            AddMessage(message);
            MessageFollowUpActions(message);
        }
    }

    public void AddMessage(MessageSO messageSO)
    {
        if (!messages.Contains(messageSO))
        {
            newMessageIndicator.SetActive(true);
            noMessagesText.text = "";
            messages.Add(messageSO);
            yourMessagesText.text = "Messages\n(" + messages.Count + ")";
            GameObject newConn = Instantiate(messageItemPrefab, messagesListParent);
            newConn.GetComponent<MessageInfo>().SetInfo(messageSO);
            Debug.Log("LinkedFin message from " + messageSO.connection.connName);
        }

        // We've finished all 3 fintroductions (+ message from SharkLori)
        if (messages.Count == 4)
        {
            // Add message from Shark Joe
            MessageSO message = Resources.Load<MessageSO>("SharkJoe");
            AddMessage(message);
            MessageFollowUpActions(message);
        }
    }

    public List<ConnectionSO> GetConnections()
    {
        return connections;
    }

    public void UpdateProfile(ConnectionSO oldConnection, ConnectionSO newConnection)
    {
        foreach (Transform child in connectionsListParent)
        {
            if (child.gameObject.GetComponent<ConnectionProfile>().profile == oldConnection)
            {
                child.gameObject.GetComponent<ConnectionProfile>().SetInfo(newConnection);
            }
        }
        for (int i = 0; i < connections.Count; i++)
        {
            if (connections[i] == oldConnection)
            {
                connections[i] = newConnection;
                return;
            }
        }
    }

    public void ShowFintroduceList(ConnectionSO currentProfile)
    {
        selectedProfile = currentProfile;
        fintroduceHeaderText.text = "Fintroduce " + currentProfile.connName + " to:";

        // Clear existing list items
        foreach (Transform child in fintroduceListParent)
        {
            Destroy(child.gameObject);
        }
        // Instantiate list of connection names minus current profile
        foreach (ConnectionSO conn in connections)
        {
            if (conn != currentProfile && !conn.hideFintroduce)
            {
                GameObject newListItem = Instantiate(fintroductionListItemPrefab, fintroduceListParent);
                newListItem.GetComponent<FintroduceListItem>().SetInfo(conn);
            }
        }
        fintroduceListCanvas.SetActive(true);
    }

    public void Fintroduce(ConnectionSO newConnection)
    {
        // Logic for what happens when you fintroduce each character to each other
        Debug.Log("Fintroducing " + selectedProfile.connName + " to " + 
            newConnection.connName);

        if (selectedProfile.connName == "Dr. Emilio Wrasse")
        {
            if (newConnection.connName == "Morey Lee")
            {
                MessageSO message = Resources.Load<MessageSO>("DentistGood");
                AddMessage(message);
                CorrectFintroduction(selectedProfile);
            }
        }
        else if (selectedProfile.connName == "Morey Lee")
        {
            if (newConnection.connName == "Dr. Emilio Wrasse")
            {
                MessageSO message = Resources.Load<MessageSO>("DentistGood");
                AddMessage(message);
                CorrectFintroduction(newConnection);
            }
        }
        else if (selectedProfile.connName == "PT Barnacle")
        {
            if (newConnection.connName == "Ringo Starfish")
            {
                MessageSO message = Resources.Load<MessageSO>("Ubarnacle");
                AddMessage(message);
                CorrectFintroduction(selectedProfile);
            }
        }
        else if (selectedProfile.connName == "Ringo Starfish")
        {
            if (newConnection.connName == "PT Barnacle")
            {
                MessageSO message = Resources.Load<MessageSO>("Ubarnacle");
                AddMessage(message);
                CorrectFintroduction(newConnection);
            }
        }
        else if (selectedProfile.connName == "Shelley")
        {
            if (newConnection.connName == "Otto Otterton")
            {
                MessageSO message = Resources.Load<MessageSO>("Shellow");
                AddMessage(message);
                CorrectFintroduction(selectedProfile);
            }
        }
        else if (selectedProfile.connName == "Otto Otterton")
        {
            if (newConnection.connName == "Shelley")
            {
                MessageSO message = Resources.Load<MessageSO>("Shellow");
                AddMessage(message);
                CorrectFintroduction(newConnection);
            }
        }

        fintroduceListCanvas.SetActive(false);
    }

    private void CorrectFintroduction(ConnectionSO connection)
    {
        if (connection.actionsToFire != null && connection.actionsToFire.Length > 0)
        {
            foreach (var actionToFire in connection.actionsToFire)
            {
                actionToFire.ExecuteAction();
            }
        }
    }

    private void MessageFollowUpActions(MessageSO message)
    {
        if (message.actionsToFire != null && message.actionsToFire.Length > 0)
        {
            foreach (var actionToFire in message.actionsToFire)
            {
                actionToFire.ExecuteAction();
            }
        }
    }
}
