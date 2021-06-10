using System;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "New Сonsequence", menuName = "Сonsequence")]
public class Exodus : ScriptableObject
{
    public float moneyAmount;
    public float foodAmount;
    public float happinesAmount;
    public float developmentAmount;
    public string nameOfExodus;
    public int exodusInt;
    public bool spawnNext;
    public bool HideCurrentCharacter;
}
