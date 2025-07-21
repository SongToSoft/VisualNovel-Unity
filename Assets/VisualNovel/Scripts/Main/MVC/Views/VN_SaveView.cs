using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VN_SaveView : MonoBehaviour
{
    [SerializeField] private GameObject m_saveWindow;

    public void OpenSaveWindow()
    {
        if (m_saveWindow)
        {
            m_saveWindow.SetActive(true);
        }
    }

    public void OnCloseSaveWindow()
    {
        if (m_saveWindow)
        {
            m_saveWindow.SetActive(false);
        }
    }
}
