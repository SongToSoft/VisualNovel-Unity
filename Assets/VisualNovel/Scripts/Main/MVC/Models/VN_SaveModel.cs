using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class VN_SaveModel : MonoBehaviour
{
    [SerializeField] private VN_SaveView m_saveView;

    private static VN_SaveModel m_instance;

    public void Awake()
    {
        m_instance = this;
    }

    public static VN_SaveModel Instance()
    {
        return m_instance;
    }

    public void OpenSaveWindow()
    {
        if (m_saveView)
        {
            m_saveView.OpenSaveWindow();
        }
    }

    public void OnCloseSaveWindow()
    {
        if (m_saveView)
        {
            m_saveView.OnCloseSaveWindow();
        }
    }

    public void LoadProfile(string fileName)
    {
        VN_SaveService.LoadProfile(fileName);
    }
}
