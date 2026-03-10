using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class StoveCounter : BaseCounter
{
    [SerializeField] private FryingRecipeListSO fryingRecipeListSO;
    [SerializeField] private FryingRecipeListSO burningRecipeListSO;
    [SerializeField] private StoveCounterVisual stoveCounterVisual;
    [SerializeField] private ProgressBarUI progressBarUI;
    [SerializeField] private AudioSource sound;
    public enum StoveState
    {
        Idle,
        Frying,
        Burning
    }
    private FryingRecipe fryingRecipe;
    private float fryingTimer = 0;
    private StoveState state = StoveState.Idle;
    private WarningCounter warningCounter;


    private void Start()
    {
        warningCounter =GetComponent<WarningCounter>();
    }
    public override void Interact(player player)
    {
        if (player.IsHaveKitchenObject())
        {//手上有食材
            if (IsHaveKitchenObject() == false)
            {//当前柜台为空
                if (fryingRecipeListSO.TryGetFryingRacipe(
                player.GetKitchenObject().GetKitchenObjectSO(), out FryingRecipe fryingRecipe))
                {
                    TransferKitchenObject(player, this);
                    StartFrying(fryingRecipe);
                }
                else if (burningRecipeListSO.TryGetFryingRacipe(
                player.GetKitchenObject().GetKitchenObjectSO(), out FryingRecipe burningRecipe))
                {
                    TransferKitchenObject(player, this);
                    StartBurning(burningRecipe);
                }
                else
                {

                }
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
                TurnToIdle();
                TransferKitchenObject(this, player);
            }
        }
    }
    private void Update()
    {
        switch (state)
        {
            case StoveState.Idle:
                break;
            case StoveState.Frying:
                fryingTimer += Time.deltaTime;
                progressBarUI.UpdateProgress((float)fryingTimer / fryingRecipe.fryingTime);
                if (fryingTimer >= fryingRecipe.fryingTime)
                {
                    DestroyKitchenObject();
                    CreateKitchenObject(fryingRecipe.output.prefab);
                    state = StoveState.Burning;

                    burningRecipeListSO.TryGetFryingRacipe(GetKitchenObject().GetKitchenObjectSO(),
                        out FryingRecipe newfryingRecipe);
                    StartBurning(newfryingRecipe);
                }
                break;
            case StoveState.Burning:
                fryingTimer += Time.deltaTime;
                progressBarUI.UpdateProgress((float)fryingTimer / fryingRecipe.fryingTime);
                float warningTimeNormalize = .5f;
                if (fryingTimer / fryingRecipe.fryingTime > warningTimeNormalize)
                {
                    warningCounter.ShowWarning();
                }
                if (fryingTimer >= fryingRecipe.fryingTime)
                {
                    DestroyKitchenObject();
                    CreateKitchenObject(fryingRecipe.output.prefab);
                    //设置起火状态记得
                    TurnToIdle();
                }
                 break;
            default:
                break;
        }
    }
    private void StartFrying(FryingRecipe fryingRecipe)
    {
        fryingTimer = 0;
        this.fryingRecipe = fryingRecipe;
        state = StoveState.Frying;
        stoveCounterVisual.ShowStoveEffect();
        sound.Play();
    }
    private void StartBurning(FryingRecipe fryingRecipe)
    {
        if(fryingRecipe == null)
        {
            UnityEngine.Debug.LogWarning("无法获取Buring的食谱，无法进行Buring喵~");
            TurnToIdle();
            return;
        }
        stoveCounterVisual.ShowStoveEffect();
        fryingTimer = 0;
        this.fryingRecipe = fryingRecipe;
        state = StoveState.Burning;
        sound.Play();
    }
    private void TurnToIdle()
    {
        progressBarUI.Hide();
        state = StoveState.Idle;
        stoveCounterVisual.HideStoveEffect();
        sound.Pause();
        warningCounter.StopWarning();
    }
}
