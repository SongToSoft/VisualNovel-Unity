using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class VN_HistoryView : MonoBehaviour
{
    [SerializeField] private GameObject m_historyPanel;
    [SerializeField] private GridLayoutGroup m_historyGrid;
    [SerializeField] private GameObject m_textPrefab;
    [SerializeField] private GameObject m_arrowUp, m_arrowDown;

    private Transform m_gridTransform;
    private int m_maxTextCount;
    private float m_currentHistoryPosition, m_startHistoryPosition;
    private float m_textOffset;

    public void Awake()
    {
        m_gridTransform = m_historyGrid.GetComponent<Transform>();
        m_currentHistoryPosition = m_gridTransform.localPosition.y;
        m_startHistoryPosition = m_gridTransform.localPosition.y;
        m_textOffset = m_historyGrid.cellSize.y;

        if (m_textOffset != 0)
        {
            RectTransform rectTransform = m_historyGrid.GetComponent<RectTransform>();
            m_maxTextCount = (int)(rectTransform.rect.height / (int)m_textOffset) + 1;
            VN_Logger.Log("[VN_HistoryView][Awake] maxTextCount: " + m_maxTextCount);
        }
    }

    public void OnHistoryButtonClick(List<string> textes)
    {
        m_historyPanel.SetActive(true);
        foreach (var text in textes)
        {
            var textObject = Instantiate(m_textPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            textObject.transform.SetParent(m_historyGrid.transform, false);
            if (m_gridTransform.childCount > m_maxTextCount)
            {
                m_gridTransform.localPosition = new Vector3(m_gridTransform.localPosition.x, m_gridTransform.localPosition.y + m_textOffset, m_gridTransform.localPosition.z);
            }
            m_currentHistoryPosition = m_gridTransform.localPosition.y;
            var textMesh = textObject.GetComponent<TextMeshProUGUI>();
            textMesh.text = "> " + text;
        }
        CheckHistoryArrows();
    }

    public void OnHistoryDown()
    {
        if (m_gridTransform.localPosition.y != m_currentHistoryPosition)
        {
            m_gridTransform.localPosition = new Vector3(m_gridTransform.localPosition.x, m_gridTransform.localPosition.y + m_textOffset, m_gridTransform.localPosition.z);
        }
        CheckHistoryArrows();
    }

    public void OnHistoryUp()
    {
        if (m_gridTransform.localPosition.y != m_startHistoryPosition)
        {
            m_gridTransform.localPosition = new Vector3(m_gridTransform.localPosition.x, m_gridTransform.localPosition.y - m_textOffset, m_gridTransform.localPosition.z);
        }
        CheckHistoryArrows();
    }

    public void OnCloseHistoryButtonClick()
    {
        foreach (Transform child in m_historyGrid.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        m_historyPanel.SetActive(false);
    }

    private void CheckHistoryArrows()
    {
        m_arrowUp.SetActive(m_gridTransform.localPosition.y != m_startHistoryPosition);
        m_arrowDown.SetActive(m_gridTransform.localPosition.y != m_currentHistoryPosition);
    }
}
