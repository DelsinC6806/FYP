using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{
    public Queue<string> sentences;
    public Animator dialogueBoxAnimator;
    public Button continueButton;
    public Image dialogueBox;
    public Text NPC_name;
    public Text dialogueText;

    public float typingSpeed;

    void Start()
    {
        dialogueBoxAnimator.SetBool("Talking", false);    
    }

    public void StartDialogue(Dialogue dialogue)
    {
        if (!NPC.missionTriggered)
        {
            sentences = new Queue<string>();
            NPC.currentSentence = 0;
            dialogueBoxAnimator.SetBool("Talking", true);
            sentences.Clear();
            NPC_name.text = dialogue.name;
            foreach (string word in dialogue.sentences)
            {
                sentences.Enqueue(word);
            }
            DisplayNextSentence();
        }
    }

    public void DisplayNextSentence()
    {
        if (!NPC.missionTriggered)
        {
            if (sentences.Count == 0)
            {
                EndDialogue();
                return;
            }
            StartCoroutine("Type", sentences.Dequeue());
            NPC.currentSentence += 1;
        }
    }

    IEnumerator Type(string phrase)
    {
        continueButton.enabled = false;
        dialogueText.text = "";
        foreach (char letter in phrase.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
        continueButton.enabled = true;
    }

    void EndDialogue()
    {
        dialogueBoxAnimator.SetBool("Talking", false);
        PlayerController.instance.EnableControl();
        NPC.Talking = false;
    }




}