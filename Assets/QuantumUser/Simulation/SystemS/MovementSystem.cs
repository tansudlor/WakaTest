namespace Quantum
{
    using Photon.Deterministic;
    using Quantum.Collections;
    using System;
    using UnityEngine;
    using UnityEngine.Scripting;


   

    [Preserve]
    public unsafe class MovementSystem : SystemMainThreadFilter<MovementSystem.Filter>, ISignalOnPlayerAdded
    {

        private QDictionary<int, FP> dict;
        private int count = 0;
        public override void OnInit(Frame f)
        {
            //base.OnInit(f);
            dict = f.AllocateDictionary(out f.Global->CoinData);
            
        }

        
        public void OnPlayerAdded(Frame f, PlayerRef player, bool firstTime)
        {
           
            var runtimePlayer = f.GetPlayerData(player);
            var entity = f.Create(runtimePlayer.PlayerAvatar);

            var link = new PlayerLink()
            {
                Player = player
            };
            f.Add(entity, link);
            

            if (f.Unsafe.TryGetPointer<Transform3D>(entity, out var transform))
            {
                FPVector3 pos = FPVector3.Zero;

                if (player % 2 == 0)
                {
                    transform->Position = new FPVector3(-player, 2, 0);
                }
                else
                {

                    transform->Position = new FPVector3(player+1, 2, 0);
                }
                
            }

        }

        

        public override void Update(Frame f, ref Filter filter)
        {
            dict[count] = FP._0_02;

            /*Debug.Log("count " + count + " + " + f.Number);
            Debug.Log("dict.Count " + dict.Count);*/
            /*Debug.Log((f.IsPredicted?"send frame ":"receive frame ") + f.Number);
            filter.Link->Score += 1;
            Debug.Log(filter.Link->Score);*/
            var input = f.GetPlayerInput(filter.Link->Player);

            var direction = input->Direction;

            if (direction.Magnitude > 1)
            {
                direction = direction.Normalized;
            }

            if (input->Jump.WasPressed)
            {
                filter.CharacterController->Jump(f);
            }

            
            if (input->Fire.WasPressed)
            {
                Debug.Log("Fire");
                f.Signals.SpwanCoinObject();
            }

            filter.CharacterController->Move(f, filter.Entity, direction.XOY);
            count++;
        }

        public struct Filter
        {
            public EntityRef Entity;
            public Transform3D* Transform;
            public CharacterController3D* CharacterController;
            public PlayerLink* Link;
        }
    }
}
