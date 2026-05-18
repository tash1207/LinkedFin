using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    private DialogueSO currentConversation;
    private bool currentIsAutomatic;
    private int stepNum;
    private bool dialogueActivated;

    // UI References
    [SerializeField] GameObject dialogueCanvas;
    [SerializeField] TMP_Text speakerName;
    [SerializeField] Image portrait;
    [SerializeField] TMP_Text dialogueText;

    public SpeakerSO[] speakerSO;

    private string currentSpeaker;
    private Sprite currentPortrait;

    private PlayerMovement playerMovement;

    // Button references
    [SerializeField] GameObject[] optionButton;
    [SerializeField] TMP_Text[] optionButtonText;
    [SerializeField] GameObject optionsPanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialogueCanvas.SetActive(false);

        playerMovement = FindFirstObjectByType<PlayerMovement>();

        for (int i = 0; i < optionButton.Length; i++)
        {
            optionButton[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogueActivated && ((currentIsAutomatic && stepNum == 0) || InputManager.interactAction.WasPressedThisFrame()))
        {
            if (stepNum >= currentConversation.speakers.Length)
            {
                TurnOffDialogue();
                Time.timeScale = 1;
                playerMovement.enabled = true;
            }
            else
            {
                playerMovement.enabled = false;
                Time.timeScale = 0;
                PlayDialogue();
            }
        }
    }

    void PlayDialogue()
    {
        // If Random NPC
        if (currentConversation.speakers[stepNum] == DialogueSpeakers.Random)
        {
            SetSpeakerInfo(false);
        }
        else
        {
            SetSpeakerInfo(true);
        }

        speakerName.text = currentSpeaker;
        portrait.sprite = currentPortrait;

        if (currentConversation.speakers[stepNum] == DialogueSpeakers.Branch)
        {
            for (int i = 0; i < currentConversation.options.Length; i++)
            {
                if (currentConversation.options[i] == null)
                {
                    optionButton[i].SetActive(false);
                }
                else
                {
                    optionButtonText[i].text = currentConversation.options[0];
                    optionButton[i].SetActive(true);
                }
            }
        }

        if (stepNum < currentConversation.dialogue.Length)
        {
            dialogueText.text = currentConversation.dialogue[stepNum];
        }
        else
        {
            optionsPanel.SetActive(true);
        }

        dialogueCanvas.SetActive(true);
        stepNum += 1;
    }

    void SetSpeakerInfo(bool recurringCharacter)
    {
        if (recurringCharacter)
        {
            foreach (var speaker in speakerSO)
            {
                if (speaker.name == currentConversation.speakers[stepNum].ToString())
                {
                    currentSpeaker = speaker.speakerName;
                    currentPortrait = speaker.speakerPortrait;
                }
            }
        }
        else
        {
            currentSpeaker = currentConversation.randomSpeakerName;
            currentPortrait = currentConversation.randomSpeakerPortrait;
        }
    }

    public void InitiateDialogue(DialogueSO npcConversation)
    {
        currentConversation = npcConversation;
        currentIsAutomatic = currentConversation.isAutomatic;
        dialogueActivated = true;
    }

    public void TurnOffDialogue()
    {
        stepNum = 0;
        dialogueActivated = false;
        optionsPanel.SetActive(true);
        dialogueCanvas.SetActive(false);
    }
}

public enum DialogueSpeakers
{
    Finnley,
    Dentist,
    Shark_Mark,
    Shark_Joe,
    Shark_Lori,
    Random,
    Branch
};
