using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{

    [Serializable]
    public class KitchenObjectSO_Model
    {
        public KitchenObjectSO kitchenObjectSO;
        public GameObject model;
    }
    [SerializeField]private List<KitchenObjectSO_Model> modelMaps;
    public void ShowKitchenObject(KitchenObjectSO kitchenObjectSO)
    {
        foreach (KitchenObjectSO_Model item in modelMaps)
        {
            if(item.kitchenObjectSO == kitchenObjectSO)
            {
                item.model.SetActive(true);return;
            }
        }
    }
}
