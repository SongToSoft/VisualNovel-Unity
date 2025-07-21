using UnityEngine.SceneManagement;
using UnityEngine;

public class VN_ButtonsController : MonoBehaviour
{
    [SerializeField] private string m_mainSceneName;
    [SerializeField] private string m_menuSceneName;

    public void OnPlayClick()
    {
        VN_SaveService.ResetProfile();
        SceneManager.LoadScene(m_mainSceneName);
    }

    public void OnLoadClick()
    {
        VN_SaveService.LoadProfile("VN_Profile_1");
        SceneManager.LoadScene(m_mainSceneName);
    }

    public void OnMenuClick()
    {
        SceneManager.LoadScene(m_menuSceneName);
    }

    public void OnExitClick()
    {
        Application.Quit();
    }
}
