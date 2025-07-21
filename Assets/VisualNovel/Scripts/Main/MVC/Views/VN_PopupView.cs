using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VN_PopupView : MonoBehaviour
{
    [SerializeField] private GameObject m_messageBoxPanel;
    [SerializeField] private TMP_Text m_messageBoxText;

    [SerializeField] private GameObject m_hookPanel;
    [SerializeField] private Image m_hookImage;

    public void OpenMessageBox(string message)
    {
        if (m_messageBoxText && m_messageBoxPanel)
        {
            m_messageBoxText.SetText(message);
            m_messageBoxPanel.SetActive(true);
        }
    }

    public void OnCloseMessageBox()
    {
        if (m_messageBoxPanel)
        {
            m_messageBoxPanel.SetActive(false);
        }
    }

    public void OpenHook(string imagePath)
    {
        if (m_hookImage && m_hookPanel)
        {
            m_hookImage.sprite = Resources.Load<Sprite>(imagePath);
            m_hookPanel.SetActive(true);
        }
    }

    public void OnCloseHook()
    {
        if (m_hookImage)
        {
            m_hookPanel.SetActive(false);
        }
    }
}
