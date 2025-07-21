using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VN_ChoiceButton
{
    public string decisionId;
    public string text;
    public List<VN_Command> commands;
}

[System.Serializable]
public class VN_Choice
{
    public string id;
    public List<VN_ChoiceButton> buttons;
}

public class VN_ChoicesList
{
    public List<string> choices;
}