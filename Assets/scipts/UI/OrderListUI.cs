using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderListUI : MonoBehaviour
{
  [SerializeField] private Transform recipeParent;
  [SerializeField] private RecipeUI recipeUITemple;
    private void Start()
    {
        recipeUITemple.gameObject.SetActive(false);
        OrderManager.Instance.OnRecipeSpawned += OrderManager_OnRecipeSpawned;
        OrderManager.Instance.OnRecipeSuccessed += OrderManager_OnRecipeSuccessed; ;
    }

    private void OrderManager_OnRecipeSuccessed(object sender, System.EventArgs e)
    {
        UpdateUI();
    }

    private void OrderManager_OnRecipeSpawned(object sender, System.EventArgs e)
    {
       UpdateUI();
    }
    private void UpdateUI()
    {
        foreach(Transform child in recipeParent)
        {
            if(child!= recipeUITemple.transform)
            {
                Destroy(child.gameObject);
            }
        }
        List<RecipeSO> recipeSOList = OrderManager.Instance.GetOrderList();
        foreach(RecipeSO recipeSO in recipeSOList)
        {
            RecipeUI recipeUI = GameObject.Instantiate(recipeUITemple);
            recipeUI.transform.SetParent(recipeParent);
            recipeUI.gameObject.SetActive(true);
            recipeUI.UpdateUI(recipeSO);
        }
    }
}
