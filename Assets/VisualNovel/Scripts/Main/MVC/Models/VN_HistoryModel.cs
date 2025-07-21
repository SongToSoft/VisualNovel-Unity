using System.Collections.Generic;
using UnityEngine;

public class VN_HistoryModel : MonoBehaviour
{
    [SerializeField] private VN_HistoryView m_historyView;

    private List<string> m_textes;

    private static VN_HistoryModel m_instance;

    public void Awake()
    {
        m_instance = this;
        m_textes = new List<string>();
    }

    public static VN_HistoryModel Instance()
    {
        return m_instance;
    }

    public void OnHistoryButtonClick()
    {
        if (m_historyView)
        {
            m_historyView.OnHistoryButtonClick(m_textes);
        }
    }

    public void OnCloseHistoryButtonClick()
    {
        if (m_historyView)
        {
            m_historyView.OnCloseHistoryButtonClick();
        }
    }

    public void AddText(string text)
    {
        m_textes.Add(text);
    }

    public void OnHistoryDown()
    {
        if (m_historyView)
        {
            m_historyView.OnHistoryDown();
        }
    }

    public void OnHistoryUp()
    {
        if (m_historyView)
        {
            m_historyView.OnHistoryUp();
        }
    }
}
