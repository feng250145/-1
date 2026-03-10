using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;
    private enum State
    {
        WaittingToStart,
        CountDownToStart,
        GamePlaying,
        GameOver
    }

    [SerializeField] private player player;

    private State state;

    private float waittingToStartTimer = 1;
    private float countDownToStartTimer = 3;
    private float gamePlayingTimer = 90;
    private float gamePlayingTimeTotal;
    private bool isGamePause=false;
    void Awake()
    {
        Instance = this;
        gamePlayingTimeTotal= gamePlayingTimer;

    }
    private void Start()
    {
        TurntoWaittingToStart();
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e)
    {
        ToggleGame();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.WaittingToStart:
                waittingToStartTimer -= Time.deltaTime;
                if (waittingToStartTimer <= 0)
                {
                    TurntoCountDownToStart();
                }
                break;
            case State.CountDownToStart:
                countDownToStartTimer -= Time.deltaTime;
                if (countDownToStartTimer <= 0)
                {
                    TurntoGamePlaying();
                }
                break;
            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer <= 0)
                {
                    TurntoGameOver();
                }
                break;
            case State.GameOver:
                break;
            default:
                break;

        }
    }
    private void TurntoWaittingToStart()
    {
        state = State.WaittingToStart;
        DisablePlayer();
        OnStateChanged?.Invoke(this, new EventArgs());
    }
    private void TurntoCountDownToStart()
    {
        state = State.CountDownToStart;
        DisablePlayer();
        OnStateChanged?.Invoke(this, new EventArgs());
    }
    private void TurntoGamePlaying()
    {
        state = State.GamePlaying;
        EnablePlayer();
        OnStateChanged?.Invoke(this, new EventArgs());
    }
    private void TurntoGameOver()
    {
        state = State.GameOver;
        DisablePlayer();
        OnStateChanged?.Invoke(this, new EventArgs());
    }
    public void DisablePlayer()
    {
        player.enabled = false;
    }
    public void EnablePlayer()
    {
        player.enabled = true;
    }
    public bool IsWaitingToStartState()
    {
        return state == State.WaittingToStart;
    }
    public bool IsCountDownState()
    {
        return state == State.CountDownToStart;
    }
    public bool IsGamePlayingState()
    {
        return state == State.GamePlaying;
    }
    public bool IsGameOverState()
    {
        return state == State.GameOver;
    }
    public float GetCountDownTimer()
    {
        return countDownToStartTimer;
    }
    public void ToggleGame()
    {
        isGamePause = !isGamePause;
        if (isGamePause)
        {
            Time.timeScale = 0;
            OnGamePaused?.Invoke(this, new EventArgs());
        }
        else
        {
            Time.timeScale = 1;
            OnGameUnpaused?.Invoke(this, new EventArgs());
        }
    }
    public float GetGamePlayingTimer()
    {
        return gamePlayingTimer;
    }
    public float GetGamePlayingTimeNormalized()
    {
        return gamePlayingTimer / gamePlayingTimeTotal;
    }
}
