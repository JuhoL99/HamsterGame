using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public class KissaInteraction : MonoBehaviour
{
    bool playerInRange;
    bool inDialogue, dialogueEnded;
    public RectTransform dialoguePanel;
    TextMeshProUGUI kissaTextBox;
    static int dialogueNum = 0;
    static string[] dialogue;
    static string[] startDialogue =
    {
        "Well well well... If it isn't the owner's favorite pet. " +
            "I've seen you struggle and shuffle about in your cage. You want out, don't you? ",
            "Not just from the cage, from this house! To be free from your shackles!",
        "I might know someone that can help with your great escape. *cackle*",
        "It's me. That someone is me.",
        "I could easily jump up to that handle on the front door, " +
            "but what would I get in return for my troubles?",
        "Oh! I know just the thing. It's a small favor, really. ",
            "I want you to find your way up to every fragile and valuable looking thing in this house... ",
         "..and break it!!",
         "There really isn't a sweeter sound in the world than the shattering of precious porcelain.",
         "Now go! Destroy everything in sight!"
    };
    static string[] endDialogue =
    {
        "I didn't think you'd have it in you, you little monster!",
        "Ugh... walking to the door is so troublesome. But, a deal is a deal. Go, be free! Spread your metaphorical wings!"
    };
    void Start()
    {
        kissaTextBox = dialoguePanel.GetComponentInChildren<TextMeshProUGUI>();
        dialogue = startDialogue;
    }
    public static void QuestDone()
    {
        dialogue = endDialogue;
        dialogueNum = 0;
    }
    void Update()
    {
        if (inDialogue && Input.GetKeyDown(KeyCode.F))
        {
            AdvanceDialogue();
        }
        if (playerInRange && Input.GetKeyDown(KeyCode.F) && !inDialogue && !dialogueEnded)
        {
            EnableDialogue();
        }
        dialogueEnded = false;
    }
    void EnableDialogue()
    {
        inDialogue = true;
        dialoguePanel.gameObject.SetActive(true);
        AdvanceDialogue();
        GameManager.FreezeGame(true);
    }
    void DisableDialogue()
    {
        inDialogue = false;
        dialoguePanel.gameObject.SetActive(false);
        GameManager.FreezeGame(false);

    }
    void AdvanceDialogue()
    {
        if (dialogueNum >= dialogue.Length)
        {
            DisableDialogue();
            dialogueNum = 0;
            dialogueEnded = true;
        }
        else
        {
            kissaTextBox.text = dialogue[dialogueNum];
            dialogueNum++;
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (inDialogue)
            {
                DisableDialogue();
                dialogueNum--;
            }
        }

    }
}
