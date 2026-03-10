using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance{ get; private set; }

    private const string GAMINPUT_BINGINGS = "GameInputBindings";
    public event EventHandler OnIteractaction;//触发事件公开

    public event EventHandler OnOperateAction;//操作事件公开
    public event EventHandler OnPauseAction;//暂停事件公开
 
    private Gamecontrol gamecontrol;

    public enum BindingType
    {
        Up,
        Down,
        Left,
        Right,
        Interact,
        Operate,
        Pause
    }
    public void Awake()
    {
        Instance = this;
        gamecontrol =  new Gamecontrol();
        if(PlayerPrefs.HasKey(GAMINPUT_BINGINGS))
        {
            gamecontrol.LoadBindingOverridesFromJson(PlayerPrefs.GetString(GAMINPUT_BINGINGS));
        }

        gamecontrol.Player1.Enable();

        gamecontrol.Player1.Interact.performed += Interact_performed;
        gamecontrol.Player1.Operate.performed += Operate_performed;
        gamecontrol.Player1.Pause.performed += Pause_performed;
   
    }
    //private void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        print("开始绑定");
    //        gamecontrol.Player1.Disable();
    //        gamecontrol.Player1.move.PerformInteractiveRebinding(1).OnComplete(callback =>
    //        {
    //            print(callback.action.bindings[1].path);
    //            print(callback.action.bindings[1].overridePath);
    //            callback.Dispose();

    //            print("绑定完成");
    //            gamecontrol.Enable();
    //        }).Start();
    //    }
    //}
    public void ReBinding(BindingType bindingType,Action onComplete)
    {
        gamecontrol.Player1.Disable();
        InputAction inputAction = null;
        int index = -1;
        switch (bindingType)
        {
            case BindingType.Up:
                index = 1;
                inputAction=gamecontrol.Player1.move;
                break;
            case BindingType.Down:
                index = 2;
                inputAction = gamecontrol.Player1.move;
                break;
            case BindingType.Left:
                index = 3;
                inputAction = gamecontrol.Player1.move;
                break;
            case BindingType.Right:
                index = 4;
                inputAction = gamecontrol.Player1.move;
                break;
            case BindingType.Interact:
                index = 0;
                inputAction = gamecontrol.Player1.Interact;
                break;
            case BindingType.Operate:
                index = 0;
                inputAction = gamecontrol.Player1.Operate;
                break;
            case BindingType.Pause:
                index = 0;
                inputAction = gamecontrol.Player1.Pause;
                break;
            default:
                break;
        }
        inputAction.PerformInteractiveRebinding(index).OnComplete(callback =>
        {
            callback.Dispose();
            gamecontrol.Player1.Enable();
            onComplete?.Invoke();

            PlayerPrefs.SetString(GAMINPUT_BINGINGS, gamecontrol.SaveBindingOverridesAsJson());
            PlayerPrefs.Save();
        }).Start();
    }
    public string GetBindingDisplayString(BindingType bindingType)
    {
        switch (bindingType)
        {
            case BindingType.Up:
                return gamecontrol.Player1.move.bindings[1].ToDisplayString();
            case BindingType.Down:
                return gamecontrol.Player1.move.bindings[2].ToDisplayString();
            case BindingType.Left:
                return gamecontrol.Player1.move.bindings[3].ToDisplayString();
            case BindingType.Right:
                return gamecontrol.Player1.move.bindings[4].ToDisplayString();
            case BindingType.Interact:
                return gamecontrol.Player1.Interact.bindings[0].ToDisplayString();
            case BindingType.Operate:
                return gamecontrol.Player1.Operate.bindings[0].ToDisplayString();
            case BindingType.Pause:
                return gamecontrol.Player1.Pause.bindings[0].ToDisplayString();
            default:
                break;
        }
        return "";
    }
    //private void Start()
    //{
    //    print(gamecontrol.Player1.move.bindings[1].ToDisplayString());
    //    print(gamecontrol.Player1.move.bindings[2].ToDisplayString());
    //    print(gamecontrol.Player1.move.bindings[3].ToDisplayString());
    //    print(gamecontrol.Player1.move.bindings[4].ToDisplayString());
    //    print(gamecontrol.Player1.Interact.bindings[0].ToDisplayString());
    //}
    /// <summary>
    /// 资源销毁
    /// </summary>
    private void OnDestroy()
    {
        gamecontrol.Player1.Interact.performed -= Interact_performed;
        gamecontrol.Player1.Operate.performed -= Operate_performed;
        gamecontrol.Player1.Pause.performed -= Pause_performed;

        gamecontrol.Dispose();
    }
    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    private void Operate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnOperateAction?.Invoke(this, EventArgs.Empty);
    }

  
    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnIteractaction?.Invoke(this, EventArgs.Empty);
    }

    public Vector3 GetMovementDirectionNormalized()
    {
        //新版输入
        Vector2 inputVector2 = gamecontrol.Player1.move.ReadValue<Vector2>();

        //float horizontal = Input.GetAxisRaw("Horizontal");
        //float vertical = Input.GetAxisRaw("Vertical");旧版输入
        Vector3 direction = new Vector3(inputVector2.x, 0, inputVector2.y);

        direction = direction.normalized;//归一化
        return direction;
    }
}

 