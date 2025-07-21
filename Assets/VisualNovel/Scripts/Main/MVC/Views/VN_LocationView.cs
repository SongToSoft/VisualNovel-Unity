using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class VN_LocationView : MonoBehaviour
{
    [SerializeField] private Image m_locationImage;
    [SerializeField] private float m_speed;
    [SerializeField] private float m_movableDistance;
    [SerializeField] private bool m_fadeEnabled = true;
    [SerializeField] private float m_animationDuration = 2f;
    [SerializeField] private float m_movementDistance = 1f;
    [SerializeField] private float m_fadeSpeed = 0.05f;

    private Vector3 m_defaultImagePosition;


    private int m_direction = -1;
    private Vector3 m_startPosition;
    private IEnumerator m_animateCoroutine;
    private string m_locationName = "";

    public void Start()
    {
        if (m_locationImage)
        {
            m_startPosition = m_locationImage.transform.position;
            m_animateCoroutine = Animate();
            m_defaultImagePosition = m_locationImage.transform.position;
        }
    }

    public void SetLocation(string locationName)
    {
        if (m_locationName == "" || !m_fadeEnabled)
        {
            m_locationName = locationName;
            m_locationImage.transform.position = m_startPosition;
            m_locationImage.sprite = Resources.Load<Sprite>("Sprites/Locations/" + m_locationName);
        }
        else
        {
            if (m_locationName != locationName)
            {
                m_locationName = locationName;
                VN_Logger.Log("[VN_LocationView][SetLocation] m_locationName: " + m_locationName);
                StopCoroutine(m_animateCoroutine);
                StartCoroutine(ChangeImage());
                //TODO: Add start animate if needed
            }
        }
    }

    public void StartAnimate()
    {
        StartCoroutine(m_animateCoroutine);
    }

    public void StopAnimate()
    {
        m_locationImage.transform.position = m_startPosition;
        StopCoroutine(m_animateCoroutine);
    }

    public IEnumerator Animate()
    {
        while (true)
        {
            if ((m_locationImage.transform.position.y > m_startPosition.y + m_movableDistance) ||
                (m_locationImage.transform.position.y < m_startPosition.y - m_movableDistance))
            {
                m_direction *= -1;
            }
            m_locationImage.transform.position = new Vector3(m_startPosition.x,
                                                             m_locationImage.transform.position.y + m_speed * m_direction * Time.deltaTime,
                                                             m_startPosition.z);
            yield return null;
        }
    }

    private IEnumerator ChangeImage()
    {
        float timer = 0f;

        m_locationImage.color = new Color(m_locationImage.color.r, m_locationImage.color.g, m_locationImage.color.b, 1);

        while (timer < m_animationDuration / 2)
        {
            timer += Time.deltaTime;
            m_locationImage.rectTransform.position += (m_movementDistance / (m_animationDuration / 2)) * Time.deltaTime * Vector3.up;
            m_locationImage.color -= new Color(0, 0, 0, m_fadeSpeed);
            yield return null;
        }

        m_locationImage.color = new Color(m_locationImage.color.r, m_locationImage.color.g, m_locationImage.color.b, 0);
        VN_Logger.Log("[VN_LocationView][ChangeImage] m_locationName: " + m_locationName);
        m_locationImage.sprite = Resources.Load<Sprite>("Sprites/Locations/" + m_locationName);

        while (timer < m_animationDuration)
        {
            timer += Time.deltaTime;
            m_locationImage.rectTransform.position -= (m_movementDistance / (m_animationDuration / 2)) * Time.deltaTime * Vector3.up;
            m_locationImage.color += new Color(0, 0, 0, m_fadeSpeed);
            yield return null;
        }

        m_locationImage.rectTransform.position = m_defaultImagePosition;
        m_locationImage.color = new Color(m_locationImage.color.r, m_locationImage.color.g, m_locationImage.color.b, 1);
    }
}
