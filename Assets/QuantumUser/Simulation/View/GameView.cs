namespace game.view
{
    using Quantum;
    using UnityEngine;
    using TMPro;

    public unsafe class GameView : QuantumSceneViewComponent
    {
        
        public TextMeshProUGUI ScoreBoard;

        public override void OnInitialize()
        {
           
          
        }

        public override void OnUpdateView()
        {
            if (ScoreBoard != null)
            {
                ScoreBoard.text = "<b>Score</b>\n";
                var playerFilter = VerifiedFrame.Filter<PlayerLink>();
                while (playerFilter.Next(out var entity, out var playerLink))
                {
                   
                    var playerName = VerifiedFrame.GetPlayerData(playerLink.Player).PlayerNickname;  
                    var playerScore = playerLink.Score;         

                    ScoreBoard.text += $"{playerName}: {playerScore}\n";
                }
            }
        }

    }
}
