using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class LookAtCamara : MonoBehaviour
{
    public enum Mode
    {
        LookAt,
        LookInverted,
        CamaraForward,
        CamaraForwardInverted
    }

    [SerializeField]public Mode mode;
    void Update()
    {
        switch (mode)
        {
            case Mode.LookAt:
                transform.LookAt(Camera.main.transform);
                break;
            case Mode.LookInverted:
                transform.LookAt(transform.position - Camera.main.transform.position+transform.position);
                break;
            case Mode.CamaraForward:
                transform.forward = Camera.main.transform.forward;
                break;
            case Mode.CamaraForwardInverted:
                transform.forward = -Camera.main.transform.forward;
                break;
            default:
                break;
        }
       
  
    }
}
  
