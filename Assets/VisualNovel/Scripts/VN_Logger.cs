using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VN_Logger : MonoBehaviour
{

    [SerializeField] private bool m_turnOn = true;

    private static bool m_isEnabled = true;

    public static void Log(string message)
    {
        if (m_isEnabled)
        {
            Debug.Log(message);
        }
    }

    private void OnValidate()
    {
        m_isEnabled = m_turnOn;
    }
}
