using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VN_ChoiceModel : MonoBehaviour
{
    [SerializeField] private VN_ChoiceView m_choiceView;

    private VN_Choice m_currentChoice;
    private SortedSet<string> m_madeDecisions;

    private static VN_ChoiceModel m_instance;

    public void Awake()
    {
        m_instance = this;
        m_madeDecisions = new SortedSet<string>();
    }

    public static VN_ChoiceModel Instance()
    {
        return m_instance;
    }

    public SortedSet<string> GetMadeDecisions()
    {
        return m_madeDecisions;
    }

    public void LoadChoice(string choiceId)
    {
        var choicePath = Resources.Load<TextAsset>("Jsons/Choices/" + choiceId);
        m_currentChoice = JsonUtility.FromJson<VN_Choice>(choicePath.text);
        VN_Logger.Log("[VN_ChoiceModel][LoadChoice] Load New Choice: " + choiceId);
        if (m_choiceView)
        {
            m_choiceView.LoadChoice(m_currentChoice);
        }
    }

    public void OnChoiceCallback(string decisionId)
    {
        m_madeDecisions.Add(decisionId);
        VN_Logger.Log("[VN_ChoiceModel][OnChoiceCallback] Add decisionId: " + decisionId);
        if (m_choiceView)
        {
            m_choiceView.OnChoiceCallback();
        }
    }
}
