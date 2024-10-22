using Photon.Deterministic;
using Quantum;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public unsafe class UIView : QuantumCallbacks
{
    /*private enum GameStateEnum
    {
        WaitingForPlayers,
        Countdown,
        GameStarted,
        GameOver
    }*/

    public TextMeshProUGUI CountDownTime;
    public TextMeshProUGUI Winner;
    private QuantumGame _game;
    private FP countdownTime = 3;
    private FP gameTime = 60;
    private bool isFirstTime = true;
    private Dictionary<string, int> playerScoreDict = new Dictionary<string, int>();
    private Frame f;
    private GameSession _session;

    public override void OnUnitySceneLoadDone(QuantumGame game)
    {
        _game = game;
    }

    public void Start()
    {
        f = _game.Frames.Verified;
        _session = f.GetSingleton<GameSession>();
    }

    public void Update()
    {
        f = _game.Frames.Verified;
        _session = f.GetSingleton<GameSession>();
        countdownTime = _session.TimeStart;
        gameTime = _session.GameTime;
        switch (_session.State)
        {
            case GameState.WaitingForPlayers:
                break;

            case GameState.Countdown:
                UpdateCountdown();
                break;

            case GameState.GameStarted:
                if (f.IsVerified && isFirstTime)
                {
                    isFirstTime = false;
                    CoinSystem.SpawnCoin(f);
                }
                UpdateGameTimer();
                break;

            case GameState.GameOver:
                EndGame();
                break;
        }
    }

    private void UpdateCountdown(/*Frame f*/)
    {
        //countdownTime -= f.DeltaTime; // ลดค่า countdown ตามเวลาที่ผ่านไปในแต่ละ Frame

        SetText(countdownTime);
        if (countdownTime < 0)
        {
            SetText("GO!!!");
            //StartGame(f);
        }
        else
        {
            //Debug.Log($"Countdown: {countdownTime}");
        }
    }

    /*private void StartGame(Frame f)
    {
        currentState = GameState.GameStarted;
        gameTime = 60;
        Debug.Log("Game started! 60 seconds remaining.");

    }*/

    private void UpdateGameTimer(/*Frame f*/)
    {
        SetText(gameTime);
        if (gameTime < 0)
        {
           
            EndGame();//f);
        }
        else
        {
            //Debug.Log($"Game time remaining: {gameTime}");
        }
    }

    private void EndGame(/*Frame f*/)
    {
        SetText("Gameover!!!");
        Debug.Log("Game over! Time's up.");
        var playerFilter = f.Filter<PlayerLink>();
        while (playerFilter.Next(out var entity, out var playerLink))
        {
            var playerName = f.GetPlayerData(playerLink.Player).PlayerNickname;
            var playerScore = playerLink.Score;
            playerScoreDict[playerName] = playerScore;
        }
        var maxEntry = playerScoreDict.Aggregate((l, r) => l.Value > r.Value ? l : r);

        SetWinner(maxEntry.Key + " Win");

    }

    // Start is called before the first frame update
    public void SetText(FP time)
    {
        int second = Mathf.RoundToInt(time.AsFloat);
        CountDownTime.text = second.ToString();
    }

    public void SetText(string str)
    {
        CountDownTime.text = str;
    }

    public void SetWinner(string str)
    {
        Winner.gameObject.SetActive(true);
        Winner.text = str;
    }

}

