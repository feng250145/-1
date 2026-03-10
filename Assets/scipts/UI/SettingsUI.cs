using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class SettingsUI : MonoBehaviour
{
    public static SettingsUI Instance { get;private set; }
    [SerializeField] private GameObject uiparent;

    [SerializeField] private Button musicButton;
    [SerializeField] private TextMeshProUGUI musicButtonText;
    [SerializeField] private Button soundButton;
    [SerializeField] private TextMeshProUGUI soundButtonText;
    [SerializeField] private Button closeButton;

    [SerializeField] private Button UpButton;
    [SerializeField] private Button DownButton;
    [SerializeField] private Button LeftButton;
    [SerializeField] private Button RightButton;
    [SerializeField] private Button InteractButton;
    [SerializeField] private Button OperateButton;
    [SerializeField] private Button PauseButton;

    [SerializeField] private TextMeshProUGUI UpButtonText;
    [SerializeField] private TextMeshProUGUI DownButtonText;
    [SerializeField] private TextMeshProUGUI LeftButtonText;
    [SerializeField] private TextMeshProUGUI RightButtonText;
    [SerializeField] private TextMeshProUGUI InteractButtonText;
    [SerializeField] private TextMeshProUGUI OperateButtonText;
    [SerializeField] private TextMeshProUGUI PauseButtonText;

    [SerializeField] private GameObject rebindingHint;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        Hide();
        UpdateVisual();
        soundButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        musicButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        closeButton.onClick.AddListener(() =>
        {
            Hide();
        });
        UpButton.onClick.AddListener(() =>{ReBinding(GameInput.BindingType.Up); });
        DownButton.onClick.AddListener(() => { ReBinding(GameInput.BindingType.Down); });
        LeftButton.onClick.AddListener(() => { ReBinding(GameInput.BindingType.Left); });
        RightButton.onClick.AddListener(() => { ReBinding(GameInput.BindingType.Right); });
        InteractButton.onClick.AddListener(() => { ReBinding(GameInput.BindingType.Interact); });
        OperateButton.onClick.AddListener(() => { ReBinding(GameInput.BindingType.Operate); });
        PauseButton.onClick.AddListener(() => { ReBinding(GameInput.BindingType.Pause); });
    }
    public void Show()
    {
        uiparent.SetActive(true);
    }
    private void Hide()
    {
        uiparent.SetActive(false);
    }
    private void UpdateVisual()
    {
        soundButtonText.text = "音效大小:" + SoundManager.Instance.GetVolume();
        musicButtonText.text = "音乐大小:" + MusicManager.Instance.GetVolume();

        UpButtonText.text =GameInput.Instance.GetBindingDisplayString(GameInput.BindingType.Up);
        DownButtonText.text = GameInput.Instance.GetBindingDisplayString(GameInput.BindingType.Down);
        LeftButtonText.text = GameInput.Instance.GetBindingDisplayString(GameInput.BindingType.Left);
        RightButtonText.text = GameInput.Instance.GetBindingDisplayString(GameInput.BindingType.Right);
        InteractButtonText.text = GameInput.Instance.GetBindingDisplayString(GameInput.BindingType.Interact);
        OperateButtonText.text = GameInput.Instance.GetBindingDisplayString(GameInput.BindingType.Operate);
        PauseButtonText.text = GameInput.Instance.GetBindingDisplayString(GameInput.BindingType.Pause);
    }
    private void ReBinding(GameInput.BindingType bindingType)
    {
        rebindingHint.SetActive(true);
        GameInput.Instance.ReBinding(bindingType,()=>
        {
            rebindingHint.SetActive(false);
            UpdateVisual();
        });
    }
}
