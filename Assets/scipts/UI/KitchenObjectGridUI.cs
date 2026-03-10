using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObjectGridUI : MonoBehaviour
{
    [SerializeField]private KitchenObjectIconUI iconTeplateUI;

    private void Start()
    {
        iconTeplateUI.Hide();
    }
    public void ShowKitchenObjectUI(KitchenObjectSO kitchenObjectSO)
    {
        KitchenObjectIconUI newIconUI = GameObject.Instantiate(iconTeplateUI,transform);
        newIconUI.Show(kitchenObjectSO.sprite);
    }
}
