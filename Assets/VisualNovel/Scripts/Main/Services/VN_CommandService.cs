using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;
using System;
using static UnityEditor.FilePathAttribute;

public class VN_CommandService
{
    enum ECommand
    {
        LOAD_DIALOG,
        CLEAR_DIALOG,
        SCENE_OPEN,
        MAKE_CHOICE,
        SET_CHARACTER_VISIBLE,
        SET_LOCATION,
        START_LOCATION_ANIMATE,
        STOP_LOCATION_ANIMATE,
        OPEN_MESSAGE_BOX,
        OPEN_HOOK,
        PLAY_SOUND,
        PLAY_LOOP_SOUND,
        SET_TEXT_DELAY
    }

    private static Dictionary<string, ECommand> m_commands = new Dictionary<string, ECommand>()
    {
        { "loadDialog", ECommand.LOAD_DIALOG },
        { "clearDialog", ECommand.CLEAR_DIALOG },
        { "sceneOpen", ECommand.SCENE_OPEN },
        { "makeChoice", ECommand.MAKE_CHOICE },
        { "setCharacterVisible", ECommand.SET_CHARACTER_VISIBLE },
        { "setLocation", ECommand.SET_LOCATION },
        { "startLocationAnimate", ECommand.START_LOCATION_ANIMATE },
        { "stopLocationAnimate", ECommand.STOP_LOCATION_ANIMATE },
        { "openMessageBox", ECommand.OPEN_MESSAGE_BOX },
        { "openHook", ECommand.OPEN_HOOK },
        { "playSound", ECommand.PLAY_SOUND },
        { "playLoopSound", ECommand.PLAY_LOOP_SOUND },
        { "setTextDelay", ECommand.SET_TEXT_DELAY },
    };

    public static void RunCommand(string command, string commandArg)
    {
        VN_Logger.Log("[VN_CommandService][RunCommand] " + command + ": " + commandArg);
        switch ((m_commands[command]))
        {
            case ECommand.LOAD_DIALOG:
                VN_DialogModel.Instance().LoadDialog(commandArg);
                break;
            case ECommand.CLEAR_DIALOG:
                VN_DialogModel.Instance().ClearDialog();
                break;
            case ECommand.SCENE_OPEN:
                SceneManager.LoadScene(commandArg);
                break;
            case ECommand.MAKE_CHOICE:
                VN_ChoiceModel.Instance().LoadChoice(commandArg);
                break;
            case ECommand.SET_CHARACTER_VISIBLE:
                VN_CharactersModel.Instance().SetCharacterVisible(commandArg == "true");
                break;
            case ECommand.SET_LOCATION:
                VN_LocationModel.Instance().SetLocation(commandArg);
                break;
            case ECommand.START_LOCATION_ANIMATE:
                VN_LocationModel.Instance().StartAnimate();
                break;
            case ECommand.STOP_LOCATION_ANIMATE:
                VN_LocationModel.Instance().StopAnimate();
                break;
            case ECommand.OPEN_MESSAGE_BOX:
                VN_PopupModel.Instance().OpenMessageBox(commandArg);
                break;
            case ECommand.OPEN_HOOK:
                VN_PopupModel.Instance().OpenHook(commandArg);
                break;
            case ECommand.PLAY_SOUND:
                //TODO: Add request audio model
                break;
            case ECommand.PLAY_LOOP_SOUND:
                //TODO: Add request audio model
                break;
            case ECommand.SET_TEXT_DELAY:
                float.TryParse(commandArg, out float tryParsedFloat);
                VN_DialogModel.Instance().SetTextDelay(tryParsedFloat);
                break;
        }
    }
}
