namespace Quantum
{
    using Photon.Deterministic;
    using UnityEngine;
    using UnityEngine.Scripting;
    using static UnityEngine.EventSystems.EventTrigger;

    [Preserve]
    public unsafe class CoinSystem : SystemSignalsOnly, ISignalOnTriggerEnter3D ,ISignalSpwanCoinObject
    {
        private EntityRef _lastCoinEntity;
        private bool _coinSpawned = false;

        public static void SpawnCoin(Frame f)
        {

            CoinSpawnPointData spawnPointDataAsset = f.FindAsset<CoinSpawnPointData>(f.Map.UserAsset.Id);

            int index = f.RNG->Next(0, spawnPointDataAsset.spawnpoints.Length);
            Debug.Log("index" + spawnPointDataAsset.spawnpoints.Length);
            Debug.Log("index" + index);
            FPVector3 coinPosition = spawnPointDataAsset.spawnpoints[index];
            Debug.Log("coinPosition" + coinPosition);
            //FPVector3 coinPosition = new FPVector3(1, 1, 1);

            EntityRef coinEntity = f.Create(f.SimulationConfig.Coin);

            Transform3D* coinEntityTransform = f.Unsafe.GetPointer<Transform3D>(coinEntity);

            coinEntityTransform->Position = coinPosition;
            
            /*if (f.Unsafe.TryGetPointer<Transform3D>(coinEntity, out var transform))
            {
                transform->Position = coinPosition;
            }*/

            Debug.Log("Fnumber " + f.Number);



        }

        public void OnTriggerEnter3D(Frame f, TriggerInfo3D info)
        {
            Debug.Log("  the coin!");
            Debug.Log(info.Other + "     " + info.Entity);
            Debug.Log("f.Has<PlayerLink>(info.Entity) " + f.Has<PlayerLink>(info.Entity));
            Debug.Log("f.Has<Coin>(info.Other) " + f.Has<Coin>(info.Other));
            if (f.Has<PlayerLink>(info.Entity) && f.Has<Coin>(info.Other))
            {
                f.Destroy(info.Other);
                Debug.Log("Player collected the coin!");
                HandleScorePlayer(f, info);
                SpawnCoin(f);
            }



        }

        public void SpwanCoinObject(Frame f)
        {
            Debug.Log((f.IsPredicted?"send frame ":"receive frame ") + f.Number);
            SpawnCoin(f);
        }

        private void HandleScorePlayer(Frame f, TriggerInfo3D info)
        {
            PlayerLink* playerLink = f.Unsafe.GetPointer<PlayerLink>(info.Entity);
            playerLink->Score++;
        }

        public struct Filter
        {
            public EntityRef Entity;
            public Transform3D* Transform;
            public Coin* Coin;
        }
    }
}
