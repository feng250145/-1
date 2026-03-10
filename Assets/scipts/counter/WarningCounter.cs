using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WarningCounter : MonoBehaviour
{
    private const string IS_Flicker="IsFlicker";
    [SerializeField]private GameObject warningUI;
    [SerializeField]private Animator progressBarAnimator;

    private bool isWarning = false;
    private float warningSoundRate = .2f;
    private float warningSoundTimer = 0f;
    private void Update()
    {
        if (isWarning)
        {
            warningSoundTimer += Time.deltaTime;
            if (warningSoundTimer > warningSoundRate)
            {
                warningSoundTimer = 0;
                SoundManager.Instance.PlayWarningSound();
            }
        }
    }
    public void ShowWarning()
    {
        if(isWarning==false)
        {
            isWarning = true;
            warningUI.SetActive(true);
            progressBarAnimator.SetBool(IS_Flicker, true);
        }
     
    }
    public void StopWarning()
    {
        isWarning = false;
        warningUI.SetActive(false);
        progressBarAnimator.SetBool(IS_Flicker, false);
    }
}
