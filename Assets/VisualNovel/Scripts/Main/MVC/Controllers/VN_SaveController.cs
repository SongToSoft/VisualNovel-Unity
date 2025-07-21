using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VN_SaveController : MonoBehaviour
{
    public void OnOpenSaveWindowClick()
    {
        VN_SaveModel.Instance().OpenSaveWindow();
    }

    public void OnSaveClick1()
    {
        VN_SaveService.SaveProfile("VN_Profile_1");
    }
    public void OnSaveClick2()
    {
        VN_SaveService.SaveProfile("VN_Profile_2");
    }

    public void OnSaveClick3()
    {
        VN_SaveService.SaveProfile("VN_Profile_3");
    }

    public void OnCloseSaveWindowClick()
    {
        VN_SaveModel.Instance().OnCloseSaveWindow();
    }

    public void OnLoadClick1()
    {
        VN_SaveModel.Instance().LoadProfile("VN_Profile_1");
    }
}
