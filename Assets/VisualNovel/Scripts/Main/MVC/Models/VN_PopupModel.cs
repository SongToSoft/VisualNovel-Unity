using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class VN_PopupModel : MonoBehaviour
{
    [SerializeField] private VN_PopupView m_popupView;

    private static VN_PopupModel m_instance;

    public void Awake()
    {
        m_instance = this;
    }

    public static VN_PopupModel Instance()
    {
        return m_instance;
    }

    public void OpenMessageBox(string message)
    {
        if (m_popupView)
        {
            m_popupView.OpenMessageBox(message);
        }
    }

    public void OpenHook(string imagePath)
    {
        if (m_popupView)
        {
            m_popupView.OpenHook(imagePath);
        }
    }

    public void OnCloseMessageBox()
    {
        if (m_popupView)
        {
            m_popupView.OnCloseMessageBox();
        }
    }

    public void OnCloseHook()
    {
        if (m_popupView)
        {
            m_popupView.OnCloseHook();
        }
    }
}
