using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VN_ChoiceView : MonoBehaviour
{
    [SerializeField] private GameObject m_choicePanel;
    [SerializeField] private GameObject m_choiceButtonPrefab;

    public void AddButton(VN_ChoiceButton button)
    {
        if (m_choiceButtonPrefab == null || button == null)
        {
            VN_Logger.Log("[VN_ChoiceView][AddButton] Can't add new choice button");
            return;
        }
        VN_Logger.Log("[VN_ChoiceView][AddButton] Add new choice button: " + button.text);
        var buttonObject = Instantiate(m_choiceButtonPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        buttonObject.transform.SetParent(m_choicePanel.transform, false);
        var buttonView = buttonObject.GetComponent<VN_ChoiceButtonView>();
        if (buttonView)
        {
            buttonView.LoadChoiceButton(button);
        }
    }

    public void LoadChoice(VN_Choice choice)
    {
        if (choice == null && m_choicePanel == null)
        {
            return;
        }

        m_choicePanel.SetActive(true);
        foreach (var button in choice.buttons)
        {
            AddButton(button);
        }
    }

    public void OnChoiceCallback()
    {
        if (m_choicePanel == null)
        {
            return;
        }

        foreach (Transform child in m_choicePanel.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        m_choicePanel.SetActive(false);
    }
}
