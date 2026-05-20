using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
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

    public DialogueSO GetCurrentConversation()
    {
        return conversation[convNum];
    }

    // TODO: Check for new conversation.

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
            if (conversation[convNum].isOneTime)
            {
                convNum += 1;
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
            dialogueManager.TurnOffDialogue();
            dialogueInitiated = false;
        }
    }

    void Flip()
    {
        Vector3 currentScale = GetSpriteObject().localScale;
        currentScale.x *= -1;
        GetSpriteObject().localScale = currentScale;
    }
}
