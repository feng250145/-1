using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ≤÷ø‚¿‡πÒÃ®
/// </summary>
public class ContainerCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    [SerializeField] private ContainerCounterVisual containerCounterVisual;

   
    public override void Interact(player player)
    {
        if (player.IsHaveKitchenObject()) return;

        CreateKitchenObject(kitchenObjectSO.prefab);
        TransferKitchenObject(this, player);

        containerCounterVisual.PlayOpen();
    }
 
}
