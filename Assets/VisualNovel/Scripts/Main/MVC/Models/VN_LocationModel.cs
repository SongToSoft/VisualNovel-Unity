using UnityEngine;

public class VN_LocationModel : MonoBehaviour
{
    [SerializeField] private VN_LocationView m_locationView;

    private static VN_LocationModel m_instance;
    private string m_locationName;

    public void Awake()
    {
        m_instance = this;
    }

    public static VN_LocationModel Instance()
    {
        return m_instance;
    }

    public void SetLocation(string locationName)
    {
        m_locationName = locationName;
        if (m_locationView)
        {
            m_locationView.SetLocation(locationName);
        }
    }

    public string GetLocationName()
    {
        return m_locationName;
    }

    public void StartAnimate()
    {
        if (m_locationView)
        {
            m_locationView.StartAnimate();
        }
    }

    public void StopAnimate()
    {
        if (m_locationView)
        {
            m_locationView.StopAnimate();
        }
    }
}
