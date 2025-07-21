using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;


[System.Serializable]
public class VN_Profile
{
    public string currentDialog;
    public string currentLocationName;
    public int currentReplica;
    public List<string> madeDecisions;
}

public class VN_SaveService
{
    private static VN_Profile m_profile;

    public static VN_Profile GetProfile() { return m_profile; }

    public static void ResetProfile() { m_profile = null; }

    public static void LoadProfile(string fileName)
    {
        string profilePath = Application.persistentDataPath + "/" + fileName + ".json";
        if (File.Exists(profilePath))
        {
            var jsonString = File.ReadAllText(profilePath);
            m_profile = JsonUtility.FromJson<VN_Profile>(jsonString);
            VN_Logger.Log("[VN_SaveService][LoadProfile] Load Profile: " + m_profile.currentDialog + ", " + m_profile.currentReplica);
            //TODO: End Load Profile
        }
        else
        {
            VN_Logger.Log("[VN_SaveService][LoadProfile] Profile file: " + " not found.");
        }
    }

    public static void SaveProfile(string fileName)
    {
        UpdateProfile();
        string profileData = JsonUtility.ToJson(m_profile);
        string profilePath = Application.persistentDataPath + "/" + fileName + ".json";
        System.IO.File.WriteAllText(profilePath, profileData);
        VN_PopupModel.Instance().OpenMessageBox("Game was saved");
        VN_Logger.Log("[VN_SaveService][SaveProfile] Save profile to: " + profilePath);
    }

    private static void UpdateProfile()
    {
        if (m_profile == null)
        {
            m_profile = new VN_Profile();
            m_profile.madeDecisions = new List<string>();
        }
        m_profile.currentDialog = VN_DialogModel.Instance().GetCurrentDialogId();
        m_profile.currentReplica = VN_DialogModel.Instance().GetCurrentRepliceCount();
        m_profile.madeDecisions = VN_ChoiceModel.Instance().GetMadeDecisions().ToList();
        m_profile.currentLocationName = VN_LocationModel.Instance().GetLocationName();
    }
}
