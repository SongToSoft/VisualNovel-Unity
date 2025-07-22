using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using UnityEngine.TextCore.Text;

public class VN_CharactersView : MonoBehaviour
{
    [SerializeField] private Image m_characterMiddleImage;
    [SerializeField] private Image m_characterRightImage;
    [SerializeField] private bool m_fadeEnabled = true;
    [SerializeField] private float m_animationDuration = 2f;
    [SerializeField] private float m_movementDistance = 1f;
    [SerializeField] private float m_fadeSpeed = 0.05f;

    private Vector3 m_defaultCharacterPosition;

    private bool m_isCharacterReverse;
    private string m_characterName = "";

    public void Start()
    {
        if (m_characterRightImage)
        {
            m_characterRightImage.color = new Color(255, 255, 255, 255);
            m_defaultCharacterPosition = m_characterRightImage.transform.position;
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
        if (m_characterName == "" || !m_fadeEnabled)
        {
            m_characterName = character.path;
            m_isCharacterReverse = character.reverse;
            characterImage.sprite = Resources.Load<Sprite>(m_characterName);
            characterImage.transform.localScale = new Vector3(character.reverse ? -1 : 1, 1, 1);
        }
        else
        {
            if (m_characterName != character.path)
            {
                m_characterName = character.path;
                m_isCharacterReverse = character.reverse;
                StartCoroutine(AnimateImage());
            }
        }
    }

    public void SetCharacterVisible(bool visible)
    {
        m_characterRightImage.gameObject.SetActive(visible);
    }

    private IEnumerator AnimateImage()
    {
        //TODO: Fix faster click on next
        float timer = 0f;

        m_characterRightImage.color = new Color(m_characterRightImage.color.r, m_characterRightImage.color.g, m_characterRightImage.color.b, 1);
        m_characterRightImage.transform.position = m_defaultCharacterPosition;

        while (timer < m_animationDuration / 2)
        {
            timer += Time.deltaTime;
            m_characterRightImage.rectTransform.position += (m_movementDistance / (m_animationDuration / 2)) * Time.deltaTime * Vector3.right;
            m_characterRightImage.color -= new Color(0, 0, 0, m_fadeSpeed);
            yield return null;
        }

        m_characterRightImage.transform.localScale = new Vector3(m_isCharacterReverse ? -1 : 1, 1, 1);
        m_characterRightImage.color = new Color(m_characterRightImage.color.r, m_characterRightImage.color.g, m_characterRightImage.color.b, 0);
        m_characterRightImage.sprite = Resources.Load<Sprite>(m_characterName);

        while (timer < m_animationDuration)
        {
            timer += Time.deltaTime;
            m_characterRightImage.rectTransform.position -= (m_movementDistance / (m_animationDuration / 2)) * Time.deltaTime * Vector3.right;
            m_characterRightImage.color += new Color(0, 0, 0, m_fadeSpeed);
            yield return null;
        }

        m_characterRightImage.rectTransform.position = m_defaultCharacterPosition;
        m_characterRightImage.color = new Color(m_characterRightImage.color.r, m_characterRightImage.color.g, m_characterRightImage.color.b, 1);
    }
}
