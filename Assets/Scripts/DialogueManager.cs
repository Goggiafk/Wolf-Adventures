using Steamworks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class ChoiceEvent : UnityEvent<Choice> { }

public class DialogueManager : MonoBehaviour
{
    public ChoiceEvent choiceEvent = new ChoiceEvent();
    public Dialogue dialogue;
    public Text chatText;
    public float delay = 0.1f;
    public AudioClip clip;
    public AudioSource mainSource;
    int activeLineIndex;
    bool dialogueStarted;
    public void ChangeDialogue(Dialogue nextDialogue)
    {
        dialogueStarted = false;
        dialogue = nextDialogue;
        AdvanceLine();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            AdvanceLine();
        }
    }
    private void Initialize()
    {
        dialogueStarted = true;
        activeLineIndex = 0;
    }
    public void AdvanceLine()
    {
        if (dialogue == null) return;
        if(!dialogueStarted) Initialize();

        if (activeLineIndex < dialogue.lines.Length)
            DisplayNextSentence();
        else
           AdvanceDialogue();
            
    }

    private void AdvanceDialogue()
    {
        if (dialogue.choice != null)
             choiceEvent.Invoke(dialogue.choice);
        else if (dialogue.nextDialogue != null)
            ChangeDialogue(dialogue.nextDialogue);
        else
            EndDialogue();
    }
    /*public Text nameText;

    bool option;

    private Queue<string> sentences;
    void Start()
    {
        sentences = new Queue<string>();
    }
    
    public void StartDialogue(Dialogue dialogue, Choice choice)
    {
        if (PlayerPrefs.GetInt("_language_index") == 1)
        {
            nameText.text = dialogue.name;

            sentences.Clear();

            foreach (string sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }
            
            DisplayNextSentence();
        } else
        {
            choice1.GetComponent<Text>().text = 
            choice2.GetComponent<Text>().text = dialogue.ChoiceTwoEnglishName;

            nameText.text = dialogue.englishName;

            sentences.Clear();

            foreach (string sentence in dialogue)
            {
                sentences.Enqueue(sentence);
            }

            choice.choice1.GetComponent<DialogueTrigger>().TriggerOption1();
            DisplayNextSentence();
        }
    }
    */
    private void DisplayNextSentence()
    {
        Line line = dialogue.lines[activeLineIndex];
        StopAllCoroutines();
        StartCoroutine(TypeSentence(line.text));

        activeLineIndex += 1;
    }

    IEnumerator TypeSentence(string sentence)
    {
        chatText.text = "";
        foreach(char letter in sentence)
        {
            chatText.text += letter;
            mainSource.PlayOneShot(clip);
            yield return new WaitForSeconds(delay);
        }
    }

    
    void EndDialogue()
    {
        dialogue = null;
        dialogueStarted = false;
        gameObject.SetActive(false);
    }
}
