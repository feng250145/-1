using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter :KitchenObjectHolder
{
    [SerializeField] private GameObject selectedCounter;
    public virtual void Interact(player player)
    {
        Debug.LogWarning("交互方法没有重写");

    }
    public virtual void InteractOperate(player player)
    {

    }
    
    public void SelectCounter()
    {
        selectedCounter.SetActive(true);
    }

    public void CancelSelect()
    {
        selectedCounter.SetActive(false);
    }
 
}
