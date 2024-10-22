namespace game.view
{
    using Quantum;
    using UnityEngine;
    using TMPro;

    public unsafe class GameView : QuantumSceneViewComponent
    {
        
        public TextMeshProUGUI ScoreBoard;
        public TextMeshProUGUI CountDownTime;


        public override void OnInitialize()
        {
           
           //GameUIManager.GetInstance().GameView = this;
        }

        public override void OnUpdateView()
        {
            

            if (ScoreBoard != null)
            {
                ScoreBoard.text = "<b>Score</b>\n";
                var playerFilter = VerifiedFrame.Filter<PlayerLink>();
                while (playerFilter.Next(out var entity, out var playerLink))
                {
                    // สมมติว่า PlayerLink มีทั้งชื่อผู้เล่นและคะแนนอยู่แล้ว
                    var playerName = VerifiedFrame.GetPlayerData(playerLink.Player).PlayerNickname;  // เข้าถึงชื่อผู้เล่นโดยตรง
                    var playerScore = playerLink.Score;          // เข้าถึงคะแนนผู้เล่นโดยตรง

                    ScoreBoard.text += $"{playerName}: {playerScore}\n";
                }
            }
        }

    }
}
