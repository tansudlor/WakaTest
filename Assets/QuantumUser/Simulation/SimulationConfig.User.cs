using UnityEngine;

namespace Quantum
{
    public partial class SimulationConfig : AssetObject
    {
        [Header("Prototypes")]
        public AssetRef<EntityPrototype> Coin;
        
    }
}