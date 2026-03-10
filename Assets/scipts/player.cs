using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class player : KitchenObjectHolder
{
    public static player Instance { get; private set; }
    [SerializeField] private float rotatespeed = 10;
    [SerializeField] private float movespeed = 6;
    [SerializeField] private float lastspeed = 7;
    [Header("输入设置")]
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;


    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask counterLayerMask;
   
    
    private bool isWalking = false;
    private BaseCounter selectedCounter;
  
    private void Start()
    {
        gameInput.OnIteractaction += GameInput_OnInteractAction;
        gameInput.OnOperateAction += GameInput_OnOperateAction;
       
    }

  

    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        HandleInteraction();
      
            // 检测Shift键是否按下
            if (Input.GetKey(sprintKey))
            {
                movespeed = rotatespeed; // 按下时使用冲刺速度
            }
            else
            {
                movespeed = lastspeed; // 松开时恢复正常速度
            }
        }
    private void FixedUpdate()
    {
        HandleMovement();
    }

    public bool IsWalking
    {  get 
        { 
            return isWalking; 
        }
    }
    /// <summary>
    /// 当按下操作键f时，调用HandleOperate方法
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <exception cref="System.NotImplementedException"></exception>
  

    private void GameInput_OnOperateAction(object sender, System.EventArgs e)
    {
        selectedCounter?.InteractOperate(this);
    }

    /// <summary>
    /// 当按下e键时，调用HandleInteraction方法
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        selectedCounter?.Interact(this);
    }
   
    private void HandleMovement()
    {
        Vector3 direction = gameInput.GetMovementDirectionNormalized();

        isWalking = direction != Vector3.zero;

        transform.position += direction * Time.deltaTime * movespeed;

        if (direction != Vector3.zero)
        {
            transform.forward = Vector3.Slerp(transform.forward, direction, Time.deltaTime * rotatespeed);
        }
    }
    /// <summary>
    /// 处理柜台选择跟取消选择的状态
    /// </summary>
    private void HandleInteraction()
    {
        //检测物体前面的类别
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitinfo, 2f,counterLayerMask))
        {
             //判断是否有撞到clearcounter
             if(hitinfo.transform.TryGetComponent<BaseCounter>(out BaseCounter counter))
                { 
                    //counter.Interact();
                    SetSelectedCounter(counter);
                }
             else
            {
                SetSelectedCounter(null);
            }  
        }
        else
        { 
            SetSelectedCounter(null); 
        }
    }
   
    public void SetSelectedCounter(BaseCounter counter)
    {
        //如果检测柜台发生改变则取消上一个柜台的选择，并选择新的柜台
        if(counter!= selectedCounter)
        {
            selectedCounter?.CancelSelect();
            counter?.SelectCounter();

            this.selectedCounter = counter;
        }
        
    }
}
