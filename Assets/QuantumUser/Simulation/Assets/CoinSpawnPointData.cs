namespace Quantum
{
    using Photon.Deterministic;
    using UnityEngine;

    [System.Serializable]
    public class CoinSpawnPointData : AssetObject
    {
        public FPVector3[] spawnpoints;
        public GameObject coin;
    }

}
