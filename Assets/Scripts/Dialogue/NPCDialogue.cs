using System;
using UnityEditor.Search;
using UnityEditor.Tilemaps;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public DialogueSO[] conversation;
    public bool shouldFlip;
    public bool isAutomatic;
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

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player" && !dialogueInitiated)
        {
            speechBubbleRenderer.enabled = true;
            player = collider.gameObject.GetComponent<Transform>();

            if (shouldFlip && player.position.x > transform.position.x && transform.parent.localScale.x < 0)
            {
                Flip();
            }
            else if (shouldFlip && player.position.x < transform.position.x && transform.parent.localScale.x > 0)
            {
                Flip();
            }

            dialogueManager.InitiateDialogue(this);
            dialogueInitiated = true;
        }
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
        Vector3 currentScale = transform.parent.localScale;
        currentScale.x *= -1;
        transform.parent.localScale = currentScale;
    }
}
