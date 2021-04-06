using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text chatText;
    public float delay = 0.1f;
    public AudioClip clip;
    public AudioSource mainSource;

    private Queue<string> sentences;
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        if (PlayerPrefs.GetInt("language") == 0)
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
            nameText.text = dialogue.englishName;

            sentences.Clear();

            foreach (string sentence in dialogue.sentencesInEnglish)
            {
                sentences.Enqueue(sentence);
            }

            DisplayNextSentence();
        }
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
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

    }

}
