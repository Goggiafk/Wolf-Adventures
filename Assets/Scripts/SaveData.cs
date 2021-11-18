using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    private static SaveData _current;
    public static SaveData current
    {
        get
        {
            if (_current == null)
            {
                _current = new SaveData();
            }
            return _current;
        }
        set
        {
            if (value != null)
            {
                _current = value;
            }
        }
    }

    public PlayerProfile profile;
    public float money;
    public float sawDust;
    public float happines;
    public float development;
    public int stotyCharacterIdS;
    public string[] eventHolderS = new string[999];
    public byte[] eventHolderIdS = new byte[999];
    public string[] characterNamesS = new string[999];
    public string[] savedCharactersS = new string[999];
    public int idOfRestS;
    public string[] savedRestS = new string[999];
    public int idKokS;
    public int currentDayS;
    public int[] optionNumS;
    public int idOfUpgradeS;

    public int peopleAgainstS;
    public int relationWithMexS;
    public int moneyToMexS;
    //Items
    public int numberOfItems;
    public string[] itemName = new string[5];
    public string[] englishItemName = new string[5];
    public string[] itemDescription = new string[5];
    public string[] englishItemDescription = new string[5];
    public string[] spriteName = new string[5];
    public int[] itemCost = new int[5];
    public char[] itemSellType = new char[5];
    //Upgrades
    public int upgradeId;
    public string[] upgradeName = new string[999];
    public string[] englishUpgradeName = new string[999];
    public int[] upgradePrice = new int[999];
    public string[] upgradeObject = new string[999];
    //Events
    public int idOfEventCounterS;
    public string[] eventCountersS = new string[100];
    public int[] whenToAppearS = new int[100];
    public int[] eventIntsS = new int[100];
    public int optionIdS = 0;
    public string[] optionListS = new string[100];
}
