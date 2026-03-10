using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private GameObject uiParent;
    [SerializeField] private TextMeshProUGUI upText;
    [SerializeField] private TextMeshProUGUI downText;
    [SerializeField] private TextMeshProUGUI leftText;
    [SerializeField] private TextMeshProUGUI rightText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI operateText;
    [SerializeField] private TextMeshProUGUI pauseText;
    // Start is called before the first frame update
    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        Show();
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsWaitingToStartState())
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Show()
    {
        UpdateVisual();
        uiParent.SetActive(true);
    }
    private void Hide()
    {
        uiParent.SetActive(false);
    }
    private void UpdateVisual()
    {
        upText.text = GameInput.Instance.GetBindingDisplayString(GameInput.BindingType.Up);
        downText.text = GameInput.Instance.GetBindingDisplayString(GameInput.BindingType.Down);
        leftText.text = GameInput.Instance.GetBindingDisplayString(GameInput.BindingType.Left);
        rightText.text = GameInput.Instance.GetBindingDisplayString(GameInput.BindingType.Right);
        interactText.text = GameInput.Instance.GetBindingDisplayString(GameInput.BindingType.Interact);
        operateText.text = GameInput.Instance.GetBindingDisplayString(GameInput.BindingType.Operate);
        pauseText.text = GameInput.Instance.GetBindingDisplayString(GameInput.BindingType.Pause);
    }
}
