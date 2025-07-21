using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class VN_DialogView : MonoBehaviour
{
    [SerializeField] private GridLayoutGroup m_dialogGrid;
    [SerializeField] private GameObject m_arrowUp, m_arrowDown;
    [SerializeField] private GameObject m_textPrefab;

    [SerializeField] private float m_textDelay;
    [SerializeField] private bool m_clearOnDialogLoad;

    private Transform m_gridTransform;
    private int m_maxTextCount;
    private float m_textOffset = 0;
    private float m_currentDialogsPosition, m_startDialogPosition;
    private Vector3 m_defaultlocalGridPosition;
    private bool m_isDirty = false;

    public void Awake()
    {
        if (m_dialogGrid)
        {
            m_gridTransform = m_dialogGrid.GetComponent<Transform>();
            m_currentDialogsPosition = m_gridTransform.localPosition.y;
            m_startDialogPosition = m_gridTransform.localPosition.y;
            m_textOffset = m_dialogGrid.cellSize.y;
            m_defaultlocalGridPosition = m_gridTransform.localPosition;

            if (m_textOffset != 0)
            {
                RectTransform rectTransform = m_dialogGrid.GetComponent<RectTransform>();
                m_maxTextCount = (int)(rectTransform.rect.height / (int)m_textOffset) + 1;
                VN_Logger.Log("[VN_DialogView][Awake] maxTextCount: " + m_maxTextCount);
            }
        }
    }

    public void Start()
    {
        if (m_arrowUp)
        {
            m_arrowUp.SetActive(false);
        }
        if (m_arrowDown)
        {
            m_arrowDown.SetActive(false);
        }
    }

    public void SetTextDelay(float delay)
    {
        VN_Logger.Log("[VN_DialogView][SetTextDelay] delay: " + delay);
        m_textDelay = delay;
    }

    public void DialogDown()
    {
        if (m_gridTransform.localPosition.y != m_currentDialogsPosition)
        {
            m_gridTransform.localPosition = new Vector3(m_gridTransform.localPosition.x, m_gridTransform.localPosition.y + m_textOffset, m_gridTransform.localPosition.z);
        }
        CheckDialogArrows();
    }

    public void DialogUp()
    {
        if (m_gridTransform.localPosition.y != m_startDialogPosition)
        {
            m_gridTransform.localPosition = new Vector3(m_gridTransform.localPosition.x, m_gridTransform.localPosition.y - m_textOffset, m_gridTransform.localPosition.z);
        }
        CheckDialogArrows();
    }

    public void OnNextClick()
    {
        m_gridTransform.localPosition = new Vector3(m_gridTransform.localPosition.x, m_currentDialogsPosition, m_gridTransform.localPosition.z);
    }

    public void ClearDialog()
    {
        VN_Logger.Log("[VN_DialogView][ClearDialog] Clear Dialog View");
        m_isDirty = true;

        foreach (Transform child in m_gridTransform)
        {
            GameObject.Destroy(child.gameObject);
        }

        m_gridTransform.localPosition = m_defaultlocalGridPosition;
        m_currentDialogsPosition = m_gridTransform.localPosition.y;
        m_startDialogPosition = m_gridTransform.localPosition.y;

        CheckDialogArrows();
    }

    public void LoadDialog()
    {
        if (m_clearOnDialogLoad)
        {
            ClearDialog();
        }
    }

    public void NextReplica(Color32 textColor, string text)
    {
        if (m_textPrefab == null)
        {
            return;
        }

        VN_Logger.Log("[VN_DialogView][NextReplica] add new replica: " + text + ", gridTransform.childCount: " + m_gridTransform.childCount);
        var textObject = Instantiate(m_textPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        textObject.transform.SetParent(m_dialogGrid.transform, false);
        if (m_gridTransform.childCount > m_maxTextCount && !m_isDirty)
        {
            m_gridTransform.localPosition = new Vector3(m_gridTransform.localPosition.x, m_gridTransform.localPosition.y + m_textOffset, m_gridTransform.localPosition.z);
        }
        m_currentDialogsPosition = m_gridTransform.localPosition.y;

        var textMesh = textObject.GetComponent<TextMeshProUGUI>();
        textMesh.color = textColor;
        textMesh.text = "> ";

        StartCoroutine(TextAnimation(textMesh, text));
        CheckDialogArrows();
        m_isDirty = false;
    }

    private void CheckDialogArrows()
    {
        m_arrowUp.SetActive(m_gridTransform.localPosition.y != m_startDialogPosition);
        m_arrowDown.SetActive(m_gridTransform.localPosition.y != m_currentDialogsPosition);
    }

    private IEnumerator TextAnimation(TextMeshProUGUI textMesh, string text)
    {
        foreach (var ch in text)
        {
            textMesh.text += ch;
            yield return new WaitForSeconds(m_textDelay);
        }
    }
}
