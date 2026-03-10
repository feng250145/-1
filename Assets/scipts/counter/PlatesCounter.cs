using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlatesCounter : BaseCounter
{
    [SerializeField] private float spawnRate = 3;
    [SerializeField] private KitchenObjectSO plateSO;
    [SerializeField] private int plateCountMax = 5;
    private List<KitchenObject> platesList = new List<KitchenObject>();

    private float timer = 0;
    
    private void Update()
    {
        if(platesList.Count < plateCountMax)
        {
            timer += Time.deltaTime;
        }
        
        if ( timer > spawnRate)
        {
            timer = 0;
            SpawPlate();
        }
    }
    public override void Interact(player player)
    {
        if (player.IsHaveKitchenObject()==false)
        {//手上没食材
            if(platesList.Count > 0)
            {
                player.AddKitchenObject(platesList[platesList.Count-1]);
                platesList.RemoveAt(platesList.Count-1);
            }
        }
    }

   
    public void SpawPlate()
    {
        if (platesList.Count > plateCountMax)
        {
            timer = 0;
            return;
        }
        KitchenObject kitchenObject = GameObject.Instantiate(plateSO.prefab, GetHoldPoint()).GetComponent<KitchenObject>();
      
        kitchenObject.transform.localPosition= Vector3.up * 0.1f * platesList.Count;
        platesList.Add(kitchenObject);

    }
}
