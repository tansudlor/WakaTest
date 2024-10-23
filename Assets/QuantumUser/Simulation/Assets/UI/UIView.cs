using Photon.Deterministic;
using Quantum;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public unsafe class UIView : QuantumCallbacks
{
    public TextMeshProUGUI CountDownTime;
    public TextMeshProUGUI Winner;
    public TextMeshProUGUI ScoreBoard;
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

        if (_session.State == GameState.GameOver)
        {
            return;
        }

        ScoreBoardData();
    }

    private void UpdateCountdown()
    {
       
        SetText(countdownTime);
        if (countdownTime < 0)
        {
            SetText("GO!!!");
           
        }
        else
        {
            
        }
    }

   
    private void UpdateGameTimer()
    {
        SetText(gameTime);
        if (gameTime < 0)
        {     
            EndGame();
        }
        else
        {
            //Debug.Log($"Game time remaining: {gameTime}");
        }
    }

    private void EndGame()
    {
        SetText("Gameover!!!");
        var maxEntry = playerScoreDict.Aggregate((l, r) => l.Value > r.Value ? l : r);
        SetWinner(maxEntry.Key + " Win");

    }

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

    private void ScoreBoardData()
    {
        ScoreBoard.text = "<b>Score</b>\n";
        var playerFilter = f.Filter<PlayerLink>();
        while (playerFilter.Next(out var entity, out var playerLink))
        {
            var playerName = f.GetPlayerData(playerLink.Player).PlayerNickname;
            var playerScore = playerLink.Score;
            playerScoreDict[playerName] = playerScore;
            ScoreBoard.text += $"{playerName}: {playerScore}\n";
        }
        
    }


}

