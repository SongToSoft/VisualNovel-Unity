using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class VN_ChoiceButtonView : MonoBehaviour
{
    [SerializeField] private TMP_Text m_buttonText;

    private VN_ChoiceButton m_choiceButton;

    public void LoadChoiceButton(VN_ChoiceButton choiceButton)
    {
        if (choiceButton == null)
        {
            return;
        }
        m_buttonText.text = choiceButton.text;
        m_choiceButton = choiceButton;
        GetComponent<Button>().onClick.AddListener(OnChoiceCallback);
    }

    private void OnChoiceCallback()
    {
        VN_Logger.Log("[VN_ChoiceButtonView][OnChoiceCallback] Make Choice: " + m_choiceButton.decisionId);

        if (m_choiceButton.commands.Count != 0)
        {
            for (int i = 0; i < m_choiceButton.commands.Count; i++)
            {
                VN_CommandService.RunCommand(m_choiceButton.commands[i].command, m_choiceButton.commands[i].commandArg);
            }
        }
        VN_ChoiceModel.Instance().OnChoiceCallback(m_choiceButton.decisionId);
    }
}
