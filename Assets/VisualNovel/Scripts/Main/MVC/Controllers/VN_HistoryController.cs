using UnityEngine;

public class VN_HistoryController : MonoBehaviour
{
    public void OnHistoryButtonClick()
    {
        VN_HistoryModel.Instance().OnHistoryButtonClick();
    }

    public void OnHistoryDown()
    {
        VN_HistoryModel.Instance().OnHistoryDown();
    }

    public void OnHistoryUp()
    {
        VN_HistoryModel.Instance().OnHistoryUp();
    }

    public void OnCloseHistoryButtonClick()
    {
        VN_HistoryModel.Instance().OnCloseHistoryButtonClick();
    }
}
