using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VN_DialogController : MonoBehaviour
{
    [SerializeField] private Button nextDialogButton;
    [SerializeField] private float disableTime;

    public void Start()
    {
        StartDisable();
    }

    public void StartDisable()
    {
        StartCoroutine(TimeoutEndTurnButton());
    }

    IEnumerator TimeoutEndTurnButton()
    {
        nextDialogButton.interactable = false;
        yield return new WaitForSeconds(disableTime);
        nextDialogButton.interactable = true;
    }

    public void DialogDown()
    {
        VN_DialogModel.Instance().DialogDown();
    }

    public void DialogUp()
    {
        VN_DialogModel.Instance().DialogUp();
    }

    public void OnNextClick()
    {
        VN_DialogModel.Instance().OnNextClick();
        StartDisable();
    }
}
