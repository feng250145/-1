using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    public  override void Interact(player player)
    {
       if(player.IsHaveKitchenObject())
        {//手上有食材KitchenObject

            if(player.GetKitchenObject()
                .TryGetComponent<PlateKitchenObject>(out PlateKitchenObject plateKitchenObject))
            {//手上有盘子
                if (IsHaveKitchenObject() == false)
                {//当前柜台为空
                    TransferKitchenObject(player, this);

                }
                else
                {//当前柜台有食材
                    bool isSuccess =plateKitchenObject.AddKitchenObjectSO(GetKitchenObjectSO());
                    if(isSuccess)
                    {
                        DestroyKitchenObject();
                    }
                    
                }
            }
            else
            {//手上是普通食材
                if (IsHaveKitchenObject() == false)
                {//当前柜台为空
                    TransferKitchenObject(player, this);

                }
                else
                {//当前柜台有食材
                    if (GetKitchenObject().TryGetComponent<PlateKitchenObject>(out plateKitchenObject))
                    {

                        if (plateKitchenObject.AddKitchenObjectSO(player.GetKitchenObjectSO()))
                        {
                            player.DestroyKitchenObject();
                        }
                    }
                }
            }
            //if(IsHaveKitchenObject()==false)
            //{//当前柜台为空
            //    TransferKitchenObject(player,this);

            //}
            //else
            //{//当前柜台有食材

            //}
        }
       else
        {//手上没食材
            if (IsHaveKitchenObject()==false)
            {//当前柜台为空

              
            }
            else
            {//当前柜台有食材
                TransferKitchenObject(this, player);
            }
        }
    }
   
}
