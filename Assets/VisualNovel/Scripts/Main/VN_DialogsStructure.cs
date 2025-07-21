using System.Collections.Generic;

[System.Serializable]
public class VN_Command
{
    public string command;
    public string commandArg;
}

[System.Serializable]
public class VN_Replica
{
    public string character;
    public string text;
    public List<VN_Command> commands;
}

[System.Serializable]
public class VN_Dialog
{
    public string id;
    public string location;
    public List<VN_Replica> replicas;
}