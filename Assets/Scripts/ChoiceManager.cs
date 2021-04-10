using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceManager : MonoBehaviour
{
    public Choice choice;
    public Text choiceText;
    public Button optionButton;

    private List<OptionManager> optionManagers = new List<OptionManager>();

    public void Change(Choice _choice)
    {
        RemoveChoices();
        choice = _choice;
        gameObject.SetActive(true);
        Initialize();
    }

    public void Hide(Dialogue dialogue)
    {
        RemoveChoices();
        gameObject.SetActive(false);
    }

    private void RemoveChoices()
    {
        foreach (OptionManager c in optionManagers)
            Destroy(c.gameObject);

        optionManagers.Clear();
    }

    private void Start() { }

    private void Initialize()
    {
        choiceText.text = choice.text;

        for(int index = 0; index < choice.options.Length; index++)
        {
            OptionManager c = OptionManager.AddChoiceButton(optionButton, choice.options[index], index);
            optionManagers.Add(c);
        }

        optionButton.gameObject.SetActive(false);
    }
}
