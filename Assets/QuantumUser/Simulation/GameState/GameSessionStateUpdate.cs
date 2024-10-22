namespace Quantum
{
    using Photon.Deterministic;
    using UnityEngine;
    using UnityEngine.Scripting;

    [Preserve]
    public unsafe class GameSessionStateUpdate : SystemMainThreadFilter<GameSessionStateUpdate.Filter>
    {

        public override void OnInit(Frame f)
        {
            
            GameSession* gameSession = f.Unsafe.GetPointerSingleton<GameSession>();
            gameSession->State = GameState.WaitingForPlayers;
        }

        public override void Update(Frame f, ref Filter filter)
        {
            GameSession* gameSession = f.Unsafe.GetPointerSingleton<GameSession>();
            
            if (gameSession == null)
            {
                
                return;
            }
           Debug.Log(f.PlayerCount);
            if(f.PlayerConnectedCount >= f.PlayerCount)
            {
                gameSession->State = GameState.Countdown;
                gameSession->TimeStart = gameSession->TimeStart - f.DeltaTime;
            }

            if(gameSession->State == GameState.Countdown && gameSession->TimeStart < 0)
            {
                gameSession->State = GameState.GameStarted;
                gameSession->GameTime = gameSession->GameTime - f.DeltaTime;

            }

            if (gameSession->State == GameState.GameStarted && gameSession->GameTime < 0)
            {
                gameSession->State = GameState.GameOver;

            }

        }

        public struct Filter
        {
            public EntityRef Entity;
            public GameSession* GameSession;
        }
    }
}
