using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CuttingCounter : BaseCounter
{
    public static event EventHandler OnCut;

    [SerializeField]private CuttingRecipeListSO cuttingRecipeListSO;

    [SerializeField]private ProgressBarUI progressBarUI;
    [SerializeField]private CuttingCounterVisual cuttingCounterVisual;
    private int cuttingCount = 0;
    public override void Interact(player player)
    {
        if (player.IsHaveKitchenObject())
        {//手上有食材
            if (IsHaveKitchenObject() == false)
            {//当前柜台为空
                cuttingCount = 0;
                TransferKitchenObject(player, this);

            }
            else
            {//当前柜台有食材

            }
        }
        else
        {//手上没食材
            if (IsHaveKitchenObject() == false)
            {//当前柜台为空


            }
            else
            {//当前柜台有食材
                TransferKitchenObject(this, player);
                progressBarUI.Hide();
            }
        }
    }
    public override void InteractOperate(player player)
    {
       if(IsHaveKitchenObject() == true)
        {

            if (cuttingRecipeListSO.TryGetCuttingRacipe(GetKitchenObject().GetKitchenObjectSO(), out CuttingRecipe cuttingRecipe))
            {
                Cut();

                progressBarUI.UpdateProgress((float)cuttingCount / cuttingRecipe.cuttingCountMax);

                if (cuttingCount == cuttingRecipe.cuttingCountMax)
                {
                    DestroyKitchenObject();
                    CreateKitchenObject(cuttingRecipe.output.prefab);
                }

            }
        }
    }


    public void Cut()
    {
        OnCut?.Invoke(this, EventArgs.Empty);
        cuttingCount++;
        cuttingCounterVisual.PlayCut();
    }
    public static void ClearStaticData()
    {
        OnCut = null;
    }
}
