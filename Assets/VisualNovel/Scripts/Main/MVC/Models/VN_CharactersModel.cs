using System.Collections.Generic;
using UnityEngine;

public class VN_CharactersModel : MonoBehaviour
{
    [SerializeField] private TextAsset m_charactersJson;
    [SerializeField] private VN_CharactersView m_charactersView;

    private Dictionary<string, VN_Character> m_charactersDictionary;
    private VN_Character m_activeCharacter;

    private static VN_CharactersModel m_instance;

    public void Awake()
    {
        m_instance = this;
        m_charactersDictionary = new Dictionary<string, VN_Character>();
        VN_CharacterList characterList = JsonUtility.FromJson<VN_CharacterList>(m_charactersJson.text);
        foreach (var character in characterList.characters)
        {
            m_charactersDictionary.Add(character.name, character);
        }
    }

    public static VN_CharactersModel Instance()
    {
        return m_instance;
    }

    public VN_Character GetCharacterByName(string characterName)
    {
        return m_charactersDictionary[characterName];
    }

    public VN_Character GetActiveCharacter()
    {
        return m_activeCharacter;
    }

    public void SetActiveCharacter(string characterName)
    {
        VN_Logger.Log("[VN_CharactersModel][SetActiveCharacter] Set character: " + characterName);
        m_activeCharacter = m_charactersDictionary[characterName];
        if (m_charactersView)
        {
            m_charactersView.SetActiveRightCharacter(m_activeCharacter);
        }
    }

    public Color32 GetActiveCharacterColor()
    {
        return new Color32((byte)m_activeCharacter.color[0],
                           (byte)m_activeCharacter.color[1],
                           (byte)m_activeCharacter.color[2],
                           (byte)m_activeCharacter.color[3]);
    }

    public void SetCharacterVisible(bool visible)
    {
        if (m_charactersView)
        {
            m_charactersView.SetCharacterVisible(visible);
        }
    }
}

[System.Serializable]
public class VN_Character
{
    public string name;
    public string path;
    public bool reverse;
    public List<int> color;
}

[System.Serializable]
public class VN_CharacterList
{
    public List<VN_Character> characters;
}