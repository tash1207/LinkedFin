using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public SpeakerSO speakerSOIdentifier;
    public DialogueSO[] conversation;
    public int convNum;
    public bool shouldFlip;
    private Transform player;
    private SpriteRenderer speechBubbleRenderer;
    private DialogueManager dialogueManager;

    private bool dialogueInitiated;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        speechBubbleRenderer = GetComponent<SpriteRenderer>();
        speechBubbleRenderer.enabled = false;
    }

    void OnEnable()
    {
        Actions.SetDialogStep += SetCurrentDialogue;
    }

    void OnDisable()
    {
        Actions.SetDialogStep += SetCurrentDialogue;
    }

    public DialogueSO GetCurrentConversation()
    {
        return conversation[convNum];
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player" && !dialogueInitiated)
        {
            speechBubbleRenderer.enabled = true;
            player = collider.gameObject.GetComponent<Transform>();

            if (shouldFlip && player.position.x > transform.position.x && GetSpriteObject().localScale.x < 0)
            {
                Flip();
            }
            else if (shouldFlip && player.position.x < transform.position.x && GetSpriteObject().localScale.x > 0)
            {
                Flip();
            }

            dialogueManager.InitiateDialogue(GetCurrentConversation());
            dialogueInitiated = true;
        }
    }

    public void SetCurrentDialogue(SpeakerSO speakerSO, DialogueSO dialogueSO)
    {
        if (speakerSO != speakerSOIdentifier)
        {
            return;
        }

        // If no explicit dialogue is given, advance to the next.
        if (!dialogueSO)
        {
            convNum += 1;
        }

        for (int i = 0; i < conversation.Length; i++)
        {
            if (conversation[i] == dialogueSO)
            {
                convNum = i;
            }
        }
    }

    private Transform GetSpriteObject()
    {
        return transform.parent.GetChild(0);
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            speechBubbleRenderer.enabled = false;
            dialogueInitiated = false;
            dialogueManager.UninitiateDialogue();
        }
    }

    void Flip()
    {
        Vector3 currentScale = GetSpriteObject().localScale;
        currentScale.x *= -1;
        GetSpriteObject().localScale = currentScale;
    }
}
