using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using UnityEngine.TextCore.Text;

public class VN_CharactersView : MonoBehaviour
{
    [SerializeField] private Image m_characterRightImage;

    private string m_characterName = "";

    public void Start()
    {
        if (m_characterRightImage)
        {
            m_characterRightImage.color = new Color(255, 255, 255, 255);
        }
    }

    public void SetActiveRightCharacter(VN_Character character)
    {
        SetActiveCharacterImage(m_characterRightImage, character);
    }

    public void SetActiveLeftCharacter(VN_Character character)
    {
        SetActiveCharacterImage(m_characterRightImage, character);
    }

    private void SetActiveCharacterImage(Image characterImage, VN_Character character)
    {
        if (characterImage == null)
        {
            return;
        }
        if (m_characterName == "" || m_characterName != character.path)
        {
            m_characterName = character.path;
            characterImage.sprite = Resources.Load<Sprite>(m_characterName);
            characterImage.transform.localScale = new Vector3(character.reverse ? -1 : 1, 1, 1);
        }
    }

    public void SetCharacterVisible(bool visible)
    {
        m_characterRightImage.gameObject.SetActive(visible);
    }
}
