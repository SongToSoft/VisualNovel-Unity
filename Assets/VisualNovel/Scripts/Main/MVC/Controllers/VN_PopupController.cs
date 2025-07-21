using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VN_PopupController : MonoBehaviour
{
    public void OnMessageBoxClose()
    {
        VN_PopupModel.Instance().OnCloseMessageBox();
    }

    public void OnHookClose()
    {
        VN_PopupModel.Instance().OnCloseHook();
    }
}
