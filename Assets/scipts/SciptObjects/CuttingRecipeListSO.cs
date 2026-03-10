using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]public class CuttingRecipe
{
    public KitchenObjectSO input;
    public KitchenObjectSO output;
    public int cuttingCountMax;
}
[CreateAssetMenu()]public class CuttingRecipeListSO :ScriptableObject
{
    public List<CuttingRecipe> list;
    public KitchenObjectSO GetOutput(KitchenObjectSO input)
    {
        foreach (CuttingRecipe racipe in list)
        {
            if (racipe.input == input)
            {
                return racipe.output;
            }
        }
        return null;
    }
    public bool TryGetCuttingRacipe(KitchenObjectSO input,out CuttingRecipe cuttingRacipe)
    {
        foreach (CuttingRecipe racipe in list)
        {
            if (racipe.input == input)
            {
                cuttingRacipe=racipe; return true;
            }
        }
        cuttingRacipe = null;
        return false;
    }
}