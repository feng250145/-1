using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public override void Interact(player player)
    {
        if (player.IsHaveKitchenObject()
            && player.GetKitchenObject()
            .TryGetComponent<PlateKitchenObject>(out PlateKitchenObject plateKitchenObject))
        {
            //Todo 判断上菜是否正确
            OrderManager.Instance.DeliveryRecipe(plateKitchenObject);
            player.DestroyKitchenObject();

        }
    }
}
