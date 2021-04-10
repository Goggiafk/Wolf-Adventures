using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct Option
{
    [TextArea(2, 5)]
    public string text;
    public Dialogue dialogue;
}

[CreateAssetMenu(fileName = "New Question", menuName = "Question")]
public class Choice : ScriptableObject
{
    [TextArea(2, 5)]
    public string text;
    public Option[] options;
}