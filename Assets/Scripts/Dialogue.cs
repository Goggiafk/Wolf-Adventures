using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string name;
    public string englishName;
    [TextArea(3, 10)]
    public string[] sentences;
    [TextArea(3, 10)]
    public string[] sentencesInEnglish;
}
