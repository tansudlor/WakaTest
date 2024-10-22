namespace Quantum
{
    using Photon.Deterministic;
    using UnityEngine.Scripting;

    [Preserve]
    public unsafe class MovementSystem : SystemMainThreadFilter<MovementSystem.Filter>, ISignalOnPlayerAdded
    {
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

            filter.CharacterController->Move(f, filter.Entity, direction.XOY);
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
