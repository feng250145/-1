using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    public static event EventHandler OnObjectTrash;
    public override void Interact(player player)
    {
        if(player.IsHaveKitchenObject())
        {
            player.DestroyKitchenObject();
            OnObjectTrash?.Invoke(this, EventArgs.Empty);
        }
    }
    public static void ClearStaticData()
    {
        OnObjectTrash = null;
    }
}
