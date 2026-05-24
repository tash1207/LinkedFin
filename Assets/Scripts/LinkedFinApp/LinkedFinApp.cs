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
    }

    void OnDisable()
    {
        Actions.OnConnectionMade -= AddConnection;
        Actions.ToggleFinApp += ToggleApp;
        Actions.ShowFintroduceList -= ShowFintroduceList;
        Actions.Fintroduce -= Fintroduce;
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

    public void AddConnection(ConnectionSO connectionSO)
    {
        if (!connections.Contains(connectionSO))
        {
            noConnectionsText.text = "";
            connections.Add(connectionSO);
            GameObject newConn = Instantiate(connectionProfilePrefab, connectionsListParent);
            newConn.GetComponent<ConnectionProfile>().SetInfo(connectionSO);
        }
    }

    public void AddMessage(MessageSO messageSO)
    {
        if (!messages.Contains(messageSO))
        {
            noMessagesText.text = "";
            messages.Add(messageSO);
            yourMessagesText.text = "Messages\n(" + messages.Count + ")";
            // TODO: UI for messages
            GameObject newConn = Instantiate(messageItemPrefab, messagesListParent);
            newConn.GetComponent<MessageInfo>().SetInfo(messageSO);
            Debug.Log("LinkedFin message from " + messageSO.connection.connName);
        }
    }

    public List<ConnectionSO> GetConnections()
    {
        return connections;
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
            if (conn != currentProfile)
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

        // Instead of resources, add array and check
        // if (speaker.name == currentConversation.speakers[stepNum].ToString())

        if (selectedProfile.connName == "Dr. Emilio Wrasse")
        {
            if (newConnection.connName == "Morey Lee")
            {
                MessageSO dentistGood = Resources.Load<MessageSO>("DentistGood");
                AddMessage(dentistGood);
            }
        }
        else if (selectedProfile.connName == "Morey Lee")
        {
            if (newConnection.connName == "Dr. Emilio Wrasse")
            {
                MessageSO dentistGood = Resources.Load<MessageSO>("DentistGood");
                AddMessage(dentistGood);
            }
        }

        // TODO: Maybe close full app and show a dialog or add a messaging page to the app.
        fintroduceListCanvas.SetActive(false);
    }
}
