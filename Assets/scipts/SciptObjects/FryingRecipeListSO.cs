using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class FryingRecipeListSO : ScriptableObject
{
    public List<FryingRecipe> list;

    public bool TryGetFryingRacipe(KitchenObjectSO input, out FryingRecipe fryingRecipe)
    {
        foreach (FryingRecipe racipe in list)
        {
            if (racipe.input == input)
            {
                fryingRecipe = racipe; return true;
            }
        }
        fryingRecipe = null;
        return false;
    }
}


[Serializable]
public class FryingRecipe
{
    public KitchenObjectSO input;
    public KitchenObjectSO output;
    public float fryingTime;
}