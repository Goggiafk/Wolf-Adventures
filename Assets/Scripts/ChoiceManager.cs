using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceManager : MonoBehaviour
{
    public Choice choice;
    public Text choiceText;
    public Button optionButton;

    public ExodusManager exodusManager;

    public static string[] optionList = new string[10];
    public static int[] optionNum = new int[10];
    public static int optionId = 0;

    public Dialogue knifeDialogue;

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

    private void Initialize()
    {
        if (PlayerPrefs.GetInt("_language_index") == 1)
            choiceText.text = choice.text;
        else
            choiceText.text = choice.textInEnglish;

        choiceText.color = choice.character.characterColor;
        
        for(int index = 0; index < choice.options.Length; index++)
        {
            OptionManager c = OptionManager.AddChoiceButton(optionButton, choice.options[index], index);
            optionManagers.Add(c);
        }

        for (int i = 0; i <= optionId; i++)
        {
            switch (optionList[i])
            {
                case "noz":
                    if (choice.text == "Пройти обучение?")
                    {
                        Option noz;
                        noz.text = "Зарезать";
                        noz.textInEnglish = "Kill";
                        noz.dialogue = knifeDialogue;
                        OptionManager c = OptionManager.AddChoiceButton(optionButton, noz, choice.options.Length);

                        optionManagers.Add(c);
                    }
                    break;
            }
        }
    
        optionButton.gameObject.SetActive(false);
    }
}
